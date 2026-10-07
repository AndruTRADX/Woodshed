import { useRef, useState } from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { PostCommentResponse } from "@/features/posts/schemas/response/PostCommentResponse";
import type { UserResponse } from "@/shared/schemas/response/UserResponse";
import { toast } from "@/shared/stores/toastStore";
import {
  invokeHubMethod,
  useHubConnection,
} from "@/shared/hooks/useHubConnection";
import type { PagedResponse } from "@/shared/schemas/response/PagedResponse";

const PAGE_SIZE = 5;
const TEMP_ID_PREFIX = "temp-";

type CommentsCache = {
  comments: PostCommentResponse[];
  pageIndex: number;
  pageCount: number;
};

const emptyCache: CommentsCache = { comments: [], pageIndex: 1, pageCount: 1 };

const mergeComments = (
  existing: PostCommentResponse[],
  incoming: PostCommentResponse[],
) => {
  const byId = new Map(
    [...existing, ...incoming].map((comment) => [comment.id, comment]),
  );
  return [...byId.values()].sort((a, b) =>
    a.createdAt.localeCompare(b.createdAt),
  );
};

const showConnectionError = (err: unknown) => {
  const message = err instanceof Error ? err.message : "Something went wrong";
  toast.add({ title: message, type: "error" });
};

const buildOptimisticComment = (
  content: string,
  postId: string,
  currentUser: UserResponse,
): PostCommentResponse => ({
  id: `${TEMP_ID_PREFIX}${crypto.randomUUID()}`,
  postId: postId,
  content,
  hasBeenEdited: false,
  createdAt: new Date().toISOString(),
  editedAt: null,
  user: {
    id: currentUser.id,
    nickName: currentUser.nickName,
    imageUrl: currentUser.imageUrl,
    biography: currentUser.biography ?? "",
    isFollowee: false,
    isFollower: false,
    followersCount: 0,
    followingsCount: 0,
    createdAt: new Date().toISOString(),
  },
});

export const useCommentsHub = (postId: string | undefined) => {
  const queryClient = useQueryClient();
  const [cache, setCacheState] = useState<CommentsCache>(emptyCache);
  const [isLoadingComments, setIsLoadingComments] = useState(true);

  const cacheRef = useRef(cache);
  const setCache = (updater: (prev: CommentsCache) => CommentsCache) => {
    const next = updater(cacheRef.current);
    cacheRef.current = next;
    setCacheState(next);
  };

  const connectionRef = useHubConnection(
    "/hubs/comments",
    { postId: postId ?? "" },
    {
      enabled: !!postId,
      handlers: {
        ReceiveComment: (comment: PostCommentResponse) =>
          setCache((prev) => ({
            ...prev,
            comments: mergeComments(prev.comments, [comment]),
          })),
        CommentDeleted: (commentId: string) =>
          setCache((prev) => ({
            ...prev,
            comments: prev.comments.filter(
              (comment) => comment.id !== commentId,
            ),
          })),
      },
      onConnected: async (connection) => {
        try {
          const page = await connection.invoke<
            PagedResponse<PostCommentResponse>
          >("LoadComments", postId, 1, PAGE_SIZE);
          setCache((prev) => ({
            comments: mergeComments(prev.comments, page.data),
            pageIndex: page.pageIndex,
            pageCount: page.pageCount,
          }));
        } finally {
          setIsLoadingComments(false);
        }
      },
      onError: showConnectionError,
      onDisconnected: () => {
        cacheRef.current = emptyCache;
        setCacheState(emptyCache);
        setIsLoadingComments(true);
      },
    },
  );

  const loadMoreMutation = useMutation({
    mutationFn: () =>
      invokeHubMethod<PagedResponse<PostCommentResponse>>(
        connectionRef,
        "LoadComments",
        postId,
        cache.pageIndex + 1,
        PAGE_SIZE,
      ),
    onSuccess: (page) =>
      setCache((prev) => ({
        comments: mergeComments(prev.comments, page.data),
        pageIndex: page.pageIndex,
        pageCount: page.pageCount,
      })),
    onError: showConnectionError,
  });

  const sendCommentMutation = useMutation({
    mutationFn: (body: string) =>
      invokeHubMethod(connectionRef, "SendComment", postId, body),
    onMutate: (body: string) => {
      const currentUser = queryClient.getQueryData<UserResponse>(["user"]);
      if (!currentUser || !postId) return undefined;

      const previousCache = cache;
      const optimisticComment = buildOptimisticComment(
        body,
        postId,
        currentUser,
      );
      setCache((prev) => ({
        ...prev,
        comments: mergeComments(prev.comments, [optimisticComment]),
      }));
      return { previousCache, optimisticId: optimisticComment.id };
    },
    onSuccess: (_data, _body, context) => {
      if (!context?.optimisticId) return;
      setCache((prev) => ({
        ...prev,
        comments: prev.comments.filter(
          (comment) => comment.id !== context.optimisticId,
        ),
      }));
    },
    onError: (err, _body, context) => {
      if (context?.previousCache) setCache(() => context.previousCache);
      showConnectionError(err);
    },
  });

  const deleteCommentMutation = useMutation({
    mutationFn: (commentId: string) =>
      invokeHubMethod(connectionRef, "DeleteComment", postId, commentId),
    onMutate: (commentId: string) => {
      const previousCache = cache;
      setCache((prev) => ({
        ...prev,
        comments: prev.comments.filter((comment) => comment.id !== commentId),
      }));
      return { previousCache };
    },
    onError: (err, _commentId, context) => {
      if (context?.previousCache) setCache(() => context.previousCache);
      showConnectionError(err);
    },
  });

  return {
    comments: cache.comments,
    isLoadingComments,
    hasMoreComments: cache.pageIndex < cache.pageCount,
    loadMoreComments: loadMoreMutation.mutateAsync,
    sendCommentAsync: sendCommentMutation.mutateAsync,
    isPendingSendComment: sendCommentMutation.isPending,
    deleteCommentAsync: deleteCommentMutation.mutateAsync,
  };
};

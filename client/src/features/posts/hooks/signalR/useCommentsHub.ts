import { useReducer, useState } from "react";
import {
  useMutation,
  useQueryClient,
  type QueryClient,
} from "@tanstack/react-query";
import type { PostCommentResponse } from "@/features/posts/schemas/response/PostCommentResponse";
import type { PostResponse } from "@/features/posts/schemas/response/PostResponse";
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

type CommentsCacheAction =
  | { type: "comments-loaded"; page: PagedResponse<PostCommentResponse> }
  | { type: "comment-upserted"; comment: PostCommentResponse }
  | { type: "comment-removed"; commentId: string }
  | { type: "cache-replaced"; cache: CommentsCache };

function commentsCacheReducer(
  state: CommentsCache,
  action: CommentsCacheAction,
): CommentsCache {
  switch (action.type) {
    case "comments-loaded":
      return {
        comments: mergeComments(state.comments, action.page.data),
        pageIndex: action.page.pageIndex,
        pageCount: action.page.pageCount,
      };
    case "comment-upserted":
      return {
        ...state,
        comments: mergeComments(state.comments, [action.comment]),
      };
    case "comment-removed":
      return {
        ...state,
        comments: state.comments.filter((c) => c.id !== action.commentId),
      };
    case "cache-replaced":
      return action.cache;
  }
}

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

const bumpCommentsCount = (
  queryClient: QueryClient,
  postId: string,
  delta: 1 | -1,
) => {
  queryClient.setQueriesData<PagedResponse<PostResponse>>(
    { queryKey: ["posts"] },
    (paged) =>
      paged && {
        ...paged,
        data: paged.data.map((post) =>
          post.id === postId
            ? { ...post, commentsCount: post.commentsCount + delta }
            : post,
        ),
      },
  );

  queryClient.setQueryData<PostResponse>(
    ["post", postId],
    (post) => post && { ...post, commentsCount: post.commentsCount + delta },
  );
};

export const useCommentsHub = (postId: string | undefined) => {
  const queryClient = useQueryClient();
  const [cache, dispatch] = useReducer(commentsCacheReducer, emptyCache);
  const [isLoadingComments, setIsLoadingComments] = useState(true);

  const connectionRef = useHubConnection(
    "/hubs/comments",
    { postId: postId ?? "" },
    {
      enabled: !!postId,
      handlers: {
        ReceiveComment: (comment: PostCommentResponse) => {
          dispatch({ type: "comment-upserted", comment });
          if (postId) bumpCommentsCount(queryClient, postId, 1);
        },
        CommentDeleted: (commentId: string) => {
          dispatch({ type: "comment-removed", commentId });
          if (postId) bumpCommentsCount(queryClient, postId, -1);
        },
      },
      onConnected: async (connection) => {
        try {
          const page = await connection.invoke<
            PagedResponse<PostCommentResponse>
          >("LoadComments", postId, 1, PAGE_SIZE);
          dispatch({ type: "comments-loaded", page });
        } finally {
          setIsLoadingComments(false);
        }
      },
      onError: showConnectionError,
      onDisconnected: () => {
        dispatch({ type: "cache-replaced", cache: emptyCache });
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
    onSuccess: (page) => dispatch({ type: "comments-loaded", page }),
    onError: showConnectionError,
  });

  const sendCommentMutation = useMutation({
    mutationFn: (content: string) =>
      invokeHubMethod(connectionRef, "SendComment", postId, content),
    onMutate: (content: string) => {
      const currentUser = queryClient.getQueryData<UserResponse>(["user"]);
      if (!currentUser || !postId) return undefined;

      const previousCache = cache;
      const optimisticComment = buildOptimisticComment(
        content,
        postId,
        currentUser,
      );
      dispatch({ type: "comment-upserted", comment: optimisticComment });
      return { previousCache, optimisticId: optimisticComment.id };
    },
    onSuccess: (_data, _content, context) => {
      if (!context?.optimisticId) return;
      dispatch({ type: "comment-removed", commentId: context.optimisticId });
    },
    onError: (err, _content, context) => {
      if (context?.previousCache)
        dispatch({ type: "cache-replaced", cache: context.previousCache });
      showConnectionError(err);
    },
  });

  const deleteCommentMutation = useMutation({
    mutationFn: (commentId: string) =>
      invokeHubMethod(connectionRef, "DeleteComment", postId, commentId),
    onMutate: (commentId: string) => {
      const previousCache = cache;
      dispatch({ type: "comment-removed", commentId });
      return { previousCache };
    },
    onError: (err, _commentId, context) => {
      if (context?.previousCache)
        dispatch({ type: "cache-replaced", cache: context.previousCache });
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

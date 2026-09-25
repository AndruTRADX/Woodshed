import type { PostResponse } from "@/features/posts/schemas/response/PostResponse";
import { useOptimisticUpdate } from "@/shared/hooks/useOptimisticUpdate";
import type { PagedResponse } from "@/shared/schemas/response/PagedResponse";
import agent from "@/shared/services/agent";
import { useMutation } from "@tanstack/react-query";

const updatePostInPagedCache = (
  paged: PagedResponse<PostResponse>,
  postId: string,
  isLiked: boolean,
): PagedResponse<PostResponse> => ({
  ...paged,
  data: paged.data.map((post) =>
    post.id === postId
      ? {
          ...post,
          isLiked,
          likesCount: post.likesCount + (isLiked ? 1 : -1),
        }
      : post,
  ),
});

export const useLikePost = (postId: string) => {
  const { onMutate, onError } = useOptimisticUpdate<
    PagedResponse<PostResponse>,
    void
  >({
    optimisticQueryKey: () => ["posts"],
    updater: (paged) => updatePostInPagedCache(paged, postId, true),
  });

  const { mutateAsync, isPending } = useMutation({
    mutationFn: () => agent.post(`/post/${postId}/likes`),
    onMutate,
    onError,
  });

  return {
    likePostAsync: mutateAsync,
    isPendingLikePost: isPending,
  };
};

export const useDeleteLikePost = (postId: string) => {
  const { onMutate, onError } = useOptimisticUpdate<
    PagedResponse<PostResponse>,
    void
  >({
    optimisticQueryKey: () => ["posts"],
    updater: (paged) => updatePostInPagedCache(paged, postId, false),
  });

  const { mutateAsync, isPending } = useMutation({
    mutationFn: () => agent.delete(`/post/${postId}/likes`),
    onMutate,
    onError,
  });

  return {
    deleteLikePostAsync: mutateAsync,
    isPendingDeleteLikePost: isPending,
  };
};

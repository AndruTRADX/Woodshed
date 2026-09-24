import type { PostResponse } from "@/features/posts/schemas/response/PostResponse";
import { useOptimisticUpdate } from "@/shared/hooks/useOptimisticUpdate";
import agent from "@/shared/services/agent";
import { useMutation } from "@tanstack/react-query";

export const useLikePost = (postId: string) => {
  const { onMutate, onError } = useOptimisticUpdate<PostResponse, { id: string }>({
    optimisticQueryKey: ({ id }) => ["post", id],
    relatedQueryKeysToCancel: () => [["posts"]],
    updater: (post) => {
      return {
        ...post,
        isLiked: true
      }
    },
  })


  const { mutateAsync, isPending } = useMutation({
    mutationFn: async () => {
      return await agent.post(`/post/${postId}/likes`);
    },
    onMutate,
    onError,
  });

  return {
    likePostAsync: mutateAsync,
    isPendingLikePost: isPending,
  };
};

export const useDeleteLikePost = (postId: string) => {
  const { onMutate, onError } = useOptimisticUpdate<PostResponse, { id: string }>({
    optimisticQueryKey: ({ id }) => ["post", id],
    relatedQueryKeysToCancel: () => [["posts"]],
    updater: (post) => {
      return {
        ...post,
        isLiked: false
      }
    },
  })


  const { mutateAsync, isPending } = useMutation({
    mutationFn: async () => {
      return await agent.delete(`/post/${postId}/likes`);
    },
    onMutate,
    onError,
  });

  return {
    likePostAsync: mutateAsync,
    isPendingLikePost: isPending,
  };
};

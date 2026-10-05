import type { PostResponse } from "@/features/posts/schemas/response/PostResponse";
import type { PagedResponse } from "@/shared/schemas/response/PagedResponse";
import agent from "@/shared/services/agent";
import { useMutation, useQueryClient } from "@tanstack/react-query";

const updatePost = (post: PostResponse, isLiked: boolean): PostResponse => ({
  ...post,
  isLiked,
  likesCount: post.likesCount + (isLiked ? 1 : -1),
});

const useTogglePostLike = (postId: string, isLiked: boolean) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () =>
      isLiked
        ? agent.post(`/post/${postId}/likes`)
        : agent.delete(`/post/${postId}/likes`),
    onMutate: async () => {
      await queryClient.cancelQueries({ queryKey: ["posts"] });
      await queryClient.cancelQueries({ queryKey: ["post", postId] });

      queryClient.setQueriesData<PagedResponse<PostResponse>>(
        { queryKey: ["posts"] },
        (paged) =>
          paged && {
            ...paged,
            data: paged.data.map((post) =>
              post.id === postId ? updatePost(post, isLiked) : post,
            ),
          },
      );

      queryClient.setQueryData<PostResponse>(
        ["post", postId],
        (post) => post && updatePost(post, isLiked),
      );
    },
    onSettled: async () => {
      await queryClient.invalidateQueries({ queryKey: ["posts"] });
      await queryClient.invalidateQueries({ queryKey: ["post", postId] });
    },
  });
};

export const useLikePost = (postId: string) => {
  const { mutateAsync, isPending } = useTogglePostLike(postId, true);

  return {
    likePostAsync: mutateAsync,
    isPendingLikePost: isPending,
  };
};

export const useDeleteLikePost = (postId: string) => {
  const { mutateAsync, isPending } = useTogglePostLike(postId, false);

  return {
    deleteLikePostAsync: mutateAsync,
    isPendingDeleteLikePost: isPending,
  };
};

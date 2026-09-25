import type { CreatePostRequest } from "@/features/posts/schemas/request/CreatePostRequest";
import type { PostSpecificationParams } from "@/features/posts/schemas/request/PostSpecificationParams";
import type { UpdatePostRequest } from "@/features/posts/schemas/request/UpdatePostRequest";
import type { PostResponse } from "@/features/posts/schemas/response/PostResponse";
import type { PagedResponse } from "@/shared/schemas/response/PagedResponse";
import agent from "@/shared/services/agent";
import {
  keepPreviousData,
  useMutation,
  useQuery,
  useQueryClient,
} from "@tanstack/react-query";

export const useGetPosts = (params: PostSpecificationParams) => {
  const { data, isLoading, error } = useQuery<PagedResponse<PostResponse>>({
    queryKey: ["posts"],
    queryFn: () => agent.get<PagedResponse<PostResponse>>(`/post`, { params }),
    placeholderData: keepPreviousData,
  });

  return {
    pagedPosts: data ?? null,
    isLoadingPosts: isLoading,
    errorPosts: error,
  };
};

export const useGetPostById = (postId: string) => {
  const { data, isLoading, error } = useQuery<PostResponse>({
    queryKey: ["post", postId],
    queryFn: () => agent.get<PostResponse>(`/post/${postId}`),
    placeholderData: keepPreviousData,
  });

  return {
    post: data ?? null,
    isLoadingPost: isLoading,
    errorPost: error,
  };
};

export const useCreatePost = () => {
  const queryClient = useQueryClient();
  const { mutateAsync, isPending } = useMutation({
    mutationFn: async (post: CreatePostRequest) => {
      return await agent.post<string>("/post", post);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ["posts"],
      });
    },
  });

  return {
    createPostAsync: mutateAsync,
    isPendingCreatePost: isPending,
  };
};

export const useUpdatePost = (postId: string) => {
  const queryClient = useQueryClient();
  const { mutateAsync, isPending } = useMutation({
    mutationFn: async (post: UpdatePostRequest) => {
      return await agent.post<string>(`/post/${postId}`, post);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ["posts"],
      });
      await queryClient.invalidateQueries({
        queryKey: ["post", postId],
      });
    },
  });

  return {
    UpdatePostAsync: mutateAsync,
    isPendingUpdatePost: isPending,
  };
};

export const useDeletePost = (postId: string) => {
  const queryClient = useQueryClient();
  const { mutateAsync, isPending } = useMutation({
    mutationFn: async () => {
      return await agent.post(`/post/${postId}`);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ["posts"],
      });
      queryClient.removeQueries({
        queryKey: ["post", postId],
      });
    },
  });

  return {
    deletePostAsync: mutateAsync,
    isPendingDeletePost: isPending,
  };
};

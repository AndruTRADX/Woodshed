import type { PostSpecificationParams } from "@/features/posts/schemas/request/PostSpecificationParams";
import type { PostRequest } from "@/features/posts/schemas/response/PostResponse";
import type { PagedResponse } from "@/shared/schemas/response/PagedResponse";
import agent from "@/shared/services/agent";
import { keepPreviousData, useQuery } from "@tanstack/react-query";

export const useGetPosts = (params: PostSpecificationParams) => {
  const { data, isLoading, error } = useQuery<PagedResponse<PostRequest>>({
    queryKey: ["posts"],
    queryFn: () => agent.get<PagedResponse<PostRequest>>(`/post`, { params }),
    placeholderData: keepPreviousData,
  });

  return {
    pagedPosts: data ?? null,
    isLoadingPosts: isLoading,
    errorPosts: error,
  };
};

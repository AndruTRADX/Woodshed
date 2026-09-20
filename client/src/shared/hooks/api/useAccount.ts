import type { UserResponse } from "@/shared/schemas/response/UserResponse";
import agent from "@/shared/services/agent";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "react-router";

export const useGetCurrentUser = () => {
  const { data, isLoading, error } = useQuery<UserResponse | null>({
    queryKey: ["user"],
    queryFn: () => agent.get<UserResponse>(`/identity/my-account`),
    staleTime: 1000 * 60 * 5,
    retry: false,
  });

  return {
    user: data ?? null,
    isLoadingUser: isLoading,
    errorUser: error,
  };
};

export const useLogoutAccount = () => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const { mutateAsync, isPending } = useMutation({
    mutationFn: async () => {
      return await agent.post("/identity/sign-out");
    },
    onSuccess: async () => {
      queryClient.removeQueries({
        queryKey: ["user"],
      });
      queryClient.removeQueries({
        queryKey: ["posts"],
      });
      navigate(`/login`);
    },
  });

  return {
    logoutAccountAsync: mutateAsync,
    isPendingLogoutAccount: isPending,
  };
};

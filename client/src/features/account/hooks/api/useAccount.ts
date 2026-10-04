import agent from "@/shared/services/agent";
import {
  keepPreviousData,
  useMutation,
  useQuery,
  useQueryClient,
} from "@tanstack/react-query";
import type { LoginRequest } from "@account/schemas/request/LoginRequest";
import type { RegisterRequest } from "@account/schemas/request/RegisterRequest";
import { useLocation, useNavigate } from "react-router";
import { useAudio } from "@/shared/hooks/useAudio";
import { toast } from "@/shared/stores/toastStore";
import type { UserAccountResponse } from "@/shared/schemas/response/UserAccountResponse";
import type { PagedRequest } from "@/shared/schemas/request/PagedRequest";
import type { PhotoResponse } from "@/features/account/schemas/response/PhotoResponse";
import type { PagedResponse } from "@/shared/schemas/response/PagedResponse";
import type { UserResponse } from "@/shared/schemas/response/UserResponse";
import type { EditAccountRequest } from "@/features/account/schemas/request/EditAccountRequest";
import type { AddPhotoRequest } from "@/features/account/schemas/request/AddPhotoRequest";
import { useOptimisticUpdate } from "@/shared/hooks/useOptimisticUpdate";
import { useGetCurrentUser } from "@/shared/hooks/api/useAccount";
import type { FollowerResponse } from "@/features/account/schemas/response/FollowerResponse";
import type { FolloweeResponse } from "@/features/account/schemas/response/FolloweeResponse";

// Account
export const useGetAccountById = (id: string | undefined) => {
  const {
    data: account,
    isLoading: isLoadingAccount,
    error: errorAccount,
  } = useQuery<UserAccountResponse>({
    queryKey: ["account", id],
    queryFn: () => agent.get<UserAccountResponse>(`/account/${id}`),
    enabled: !!id,
  });

  return {
    account,
    isLoadingAccount,
    errorAccount,
  };
};

export const useEditAccount = () => {
  const queryClient = useQueryClient();

  const { mutateAsync, isPending } = useMutation({
    mutationFn: async (request: EditAccountRequest) => {
      return await agent.put<UserAccountResponse>("/account", request);
    },
    onSuccess: async (account: UserAccountResponse) => {
      queryClient.setQueryData(["account", account.id], account);

      queryClient.setQueryData(["user"], (data: UserResponse) => {
        if (!data) return data;

        return {
          ...data,
          nickName: account.nickName,
          biography: account.biography,
        };
      });
    },
  });

  return {
    editAccountAsync: mutateAsync,
    isPendingEditAccount: isPending,
  };
};

export const useLoginAccount = () => {
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  const location = useLocation();
  const { play } = useAudio();

  const { mutateAsync, isPending } = useMutation({
    mutationFn: async (login: LoginRequest) => {
      return await agent.post("/login?useCookies=true", login);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ["user"],
      });
      play("login");
      navigate(location.state?.from || "/posts");
      toast.add({
        title: "Welcome",
        description: "Happy to have you here!",
      });
    },
  });

  return {
    loginAccountAsync: mutateAsync,
    isPendingLoginAccount: isPending,
  };
};

export const useRegisterAccount = () => {
  const navigate = useNavigate();

  const { mutateAsync, isPending } = useMutation({
    mutationFn: async (register: RegisterRequest) => {
      return await agent.post("/identity/register", register);
    },
    onSuccess: async () => {
      toast.add({
        type: "success",
        title: "Registered successfully",
        description: "You can now log in into your woodshed!",
      });
      navigate("/login");
    },
  });

  return {
    registerAccountAsync: mutateAsync,
    isPendingRegisterAccount: isPending,
  };
};

// Photos
export const useGetAccountPhotosById = (
  id: string | undefined,
  params: PagedRequest,
) => {
  const { user: currentUser } = useGetCurrentUser();

  const {
    data: pagedPhotos,
    isLoading: isLoadingPagedPhotos,
    error: errorPagedPhotos,
  } = useQuery<PagedResponse<PhotoResponse>>({
    queryKey: ["account", id, "photos", params],
    queryFn: () =>
      agent.get<PagedResponse<PhotoResponse>>(`/account/${id}/photos`, {
        params,
      }),
    placeholderData: keepPreviousData,
    enabled: !!id,
  });

  const isCurrentUser = !!id && id === currentUser?.id;

  return {
    pagedPhotos,
    isLoadingPagedPhotos,
    errorPagedPhotos,
    isCurrentUser,
  };
};

export const useAddPhotoAccount = () => {
  const queryClient = useQueryClient();
  const user = queryClient.getQueryData<UserResponse>(["user"]);

  const { mutateAsync, isPending } = useMutation({
    mutationFn: async (request: AddPhotoRequest) => {
      return await agent.post<PhotoResponse>("/account/photos", request, {
        asFormData: true,
      });
    },
    onSuccess: async (photo: PhotoResponse) => {
      await queryClient.invalidateQueries({
        queryKey: ["account"],
      });

      queryClient.setQueryData(["user"], (data: UserResponse) => {
        if (!data) return data;

        return {
          ...data,
          imageUrl: data.imageUrl ?? photo.url,
        };
      });

      queryClient.setQueryData(
        ["account", user?.id],
        (data: UserAccountResponse) => {
          if (!data) return data;

          return {
            ...data,
            imageUrl: data.imageUrl ?? photo.url,
          };
        },
      );
    },
  });

  return {
    addPhotoAsync: mutateAsync,
    isPendingAddPhoto: isPending,
  };
};

export const useDeletePhotoAccount = () => {
  const queryClient = useQueryClient();
  const user = queryClient.getQueryData<UserResponse>(["user"]);

  const { mutateAsync, isPending } = useMutation({
    mutationFn: async (response: PhotoResponse) => {
      return await agent.delete(`/account/${response.id}/photos`);
    },
    onSuccess: async () => {
      queryClient.invalidateQueries({
        queryKey: ["user"],
      });

      queryClient.invalidateQueries({
        queryKey: ["account", user?.id],
      });
    },
  });

  return {
    deletePhotoAsync: mutateAsync,
    isPendingDeletePhoto: isPending,
  };
};

export const useSetMainPhotoAccount = () => {
  const queryClient = useQueryClient();
  const user = queryClient.getQueryData<UserResponse>(["user"]);

  const { mutateAsync, isPending } = useMutation({
    mutationFn: async (response: PhotoResponse) => {
      return await agent.put(`/account/photos/${response.id}/main`);
    },
    onSuccess: async (_, photo) => {
      queryClient.setQueryData(["user"], (data: UserResponse) => {
        if (!data) return data;

        return {
          ...data,
          imageUrl: photo.url,
        };
      });

      queryClient.setQueryData(
        ["account", user?.id],
        (data: UserAccountResponse) => {
          if (!data) return data;

          return {
            ...data,
            imageUrl: photo.url,
          };
        },
      );
    },
  });

  return {
    setMainPhotoAsync: mutateAsync,
    isPendingSetMainPhoto: isPending,
  };
};

// Account Following
const showsFollowState = (queryKey: readonly unknown[]) =>
  queryKey[0] === "account" &&
  (queryKey[2] === "followers" || queryKey[2] === "following");

export const useFollowAccount = () => {
  const queryClient = useQueryClient();
  const currentUserId = queryClient.getQueryData<UserResponse>(["user"])?.id;

  const { onMutate, onError } = useOptimisticUpdate<
    UserAccountResponse,
    { targetUserId: string }
  >({
    optimisticQueryKey: ({ targetUserId }) => ["account", targetUserId],
    updater: (account) => ({
      ...account,
      isFollowee: true,
      followersCount: account.followersCount + 1,
    }),
  });

  const { mutateAsync, isPending } = useMutation({
    mutationFn: async ({ targetUserId }: { targetUserId: string }) => {
      return await agent.post(`/account/${targetUserId}/follow`);
    },
    onMutate,
    onError,
    onSuccess: async (_data, { targetUserId }) => {
      await queryClient.invalidateQueries({
        queryKey: ["account", targetUserId],
      });
      await queryClient.invalidateQueries({
        queryKey: ["account", currentUserId],
      });
      await queryClient.invalidateQueries({
        predicate: (query) => showsFollowState(query.queryKey),
      });
    },
  });

  return {
    followAccountAsync: mutateAsync,
    isPendingFollowAccount: isPending,
  };
};

export const useUnfollowAccount = () => {
  const queryClient = useQueryClient();
  const currentUserId = queryClient.getQueryData<UserResponse>(["user"])?.id;

  const { onMutate, onError } = useOptimisticUpdate<
    UserAccountResponse,
    { targetUserId: string }
  >({
    optimisticQueryKey: ({ targetUserId }) => ["account", targetUserId],
    updater: (account) => ({
      ...account,
      isFollowee: false,
      followersCount: Math.max(0, account.followersCount - 1),
    }),
  });

  const { mutateAsync, isPending } = useMutation({
    mutationFn: async ({ targetUserId }: { targetUserId: string }) => {
      return await agent.delete(`/account/${targetUserId}/follow`);
    },
    onMutate,
    onError,
    onSuccess: async (_data, { targetUserId }) => {
      await queryClient.invalidateQueries({
        queryKey: ["account", targetUserId],
      });
      await queryClient.invalidateQueries({
        queryKey: ["account", currentUserId],
      });
      await queryClient.invalidateQueries({
        predicate: (query) => showsFollowState(query.queryKey),
      });
    },
  });

  return {
    unfollowAccountAsync: mutateAsync,
    isPendingUnfollowAccount: isPending,
  };
};

export const useGetPagedFollowers = (
  accountId: string | undefined,
  params: PagedRequest,
) => {
  const {
    data: pagedFollowers,
    isLoading: isLoadingPagedFollowers,
    error: errorPagedFollowers,
  } = useQuery<PagedResponse<FollowerResponse>>({
    queryKey: ["account", accountId, "followers", params],
    queryFn: () =>
      agent.get<PagedResponse<FollowerResponse>>(
        `/account/${accountId}/followers`,
        {
          params,
        },
      ),
    placeholderData: keepPreviousData,
    enabled: !!accountId,
  });

  return {
    pagedFollowers,
    isLoadingPagedFollowers,
    errorPagedFollowers,
  };
};

export const useGetPagedFollowing = (
  accountId: string | undefined,
  params: PagedRequest,
) => {
  const {
    data: pagedFollowing,
    isLoading: isLoadingPagedFollowing,
    error: errorPagedFollowing,
  } = useQuery<PagedResponse<FolloweeResponse>>({
    queryKey: ["account", accountId, "following", params],
    queryFn: () =>
      agent.get<PagedResponse<FolloweeResponse>>(
        `/account/${accountId}/following`,
        {
          params,
        },
      ),
    placeholderData: keepPreviousData,
    enabled: !!accountId,
  });

  return {
    pagedFollowing,
    isLoadingPagedFollowing,
    errorPagedFollowing,
  };
};

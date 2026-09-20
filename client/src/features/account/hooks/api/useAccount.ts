import agent from "@/shared/services/agent";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { LoginRequest } from "@account/schemas/request/LoginRequest";
import type { RegisterRequest } from "@account/schemas/request/RegisterRequest";
import { useLocation, useNavigate } from "react-router";
import { useAudio } from "@/shared/hooks/useAudio";

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
      navigate(location.state?.from || "/posts");
      console.log("YES!");
      play("login");
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
      // toast.add({
      //   type: "success",
      //   title: "Registered successfully",
      //   description: "You can now log in into your woodshed!",
      // });
      navigate("/login");
    },
  });

  return {
    registerAccountAsync: mutateAsync,
    isPendingRegisterAccount: isPending,
  };
};

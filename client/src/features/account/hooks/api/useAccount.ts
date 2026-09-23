import agent from "@/shared/services/agent";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { LoginRequest } from "@account/schemas/request/LoginRequest";
import type { RegisterRequest } from "@account/schemas/request/RegisterRequest";
import { useLocation, useNavigate } from "react-router";
import { useAudio } from "@/shared/hooks/useAudio";
import { toast } from "sonner";
import { Piano } from "lucide-react";
import { createElement } from "react";

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
      navigate("/login");
    },
  });

  return {
    registerAccountAsync: mutateAsync,
    isPendingRegisterAccount: isPending,
  };
};

import { requiredString } from "@/shared/lib/utils";
import { z } from "zod";

export const LoginRequestSchema = z.object({
  email: z.email().min(3).max(256),
  password: requiredString("Password", 6, 512),
});

export type LoginRequest = z.infer<typeof LoginRequestSchema>;

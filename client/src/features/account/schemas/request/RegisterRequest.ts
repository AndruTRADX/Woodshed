import { optionalString, requiredString } from "@/shared/lib/utils";
import { z } from "zod";

export const RegisterRequestSchema = z.object({
  email: z.email().min(3).max(256),
  password: requiredString("Password", 6, 512),
  nickName: requiredString("NickName", 3, 64),
  name: optionalString("Name", 155),
  lastName: optionalString("LastName", 155),
  biography: optionalString("Biography", 512),
});

export type RegisterRequest = z.infer<typeof RegisterRequestSchema>;

import { UserAccountResponseSchema } from "@/shared/schemas/response/UserAccountResponse";
import { z } from "zod";

export const FolloweeResponseSchema = z.object({
  followedAt: z.iso.datetime(),
  followee: UserAccountResponseSchema,
});

export type FolloweeResponse = z.infer<typeof FolloweeResponseSchema>;

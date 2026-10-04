import { UserAccountResponseSchema } from "@/shared/schemas/response/UserAccountResponse";
import { z } from "zod"

export const FollowerResponseSchema = z.object({
  followedAt: z.iso.datetime(),
  follower: UserAccountResponseSchema
})

export type FollowerResponse = z.infer<typeof FollowerResponseSchema>
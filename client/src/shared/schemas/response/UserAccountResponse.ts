import { z } from "zod"

export const UserAccountResponseSchema = z.object({
  id: z.string(),
  biography: z.string(),
  displayName: z.string(),
  imageUrl: z.string().nullable(),
  following: z.boolean(),
  followedBy: z.boolean(),
  followersCount: z.number(),
  followingsCount: z.number(),
})

export type UserAccountResponse = z.infer<typeof UserAccountResponseSchema>
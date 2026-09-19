import { z } from "zod"

export const UserResponseSchema = z.object({
  id: z.string(),
  nickName: z.string(),
  email: z.string(),
  name: z.string(),
  lastName: z.string(),
  biography: z.string(),
  imageUrl: z.string(),
  createdAt: z.iso.datetime(),
})

export type UserResponse = z.infer<typeof UserResponseSchema>
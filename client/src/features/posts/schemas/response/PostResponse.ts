import { UserAccountResponseSchema } from "@/shared/schemas/response/UserAccountResponse";
import { z } from "zod";

export const PostResponseSchema = z.object({
  id: z.string(),
  content: z.string(),
  hasBeenEdited: z.boolean(),
  createdAt: z.iso.datetime(),
  editedAt: z.iso.datetime().nullable(),
  userId: z.string(),
  commentsCount: z.number(),
  likesCount: z.number(),
  user: UserAccountResponseSchema,
});

export type PostResponse = z.infer<typeof PostResponseSchema>;
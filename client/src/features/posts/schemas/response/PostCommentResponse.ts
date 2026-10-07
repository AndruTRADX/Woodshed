import { UserAccountResponseSchema } from "@/shared/schemas/response/UserAccountResponse";
import { z } from "zod";

export const PostCommentResponseSchema = z.object({
  id: z.string(),
  postId: z.string(),
  content: z.string(),
  hasBeenEdited: z.boolean(),
  createdAt: z.iso.datetime(),
  editedAt: z.iso.datetime().nullable(),
  user: UserAccountResponseSchema,
});

export type PostCommentResponse = z.infer<typeof PostCommentResponseSchema>;

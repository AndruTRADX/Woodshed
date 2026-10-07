import { requiredString } from "@/shared/lib/utils";
import { z } from "zod";

export const CreatePostCommentRequestSchema = z.object({
  content: requiredString("Body", 3, 512),
});

export type CreatePostCommentRequest = z.infer<
  typeof CreatePostCommentRequestSchema
>;

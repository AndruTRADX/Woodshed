import { requiredString } from "@/shared/lib/utils";
import { z } from "zod";

export const CreatePostRequestSchema = z.object({
  content: requiredString("Content", 3, 3072),
});

export type CreatePostRequest = z.infer<typeof CreatePostRequestSchema>;

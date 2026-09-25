import { requiredString } from "@/shared/lib/utils";
import { z } from "zod";

export const UpdatePostRequestSchema = z.object({
  content: requiredString("Content", 3, 3072),
});

export type UpdatePostRequest = z.infer<typeof UpdatePostRequestSchema>;

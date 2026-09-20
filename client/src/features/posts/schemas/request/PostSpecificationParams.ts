import { optionalString } from "@/shared/lib/utils";
import { pagedRequestSchema } from "@/shared/schemas/request/PagedRequest";
import { z } from "zod";

export const PostSpecificationParamsSchema = pagedRequestSchema.extend({
  isMyPost: z.boolean().optional(),
  userId: optionalString("UserId"),
});

export type PostSpecificationParams = z.infer<
  typeof PostSpecificationParamsSchema
>;

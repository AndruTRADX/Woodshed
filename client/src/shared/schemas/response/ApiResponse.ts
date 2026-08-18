import { z } from "zod"

export const createApiResponseSchema = <T extends z.ZodTypeAny>(dataSchema: T) =>
  z.object({
    title: z.string(),
    message: z.string(),
    success: z.boolean(),
    data: dataSchema.optional(),
    errors: z.record(z.string(), z.array(z.string())).nullable(),
  })

export type ApiResponse<T> = z.infer<ReturnType<typeof createApiResponseSchema<z.ZodType<T>>>>

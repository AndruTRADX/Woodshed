import { optionalString, requiredString } from "@/shared/lib/utils";
import { z } from "zod";

export const EditAccountRequestSchema = z.object({
  nickName: requiredString("Nickname", 1, 64),
  biography: optionalString("Biography", 1024),
  name: optionalString("Name", 155),
  lastName: optionalString("Last Name", 155),
});

export type EditAccountRequest = z.infer<typeof EditAccountRequestSchema>;

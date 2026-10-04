import type { CreatePostRequest } from "@/features/posts/schemas/request/CreatePostRequest";
import { Button } from "@sharedUi/button";
import { Spinner } from "@sharedUi/spinner";
import TextInput from "@sharedForms/TextInput";
import { useMemo } from "react";
import type { UseFormReturn } from "react-hook-form";
import { Send } from "lucide-react";

type Props = {
  form: UseFormReturn<CreatePostRequest>;
  onSubmit: (data: CreatePostRequest) => void | Promise<void>;
  isPending: boolean;
  formId: string;
  rows?: number;
};

export function PostForm({
  form,
  onSubmit,
  isPending,
  formId,
  rows = 1,
}: Props) {
  const {
    formState: { isValid },
  } = form;

  const isDisabled = useMemo(() => isPending || !isValid, [isPending, isValid]);

  return (
    <form
      id={formId}
      onSubmit={form.handleSubmit(onSubmit)}
      className="flex gap-3 items-center"
    >
      <TextInput
        control={form.control}
        name="content"
        placeholder="What's on your mind?"
        multiline
        rows={rows}
        className="rounded-sm bg-card dark:bg-card border-none"
      />

      <div className="flex justify-end">
        <Button
          type="submit"
          form={formId}
          disabled={isDisabled}
          size="icon-xl"
          className="rounded-full aspect-square"
        >
          {isPending ? (
            <>
              <Spinner />
            </>
          ) : (
            <Send />
          )}
        </Button>
      </div>
    </form>
  );
}

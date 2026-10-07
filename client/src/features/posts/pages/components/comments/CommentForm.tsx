import { useCallback, useMemo } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { CardContent } from "@sharedUi/card";
import { Button } from "@sharedUi/button";
import { Spinner } from "@sharedUi/spinner";
import { ButtonGroup } from "@sharedUi/button-group";
import TextInput from "@sharedForms/TextInput";
import { Loader, Send } from "lucide-react";
import {
  CreatePostCommentRequestSchema,
  type CreatePostCommentRequest,
} from "@/features/posts/schemas/request/CreatePostCommentRequest";

interface Props {
  onSubmit: (content: string) => Promise<unknown>;
  isPending: boolean;
}

export default function CommentForm({ onSubmit, isPending }: Props) {
  const form = useForm<CreatePostCommentRequest>({
    resolver: zodResolver(CreatePostCommentRequestSchema),
    defaultValues: { content: "" },
    mode: "onTouched",
  });

  const {
    formState: { isValid },
  } = form;

  const isDisabled = useMemo(() => isPending || !isValid, [isPending, isValid]);

  const handleSubmit = useCallback(
    async ({ content }: CreatePostCommentRequest) => {
      await onSubmit(content);
      form.reset();
    },
    [onSubmit, form],
  );

  const handleKeyDown = useCallback(
    (e: React.KeyboardEvent<HTMLTextAreaElement>) => {
      if (e.key === "Enter" && !e.shiftKey) {
        e.preventDefault();
        void form.handleSubmit(handleSubmit)();
      }
    },
    [form, handleSubmit],
  );

  return (
    <CardContent>
      <form
        onSubmit={form.handleSubmit(handleSubmit)}
        className="flex items-start gap-2"
      >
        <TextInput
          control={form.control}
          name="content"
          multiline
          rows={1}
          placeholder="Enter comment (Enter to submit, SHIFT + Enter for new line)."
          onKeyDown={handleKeyDown}
          disabled={isPending}
        />
        <ButtonGroup>
          <Button
            type="submit"
            size="default"
            disabled={isDisabled}
            aria-label="Send comment"
          >
            {isPending ? <Spinner /> : <Send />}
          </Button>
          <Button
            type="button"
            size="default"
            aria-label="Clear comment"
            onClick={() => form.reset()}
            variant="outline"
          >
            <Loader />
          </Button>
        </ButtonGroup>
      </form>
    </CardContent>
  );
}

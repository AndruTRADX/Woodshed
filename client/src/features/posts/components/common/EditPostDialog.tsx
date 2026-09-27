import { useUpdatePost } from "@/features/posts/hooks/api/usePosts";
import {
  CreatePostRequestSchema,
  type CreatePostRequest,
} from "@/features/posts/schemas/request/CreatePostRequest";
import type { PostResponse } from "@/features/posts/schemas/response/PostResponse";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@sharedUi/dialog";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { PostForm } from "@/features/posts/components/form/PostForm";

type Props = {
  post: PostResponse;
  open: boolean;
  onOpenChange: (open: boolean) => void;
};

export function EditPostDialog({ post, open, onOpenChange }: Props) {
  const { updatePostAsync, isPendingUpdatePost } = useUpdatePost(post.id);

  const form = useForm<CreatePostRequest>({
    resolver: zodResolver(CreatePostRequestSchema),
    mode: "onTouched",
    values: { content: post.content },
  });

  const onSubmit = async (data: CreatePostRequest) => {
    await updatePostAsync(data, {
      onSuccess: () => {
        onOpenChange(false);
      },
    });
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Edit post</DialogTitle>
          <DialogDescription>Update what you shared.</DialogDescription>
        </DialogHeader>

        <PostForm
          form={form}
          onSubmit={onSubmit}
          isPending={isPendingUpdatePost}
          formId="edit-post-form"
          rows={6}
        />
      </DialogContent>
    </Dialog>
  );
}

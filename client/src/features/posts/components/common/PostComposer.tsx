import UserAvatar from "@/app/layout/components/UserAvatar";
import { PostForm } from "@/features/posts/components/form/PostForm";
import { useCreatePost } from "@/features/posts/hooks/api/usePosts";
import {
  CreatePostRequestSchema,
  type CreatePostRequest,
} from "@/features/posts/schemas/request/CreatePostRequest";
import { useGetCurrentUser } from "@/shared/hooks/api/useAccount";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

export function PostComposer() {
  const { user } = useGetCurrentUser();
  const { createPostAsync, isPendingCreatePost } = useCreatePost();

  const form = useForm<CreatePostRequest>({
    resolver: zodResolver(CreatePostRequestSchema),
    mode: "onSubmit",
    defaultValues: { content: "" },
  });

  const onSubmit = async (data: CreatePostRequest) => {
    await createPostAsync(data);
  };

  return (
    <div className="flex gap-2 items-center rounded-4xl border border-border bg-card py-2.5 px-4">
      <UserAvatar
        nickname={user?.nickName ?? ""}
        imageUrl={user?.imageUrl}
        size="lg"
      />

      <div className="min-w-0 flex-1">
        <PostForm
          form={form}
          onSubmit={onSubmit}
          isPending={isPendingCreatePost}
          formId="create-post-form"
        />
      </div>
    </div>
  );
}

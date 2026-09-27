import { EditPostDialog } from "@/features/posts/components/common/EditPostDialog";
import { useDeletePost } from "@/features/posts/hooks/api/usePosts";
import type { PostResponse } from "@/features/posts/schemas/response/PostResponse";
import { useConfirmDialog } from "@/shared/hooks/useConfirmDialog";
import { Button } from "@sharedUi/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@sharedUi/dropdown-menu";
import { MoreHorizontal, Pencil, Trash2 } from "lucide-react";
import { useState } from "react";

type Props = {
  post: PostResponse;
};

export function PostActionsMenu({ post }: Props) {
  const [isEditOpen, setIsEditOpen] = useState(false);
  const { deletePostAsync, isPendingDeletePost } = useDeletePost(post.id);
  const { confirmDelete } = useConfirmDialog();

  const handleDelete = async () => {
    await confirmDelete({
      title: "Delete post",
      description: "This post will be gone for good. Are you sure?",
      onConfirm: async () => {
        await deletePostAsync();
      },
    });
  };

  return (
    <>
      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <Button
            variant="ghost"
            size="icon-sm"
            aria-label="Post options"
            disabled={isPendingDeletePost}
          >
            <MoreHorizontal className="size-4" />
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end">
          <DropdownMenuItem onClick={() => setIsEditOpen(true)}>
            <Pencil className="size-4" />
            Edit
          </DropdownMenuItem>
          <DropdownMenuItem variant="destructive" onClick={handleDelete}>
            <Trash2 className="size-4" />
            Delete
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>

      <EditPostDialog
        post={post}
        open={isEditOpen}
        onOpenChange={setIsEditOpen}
      />
    </>
  );
}

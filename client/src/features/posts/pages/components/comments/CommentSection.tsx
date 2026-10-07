import { useCallback } from "react";
import { useQueryClient } from "@tanstack/react-query";
import { Separator } from "@sharedUi/separator";
import CommentList from "./CommentList";
import CommentForm from "./CommentForm";
import type { UserResponse } from "@/shared/schemas/response/UserResponse";
import { useConfirmDialog } from "@/shared/hooks/useConfirmDialog";
import { useCommentsHub } from "@/features/posts/hooks/signalR/useCommentsHub";

interface Props {
  postId: string;
}

export default function CommentsSection({ postId }: Props) {
  const queryClient = useQueryClient();
  const currentUserId = queryClient.getQueryData<UserResponse>(["user"])?.id;
  const { confirmDelete } = useConfirmDialog();

  const {
    comments,
    isLoadingComments,
    hasMoreComments,
    loadMoreComments,
    sendCommentAsync,
    isPendingSendComment,
    deleteCommentAsync,
  } = useCommentsHub(postId);

  const handleDelete = useCallback(
    (commentId: string) => {
      confirmDelete({
        title: "Delete comment",
        description: "Are you sure you want to delete this comment?",
        onConfirm: () => deleteCommentAsync(commentId),
      });
    },
    [confirmDelete, deleteCommentAsync],
  );

  return (
    <>
      <Separator />

      <CommentList
        comments={comments}
        currentUserId={currentUserId}
        isLoadingComments={isLoadingComments}
        hasMoreComments={hasMoreComments}
        onLoadMore={() => void loadMoreComments()}
        onDelete={handleDelete}
      />

      <Separator />

      <CommentForm
        onSubmit={sendCommentAsync}
        isPending={isPendingSendComment}
      />
    </>
  );
}

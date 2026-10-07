import { CardContent } from "@sharedUi/card";
import { Button } from "@sharedUi/button";
import { Spinner } from "@sharedUi/spinner";
import {
  Empty,
  EmptyDescription,
  EmptyMedia,
  EmptyTitle,
} from "@sharedUi/empty";
import CommentItem from "./CommentItem";
import type { PostCommentResponse } from "@/features/posts/schemas/response/PostCommentResponse";
import { MessageCircle } from "lucide-react";

interface Props {
  comments: PostCommentResponse[];
  currentUserId: string | undefined;
  isLoadingComments: boolean;
  hasMoreComments: boolean;
  onLoadMore: () => void;
  onDelete: (commentId: string) => void;
}

export default function CommentList({
  comments,
  currentUserId,
  isLoadingComments,
  hasMoreComments,
  onLoadMore,
  onDelete,
}: Props) {
  if (isLoadingComments) {
    return (
      <CardContent className="flex justify-center py-6">
        <Spinner />
      </CardContent>
    );
  }

  if (comments.length === 0) {
    return (
      <Empty className="p-3">
        <EmptyMedia variant="icon">
          <MessageCircle />
        </EmptyMedia>
        <EmptyTitle>No comments yet</EmptyTitle>
        <EmptyDescription>
          Be the first to share your thoughts.
        </EmptyDescription>
      </Empty>
    );
  }

  return (
    <CardContent
      id="activity-comment-container"
      className="flex flex-col divide-y divide-border max-h-112 overflow-y-auto"
    >
      {comments.map((comment) => (
        <CommentItem
          key={comment.id}
          comment={comment}
          isOwnComment={comment.user.id === currentUserId}
          onDelete={onDelete}
        />
      ))}

      {hasMoreComments && (
        <div className="flex justify-center pt-3 first:pt-0">
          <Button variant="ghost" size="sm" onClick={onLoadMore}>
            Load earlier comments
          </Button>
        </div>
      )}
    </CardContent>
  );
}

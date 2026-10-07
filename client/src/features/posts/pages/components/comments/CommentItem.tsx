import { formatDistanceToNow } from "date-fns";
import { Button } from "@sharedUi/button";
import { Avatar, AvatarFallback, AvatarImage } from "@sharedUi/avatar";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@sharedUi/dropdown-menu";
import type { PostCommentResponse } from "@/features/posts/schemas/response/PostCommentResponse";
import { GapVertical, Trash } from "lucide-react";

interface Props {
  comment: PostCommentResponse;
  isOwnComment: boolean;
  onDelete: (commentId: string) => void;
}

export default function CommentItem({
  comment,
  isOwnComment,
  onDelete,
}: Props) {
  return (
    <div className="flex gap-3 py-4 items-center first:pt-0 last:pb-0">
      <Avatar size="lg">
        <AvatarImage
          src={
            comment?.user?.imageUrl?.replace(
              "/upload/",
              "/upload/w_30,h_30,c_fill,f_auto,dpr_2/",
            ) ?? "" // default image
          }
          alt={comment.user.nickName}
        />
        <AvatarFallback>{comment.user.nickName}</AvatarFallback>
      </Avatar>
      <div className="min-w-0 flex-1">
        <div className="flex items-baseline gap-2">
          <span className="text-base font-medium text-foreground">
            {comment.user.nickName}
          </span>
          <span className="text-xs text-muted-foreground">
            {formatDistanceToNow(new Date(comment.createdAt), {
              addSuffix: true,
            })}
          </span>
          {isOwnComment && (
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button
                  variant="ghost"
                  size="icon-sm"
                  className="ml-auto text-muted-foreground"
                  aria-label="Comment actions"
                >
                  <GapVertical />
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end">
                <DropdownMenuItem
                  variant="destructive"
                  onClick={() => onDelete(comment.id)}
                >
                  <Trash className="min-w-5" />
                  Delete
                </DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
          )}
        </div>
        <p className="mb-0.5 text-sm text-foreground/90 wrap-break-word">
          {comment.content}
        </p>
      </div>
    </div>
  );
}

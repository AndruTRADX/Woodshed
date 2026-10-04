import UserAvatar from "@/app/layout/components/UserAvatar";
import PostActions from "@/features/posts/components/common/PostKeyboard";
import { PostActionsMenu } from "@/features/posts/components/common/PostMenu";
import {
  useDeleteLikePost,
  useLikePost,
} from "@/features/posts/hooks/api/usePostLike";
import type { PostResponse } from "@/features/posts/schemas/response/PostResponse";
import { useGetCurrentUser } from "@/shared/hooks/api/useAccount";
import { Link } from "react-router";

interface Props {
  post: PostResponse;
  onOpenComments?: () => void;
}

const dateFormatter = new Intl.DateTimeFormat(undefined, {
  dateStyle: "medium",
});

export default function PostCard({ post, onOpenComments }: Props) {
  const { user } = post;
  const { user: currentUser } = useGetCurrentUser();
  const isOwner = currentUser?.id === post.userId;

  const { likePostAsync, isPendingLikePost } = useLikePost(post.id);
  const { deleteLikePostAsync, isPendingDeleteLikePost } = useDeleteLikePost(
    post.id,
  );

  const handleLike = () => {
    if (post.isLiked) {
      deleteLikePostAsync();
    } else {
      likePostAsync();
    }
  };

  return (
    <article className="mx-2 flex flex-col gap-3 rounded-xl border border-border bg-card p-4 text-card-foreground shadow-[0_4px_0_var(--border)]">
      <header className="flex items-center gap-3">
        <Link to={`/account/${post.userId}`} className="shrink-0 rounded-full bg-linear-to-br from-primary via-brass-light to-brass-dark p-0.5">
          <div className="rounded-full bg-card p-0.5">
            <UserAvatar
              nickname={user.nickName}
              imageUrl={user.imageUrl}
              size="lg"
            />
          </div>
        </Link>

        <div className="min-w-0 flex-1 leading-tight">
          <Link to={`/account/${post.userId}`} className="truncate font-semibold hover:underline">{user.nickName}</Link>
          <p className="truncate text-xs text-muted-foreground">
            {user.biography}
          </p>
        </div>

        <time
          dateTime={post.createdAt}
          className="shrink-0 text-xs text-primary"
        >
          {dateFormatter.format(new Date(post.editedAt || post.createdAt))}
          {post.hasBeenEdited && <span className="text-foreground"> / Edited</span>}
        </time>

        {isOwner && <PostActionsMenu post={post} />}
      </header>

      <div className="px-2 mt-2">
        <p className="text-sm/6 wrap-break-word whitespace-pre-wrap">
          {post.content}
        </p>
      </div>

      <PostActions
        likesCount={post.likesCount}
        commentsCount={post.commentsCount}
        isLiked={post.isLiked}
        isLikePending={isPendingLikePost || isPendingDeleteLikePost}
        onLike={handleLike}
        onOpenComments={onOpenComments}
      />
    </article>
  );
}

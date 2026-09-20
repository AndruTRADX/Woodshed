import UserAvatar from "@/app/layout/components/UserAvatar";
import PostActions from "@/features/posts/components/common/PostKeyboard";
import type { PostResponse } from "@/features/posts/schemas/response/PostResponse";

interface Props {
  post: PostResponse;
  onLike?: () => void;
  onOpenComments?: () => void;
}

const dateFormatter = new Intl.DateTimeFormat(undefined, {
  dateStyle: "medium",
});

export default function PostCard({ post, onLike, onOpenComments }: Props) {
  const { user } = post;

  return (
    <article className="flex flex-col gap-3 rounded-xl border border-border bg-card p-4 text-card-foreground shadow-[0_4px_0_var(--border)]">
      <header className="flex items-center gap-3">
        <div className="shrink-0 rounded-full bg-linear-to-br from-primary via-brass-light to-brass-dark p-0.5">
          <div className="rounded-full bg-card p-0.5">
            <UserAvatar
              nickname={user.nickName}
              imageUrl={user.imageUrl}
              size="lg"
            />
          </div>
        </div>

        <div className="min-w-0 flex-1 leading-tight">
          <p className="truncate font-semibold">{user.nickName}</p>
          <p className="truncate text-xs text-muted-foreground">
            {user.biography}
          </p>
        </div>

        <time
          dateTime={post.createdAt}
          className="shrink-0 text-xs text-primary/80 [text-shadow:0_1px_0_rgba(0,0,0,0.9)]"
        >
          {dateFormatter.format(new Date(post.createdAt))}
          {post.hasBeenEdited && " · edited"}
        </time>
      </header>

      <div className="rounded-lg border border-border bg-background px-2 py-3 mt-2">
        <p className="text-sm/6 wrap-break-word whitespace-pre-wrap">
          {post.content}
        </p>
      </div>

      <PostActions
        likesCount={post.likesCount}
        commentsCount={post.commentsCount}
        onLike={onLike}
        onOpenComments={onOpenComments}
      />
    </article>
  );
}

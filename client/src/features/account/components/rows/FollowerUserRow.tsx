import { useCallback } from "react";
import { Link } from "react-router";
import { format } from "date-fns";
import { Avatar, AvatarFallback, AvatarImage } from "@sharedUi/avatar";
import { Button } from "@sharedUi/button";
import { useGetCurrentUser } from "@/shared/hooks/api/useAccount";
import {
  useFollowAccount,
  useUnfollowAccount,
} from "@/features/account/hooks/api/useAccount";
import type { FollowerResponse } from "@/features/account/schemas/response/FollowerResponse";

interface Props {
  follower: FollowerResponse;
}

export function FollowerUserRow({ follower: { follower, followedAt } }: Props) {
  const { user: currentUser } = useGetCurrentUser();
  const isCurrentUser = currentUser?.id === follower.id;
  const followedDate = new Date(followedAt);

  const { followAccountAsync, isPendingFollowAccount } = useFollowAccount();
  const { unfollowAccountAsync, isPendingUnfollowAccount } =
    useUnfollowAccount();
  const isPending = isPendingFollowAccount || isPendingUnfollowAccount;

  const handleToggleFollow = useCallback(async () => {
    if (follower.isFollowee) {
      await unfollowAccountAsync({ targetUserId: follower.id });
    } else {
      await followAccountAsync({ targetUserId: follower.id });
    }
  }, [follower.isFollowee, follower.id, followAccountAsync, unfollowAccountAsync]);

  return (
    <div className="flex items-center justify-between gap-3">
      <Link
        to={`/account/${follower.id}`}
        className="flex min-w-0 items-center gap-3"
      >
        <Avatar size="lg">
          <AvatarImage
            src={follower.imageUrl?.replace(
              "/upload/",
              "/upload/w_40,h_40,c_fill,f_auto,dpr_2/",
            )}
            alt={follower.nickName}
          />
          <AvatarFallback>
            {follower.nickName.slice(0, 2).toUpperCase()}
          </AvatarFallback>
        </Avatar>
        <div className="flex min-w-0 flex-col">
          <span className="truncate font-medium text-foreground">
            {follower.nickName}
          </span>
          {follower.biography && (
            <span className="truncate text-xs text-muted-foreground">
              {follower.biography}
            </span>
          )}
          <time
            dateTime={followedAt}
            title={format(followedDate, "PPpp")}
            className="truncate text-xs text-muted-foreground/80"
          >
            Follower since {format(followedDate, "MMM d, yyyy")}
          </time>
        </div>
      </Link>

      {!isCurrentUser && (
        <Button
          size="sm"
          variant={follower.isFollowee ? "destructive" : "default"}
          disabled={isPending}
          onClick={handleToggleFollow}
        >
          {follower.isFollowee ? "Unfollow" : "Follow"}
        </Button>
      )}
    </div>
  );
}
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
import type { FolloweeResponse } from "@/features/account/schemas/response/FolloweeResponse";

interface Props {
  followee: FolloweeResponse;
}

export function FolloweeUserRow({ followee: { followee, followedAt } }: Props) {
  const { user: currentUser } = useGetCurrentUser();
  const isCurrentUser = currentUser?.id === followee.id;
  const followedDate = new Date(followedAt);

  const { followAccountAsync, isPendingFollowAccount } = useFollowAccount();
  const { unfollowAccountAsync, isPendingUnfollowAccount } =
    useUnfollowAccount();
  const isPending = isPendingFollowAccount || isPendingUnfollowAccount;

  // isFollowee = the current user follows this account
  const handleToggleFollow = useCallback(async () => {
    if (followee.isFollowee) {
      await unfollowAccountAsync({ targetUserId: followee.id });
    } else {
      await followAccountAsync({ targetUserId: followee.id });
    }
  }, [
    followee.isFollowee,
    followee.id,
    followAccountAsync,
    unfollowAccountAsync,
  ]);

  return (
    <div className="flex items-center justify-between gap-3">
      <Link
        to={`/account/${followee.id}`}
        className="flex min-w-0 items-center gap-3"
      >
        <Avatar size="lg">
          <AvatarImage
            src={followee.imageUrl?.replace(
              "/upload/",
              "/upload/w_40,h_40,c_fill,f_auto,dpr_2/",
            )}
            alt={followee.nickName}
          />
          <AvatarFallback>
            {followee.nickName.slice(0, 2).toUpperCase()}
          </AvatarFallback>
        </Avatar>
        <div className="flex min-w-0 flex-col">
          <span className="truncate font-medium text-foreground">
            {followee.nickName}
          </span>
          {followee.biography && (
            <span className="truncate text-xs text-muted-foreground">
              {followee.biography}
            </span>
          )}
          <time
            dateTime={followedAt}
            title={format(followedDate, "PPpp")}
            className="truncate text-xs text-muted-foreground/80"
          >
            Following since {format(followedDate, "MMM d, yyyy")}
          </time>
        </div>
      </Link>

      {!isCurrentUser && (
        <Button
          size="sm"
          variant={followee.isFollowee ? "destructive" : "default"}
          disabled={isPending}
          onClick={handleToggleFollow}
        >
          {followee.isFollowee ? "Unfollow" : "Follow"}
        </Button>
      )}
    </div>
  );
}

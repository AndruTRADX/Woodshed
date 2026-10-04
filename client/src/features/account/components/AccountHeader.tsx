import { useCallback, useMemo } from "react";
import { useQueryClient } from "@tanstack/react-query";
import { Button } from "@sharedUi/button";
import type { UserAccountResponse } from "@/shared/schemas/response/UserAccountResponse";
import type { UserResponse } from "@/shared/schemas/response/UserResponse";
import {
  useFollowAccount,
  useUnfollowAccount,
} from "@/features/account/hooks/api/useAccount";
import { useLiquidGlass } from "@/shared/hooks/useLiquidGlass";
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
} from "@/shared/components/ui/avatar";

interface Props {
  account: UserAccountResponse;
}

export default function AccountHeader({ account }: Props) {
  const queryClient = useQueryClient();

  const isCurrentUser = useMemo(() => {
    return account.id === queryClient.getQueryData<UserResponse>(["user"])?.id;
  }, [account.id, queryClient]);

  const { followAccountAsync, isPendingFollowAccount } = useFollowAccount();
  const { unfollowAccountAsync, isPendingUnfollowAccount } =
    useUnfollowAccount();

  const handleToggleFollow = useCallback(async () => {
    if (account.isFollowee) {
      await unfollowAccountAsync({ targetUserId: account.id });
    } else {
      await followAccountAsync({ targetUserId: account.id });
    }
  }, [account.isFollowee, account.id, followAccountAsync, unfollowAccountAsync]);

  const { ref: glassRef, style: glassStyle } = useLiquidGlass<HTMLDivElement>();

  return (
    <div className="w-full flex flex-col">
      <div
        id="gradient-account"
        className="bg-account-header h-28 sm:h-36"
      ></div>

      <div className="-mt-8 flex flex-col sm:flex-row sm:justify-between sm:items-center gap-4 px-4">
        <div className="flex flex-col sm:flex-row sm:items-center gap-4">
          <div
            ref={glassRef}
            style={glassStyle}
            className="w-fit p-3.25 -translate-y-8 rounded-2xl bg-background/40"
          >
            <Avatar className="w-32 h-32 sm:w-40 sm:h-40">
              <AvatarImage
                src={
                  account?.imageUrl?.replace(
                    "/upload/",
                    "/upload/w_280,h_280,c_fill,f_auto,dpr_2/",
                  ) ?? ""
                }
                alt={account.nickName}
                className="rounded-xl"
              />
              <AvatarFallback>{account.nickName}</AvatarFallback>
            </Avatar>
          </div>

          <div className="flex flex-col gap-2 max-w-full sm:max-w-88 sm:mb-2">
            <div className="flex flex-col gap-1">
              <h2 className="font-semibold text-foreground text-lg">
                {account.nickName}
              </h2>
            </div>
            {!isCurrentUser && (
              <Button
                size="sm"
                variant={account.isFollowee ? "destructive" : "default"}
                disabled={isPendingFollowAccount || isPendingUnfollowAccount}
                onClick={handleToggleFollow}
              >
                {account.isFollowee ? "Unfollow" : "Follow"}
              </Button>
            )}
          </div>
        </div>

        <div className="flex gap-6 items-center sm:mb-2">
          <div className="flex flex-col items-center">
            <p className="text-muted-foreground text-xs">Followers</p>
            <h1 className="text-3xl text-foreground">
              {account.followersCount}
            </h1>
          </div>
          <div className="flex flex-col items-center">
            <p className="text-muted-foreground text-xs">Following</p>
            <h1 className="text-3xl text-foreground">
              {account.followingsCount}
            </h1>
          </div>
        </div>
      </div>
    </div>
  );
}

import { useParams } from "react-router";
import { ErrorShow } from "@/shared/components/common/ErrorShow";
import { PaginationControl } from "@/shared/components/common/PaginationControl";
import { usePagedParams } from "@/shared/hooks/usePagedParams";
import { Card, CardContent, CardHeader, CardTitle } from "@sharedUi/card";
import { Separator } from "@sharedUi/separator";
import { SkeletonFollowersCard } from "@/features/account/components/cards/components/SkeletonFollowersCard";
import { useGetPagedFollowers } from "@/features/account/hooks/api/useAccount";
import { NoContent } from "@/shared/components/common/NoContent";
import { FollowerUserRow } from "@/features/account/components/rows/FollowerUserRow";

export default function AccountFollowersCard() {
  const { id } = useParams();
  const { pageIndex, pageSize, setPageIndex } = usePagedParams("followers");

  const { pagedFollowers, isLoadingPagedFollowers, errorPagedFollowers } =
    useGetPagedFollowers(id, { pageIndex, pageSize });

  const followers = pagedFollowers?.data ?? [];

  if (isLoadingPagedFollowers) {
    return <SkeletonFollowersCard />;
  }

  if (errorPagedFollowers) {
    return <ErrorShow error={errorPagedFollowers} />;
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle>Followers</CardTitle>
      </CardHeader>
      <CardContent className="flex flex-col gap-4">
        <Separator />

        {followers.length === 0 ? (
          <NoContent title="No followers" description="No followers yet" />
        ) : (
          <div className="flex flex-col gap-4">
            {followers.map((follower) => (
              <FollowerUserRow key={follower.follower.id} follower={follower} />
            ))}
          </div>
        )}

        <PaginationControl
          pageIndex={pagedFollowers?.pageIndex ?? pageIndex}
          pageCount={pagedFollowers?.pageCount ?? 1}
          onPageChange={setPageIndex}
        />
      </CardContent>
    </Card>
  );
}

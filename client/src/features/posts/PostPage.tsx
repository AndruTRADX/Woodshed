import PostCard from "@/features/posts/components/cards/PostCard";
import SkeletonPage from "@/features/posts/components/SkeletonPage";
import { useGetPosts } from "@/features/posts/hooks/api/usePosts";
import { ErrorShow } from "@/shared/components/common/ErrorShow";
import { NoContent } from "@/shared/components/common/NoContent";
import { PaginationControl } from "@/shared/components/common/PaginationControl";
import { usePagedParams } from "@/shared/hooks/usePagedParams";

export default function PostPage() {
  const { pageIndex, pageSize, sort, setPageIndex } = usePagedParams(
    "posts",
    10,
  );

  const { pagedPosts, isLoadingPosts, errorPosts } = useGetPosts({
    pageIndex,
    pageSize,
    sort,
  });

  const posts = pagedPosts?.data ?? [];

  if (isLoadingPosts) {
    return <SkeletonPage />;
  }

  if (errorPosts) {
    return <ErrorShow error={errorPosts} />;
  }

  return (
    <div className="grid grid-cols-1 gap-6">
      <div className="flex flex-col lg:col-span-3 gap-y-8">
        {posts.length === 0 ? (
          <div className="lg:col-span-3">
            <NoContent
              title="No posts"
              description="No posts have been created :("
            />
          </div>
        ) : (
          posts.map((post) => <PostCard post={post} key={`post-card-${post.id}`} />)
        )}

        <PaginationControl
          pageIndex={pagedPosts?.pageIndex ?? pageIndex}
          pageCount={pagedPosts?.pageCount ?? 1}
          onPageChange={setPageIndex}
        />
      </div>
    </div>
  );
}

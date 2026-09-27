import PostCard from "@/features/posts/components/cards/PostCard";
import SkeletonPage from "@/features/posts/components/SkeletonPage";
import { useGetPostById } from "@/features/posts/hooks/api/usePosts";
import { ErrorShow } from "@/shared/components/common/ErrorShow";
import { Button } from "@sharedUi/button";
import { ArrowLeft } from "lucide-react";
import { Link, useParams } from "react-router";

export default function PostDetailPage() {
  const { id } = useParams<{ id: string }>();
  const { post, isLoadingPost, errorPost } = useGetPostById(id ?? "");

  if (isLoadingPost) {
    return <SkeletonPage />;
  }

  if (errorPost || !post) {
    return <ErrorShow error={errorPost} />;
  }

  return (
    <div className="flex flex-col gap-4">
      <Button variant="ghost" size="sm" className="w-fit" asChild>
        <Link to="/posts">
          <ArrowLeft className="size-4" />
          Back to feed
        </Link>
      </Button>

      <PostCard post={post} linkToDetail={false} />
    </div>
  );
}

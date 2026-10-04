import { ErrorShow } from "@/shared/components/common/ErrorShow";
import { PaginationControl } from "@/shared/components/common/PaginationControl";
import { usePagedParams } from "@/shared/hooks/usePagedParams";
import { Card, CardContent, CardHeader, CardTitle } from "@sharedUi/card";
import { useParams } from "react-router";
import { Button } from "@/shared/components/ui/button";
import { useCallback, useState } from "react";
import { Separator } from "@/shared/components/ui/separator";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@sharedUi/dropdown-menu";
import { useConfirmDialog } from "@/shared/hooks/useConfirmDialog";
import {
  useDeletePhotoAccount,
  useGetAccountPhotosById,
  useSetMainPhotoAccount,
} from "@/features/account/hooks/api/useAccount";
import type { PhotoResponse } from "@/features/account/schemas/response/PhotoResponse";
import { toast } from "@/shared/stores/toastStore";
import { SkeletonPhotosCard } from "@/features/account/components/cards/components/SkeletonPhotosCard";
import { Ellipsis, Plus, Star, Trash, X } from "lucide-react";
import { NoContent } from "@/shared/components/common/NoContent";
import SubmitPhotoForm from "@/features/account/forms/SubmitPhotoForm";

export default function AccountPhotosCard() {
  const { id } = useParams();
  const { pageIndex, pageSize, setPageIndex } = usePagedParams("photos");
  const [editMode, setEditMode] = useState(false);
  const { confirmDelete } = useConfirmDialog();

  const { pagedPhotos, isLoadingPagedPhotos, errorPagedPhotos, isCurrentUser } =
    useGetAccountPhotosById(id, {
      pageIndex,
      pageSize,
    });

  const { setMainPhotoAsync, isPendingSetMainPhoto } = useSetMainPhotoAccount();
  const { deletePhotoAsync, isPendingDeletePhoto } = useDeletePhotoAccount();

  const photos = pagedPhotos?.data ?? [];

  const handleSetMainPhoto = useCallback(
    async (photo: PhotoResponse) => {
      await setMainPhotoAsync(photo, {
        onSuccess: () => toast.add({ title: "Main photo updated" }),
      });
    },
    [setMainPhotoAsync],
  );

  const handleDeletePhoto = useCallback(
    (photo: PhotoResponse) => {
      confirmDelete({
        title: "Delete photo",
        description:
          "Are you sure you want to delete this photo? This action cannot be undone.",
        onConfirm: async () => {
          await deletePhotoAsync(photo, {
            onSuccess: () => toast.add({ title: "Photo deleted" }),
          });
        },
      });
    },
    [confirmDelete, deletePhotoAsync],
  );

  if (isLoadingPagedPhotos) {
    return <SkeletonPhotosCard />;
  }

  if (errorPagedPhotos) {
    return <ErrorShow error={errorPagedPhotos} />;
  }

  return (
    <Card>
      <CardHeader className="flex justify-between items-center">
        <CardTitle>Photos</CardTitle>
        {isCurrentUser && (
          <Button
            variant={editMode ? "destructive" : "default"}
            onClick={() => setEditMode((prev) => !prev)}
          >
            {editMode ? (
              <>
                <X /> Cancel
              </>
            ) : (
              <>
                <Plus /> Add photo
              </>
            )}
          </Button>
        )}
      </CardHeader>
      <CardContent className="flex flex-col gap-6">
        <Separator />

        {editMode ? (
          <SubmitPhotoForm onSuccess={() => setEditMode(false)} />
        ) : photos.length === 0 ? (
          <NoContent
            title="No photos found"
            description="This user has not yet uploaded any photos"
          />
        ) : (
          <div className="grid md:grid-cols-4 gap-4">
            {photos.map((photo) => (
              <div
                key={photo.publicId}
                className="relative aspect-square overflow-hidden rounded-lg border border-border cursor-pointer"
              >
                <img
                  src={photo.url.replace(
                    "/upload/",
                    "/upload/w_280,h_280,c_fill,f_auto,dpr_2/",
                  )}
                  alt="user photo"
                  loading="lazy"
                  className="size-full object-cover transition-transform duration-300 ease-out hover:scale-115 hover:rotate-3"
                />

                {isCurrentUser && (
                  <DropdownMenu>
                    <DropdownMenuTrigger asChild>
                      <Button
                        variant="default"
                        size="icon-sm"
                        onClick={(e) => e.stopPropagation()}
                        disabled={isPendingSetMainPhoto || isPendingDeletePhoto}
                        className="absolute top-2 right-2"
                        aria-label="Photo options"
                      >
                        <Ellipsis />
                      </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                      <DropdownMenuItem
                        onClick={() => handleSetMainPhoto(photo)}
                      >
                        <Star className="min-w-5" />
                        Set as main
                      </DropdownMenuItem>
                      <DropdownMenuItem
                        variant="destructive"
                        onClick={() => handleDeletePhoto(photo)}
                      >
                        <Trash className="min-w-5" />
                        Delete
                      </DropdownMenuItem>
                    </DropdownMenuContent>
                  </DropdownMenu>
                )}
              </div>
            ))}
          </div>
        )}

        <PaginationControl
          pageIndex={pagedPhotos?.pageIndex ?? pageIndex}
          pageCount={pagedPhotos?.pageCount ?? 1}
          onPageChange={setPageIndex}
        />
      </CardContent>
    </Card>
  );
}

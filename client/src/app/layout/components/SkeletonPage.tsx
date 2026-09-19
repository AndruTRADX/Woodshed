import { Card } from "@/shared/components/ui/card";
import { Separator } from "@/shared/components/ui/separator";
import {
  Sidebar,
  SidebarInset,
  SidebarProvider,
  SidebarTrigger,
} from "@/shared/components/ui/sidebar";
import { Skeleton } from "@/shared/components/ui/skeleton";

export default function SkeletonPage() {
  return (
    <SidebarProvider>
      <Sidebar></Sidebar>
      <SidebarInset>
        <header className="flex h-16 shrink-0 items-center gap-2 transition-[width,height] ease-linear group-has-data-[collapsible=icon]/sidebar-wrapper:h-12">
          <div className="flex items-center gap-2 px-4">
            <SidebarTrigger className="-ml-1" />
            <Separator
              orientation="vertical"
              className="mr-2 data-[orientation=vertical]:h-4"
            />
          </div>
        </header>
        <main
          id="woodshed-main-container"
          className="flex flex-1 flex-col gap-4 p-4 pt-0"
        >
          <Skeleton className="h-12 w-full" />

          <Card className="p-4">
            <Skeleton className="h-12 w-full" />
            <Skeleton className="h-6 w-3/6" />
            <Skeleton className="h-6 w-2/3" />
            <Skeleton className="h-6 w-2/6" />
            <Skeleton className="h-6 w-2/3" />
          </Card>
          <Card className="p-4">
            <Skeleton className="h-12 w-full" />
            <Skeleton className="h-6 w-3/6" />
            <Skeleton className="h-6 w-2/3" />
            <Skeleton className="h-6 w-2/6" />
            <Skeleton className="h-6 w-2/3" />
          </Card>
          <Card className="p-4">
            <Skeleton className="h-12 w-full" />
            <Skeleton className="h-6 w-3/6" />
            <Skeleton className="h-6 w-2/3" />
            <Skeleton className="h-6 w-2/6" />
            <Skeleton className="h-6 w-2/3" />
          </Card>
        </main>
      </SidebarInset>
    </SidebarProvider>
  );
}

import SkeletonPage from "@/app/layout/components/SkeletonPage";
import { AppSidebar } from "@/shared/components/AppSidebar";
import { ErrorShow } from "@/shared/components/common/ErrorShow";
import { NoContent } from "@/shared/components/common/NoContent";
import { Separator } from "@/shared/components/ui/separator";
import {
  SidebarInset,
  SidebarProvider,
  SidebarTrigger,
} from "@/shared/components/ui/sidebar";
import { useGetCurrentUser } from "@/shared/hooks/api/useAccount";
import { Outlet } from "react-router";

export default function App() {
  var { user, errorUser, isLoadingUser } = useGetCurrentUser();

  if (isLoadingUser) {
    return <SkeletonPage />;
  }

  if (errorUser) {
    return <ErrorShow error={errorUser} />;
  }

  if (!user) {
    return <NoContent title="Unauthorized" description={`Please log in`} />;
  }

  return (
    <SidebarProvider>
      <AppSidebar user={user} />
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
          <Outlet />
        </main>
      </SidebarInset>
    </SidebarProvider>
  );
}

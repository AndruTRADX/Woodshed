import SkeletonPage from "@/app/layout/components/SkeletonPage";
import { AppSidebar } from "@/shared/components/AppSidebar";
import { ErrorShow } from "@/shared/components/common/ErrorShow";
import { NoContent } from "@/shared/components/common/NoContent";
import { Button } from "@/shared/components/ui/button";
import { Kbd, KbdGroup } from "@/shared/components/ui/kbd";
import { Separator } from "@/shared/components/ui/separator";
import {
  SidebarInset,
  SidebarProvider,
  SidebarTrigger,
} from "@/shared/components/ui/sidebar";
import { useGetCurrentUser } from "@/shared/hooks/api/useAccount";
import { useIsMobile } from "@/shared/hooks/use-mobile";
import { useLiquidGlass } from "@/shared/hooks/useLiquidGlass";
import { FolderGit2 } from "lucide-react";
import { Outlet } from "react-router";

const GITHUB_URL = "https://github.com/AndruTRADX/Woodshed";

export default function App() {
  const { user, errorUser, isLoadingUser } = useGetCurrentUser();
  const isMobile = useIsMobile();
  const { ref: glassRef, style: glassStyle } = useLiquidGlass<HTMLElement>({ border: false,  });

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
        <header
          ref={glassRef}
          style={glassStyle}
          className="sticky top-0 z-10 flex h-16 shrink-0 items-center justify-between gap-2 bg-background/40 backdrop-blur-md transition-[width,height] ease-linear group-has-data-[collapsible=icon]/sidebar-wrapper:h-12"
        >
          <div className="flex items-center gap-2 px-4">
            <SidebarTrigger className="-ml-1" />
            {!isMobile && (
              <KbdGroup>
                <Kbd>Ctrl</Kbd>
                <span>+</span>
                <Kbd>B</Kbd>
              </KbdGroup>
            )}
            <Separator
              orientation="vertical"
              className="mr-2 data-[orientation=vertical]:h-4"
            />
          </div>
          <div className="flex items-center gap-2 px-4">
            <Button
              variant="ghost"
              size="lg"
              onClick={() => window.open(GITHUB_URL, "_blank")}
            >
              <FolderGit2 />
              GitHub
            </Button>
          </div>
        </header>
        <div
          id="woodshed-main-container"
          className="flex items-center flex-1 flex-col gap-4 px-4 pt-2 pb-8"
        >
          <main className="w-full max-w-5xl">
            <Outlet />
          </main>
        </div>
      </SidebarInset>
    </SidebarProvider>
  );
}

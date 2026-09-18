import { ThemeProvider } from "@/app/layout/ThemeProvider";
import { Toaster } from "@sharedUi/toast";
import { Outlet } from "react-router";

export default function App() {

  return (
    <ThemeProvider defaultTheme="system" storageKey="woodshed-ui-theme">
      <div>
        {/* <ConfirmDialog /> */}
        {/* <ScrollRestoration /> */}
        {/* <Toaster richColors /> */}
        <Toaster />
        <main
          id="woodshed-main-container"
          className=""
        >
          <Outlet />
        </main>
      </div>
    </ThemeProvider>
  )
}

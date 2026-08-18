import { ThemeProvider } from "@/app/layout/ThemeProvider";
import { Outlet } from "react-router";

export default function App() {

  return (
    <ThemeProvider defaultTheme="system" storageKey="reactivities-ui-theme">
      <div>
        {/* <ConfirmDialog /> */}
        {/* <ScrollRestoration /> */}
        {/* <Toaster richColors /> */}
        <main
          id=""
          className=""
        >
          <Outlet />
        </main>
      </div>
    </ThemeProvider>
  )
}

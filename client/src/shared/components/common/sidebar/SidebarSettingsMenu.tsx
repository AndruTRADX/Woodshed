import { useTheme, type Theme } from "@/app/layout/ThemeProvider";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuRadioGroup,
  DropdownMenuRadioItem,
  DropdownMenuTrigger,
} from "@/shared/components/ui/dropdown-menu";
import {
  SidebarMenuButton,
  SidebarMenuItem,
  useSidebar,
} from "@/shared/components/ui/sidebar";
import { THEME_OPTIONS } from "@/shared/constants/components/AppSidebar";
import { useLogoutAccount } from "@/shared/hooks/api/useAccount";
import { Settings, UserMinus } from "lucide-react";
import { useCallback } from "react";

export default function SidebarSettingsMenu() {
  const { theme, setTheme } = useTheme();
  const { isMobile } = useSidebar();
  const { logoutAccountAsync } = useLogoutAccount();

  const handleLogout = useCallback(async () => {
    await logoutAccountAsync();
  }, [logoutAccountAsync]);

  return (
    <SidebarMenuItem>
      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <SidebarMenuButton>
            <Settings />
            <span>Settings</span>
          </SidebarMenuButton>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="start" side={isMobile ? "bottom" : "right"}>
          <DropdownMenuRadioGroup
            value={theme}
            onValueChange={(value) => setTheme(value as Theme)}
          >
            <DropdownMenuLabel>Theme</DropdownMenuLabel>
            {THEME_OPTIONS.map(({ value, label, icon: Icon }) => (
              <DropdownMenuRadioItem key={value} value={value}>
                <Icon className="min-w-4" />
                {label}
              </DropdownMenuRadioItem>
            ))}
          </DropdownMenuRadioGroup>
          <DropdownMenuRadioGroup
            value={theme}
            onValueChange={(value) => setTheme(value as Theme)}
          >
            <DropdownMenuLabel>Account</DropdownMenuLabel>
            <DropdownMenuItem onClick={handleLogout}>
              <UserMinus />
              Log out
            </DropdownMenuItem>
          </DropdownMenuRadioGroup>
        </DropdownMenuContent>
      </DropdownMenu>
    </SidebarMenuItem>
  );
}

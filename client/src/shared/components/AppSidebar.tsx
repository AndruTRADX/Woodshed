import UserAvatar from "@/app/layout/components/UserAvatar";
import { useTheme, type Theme } from "@/app/layout/ThemeProvider";
import GlassItem from "@/shared/components/ui/glass-item";
import {
  SIDEBAR_BLOCKS,
  THEME_OPTIONS,
} from "@/shared/constants/components/AppSidebar";
import type { UserResponse } from "@/shared/schemas/response/UserResponse";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuLabel,
  DropdownMenuRadioGroup,
  DropdownMenuRadioItem,
  DropdownMenuTrigger,
} from "@sharedUi/dropdown-menu";
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupLabel,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarMenuSub,
  SidebarMenuSubButton,
  SidebarMenuSubItem,
  SidebarRail,
  useSidebar,
} from "@sharedUi/sidebar";
import { Piano, Settings } from "lucide-react";
import { Link } from "react-router";

type Props = {
  user: UserResponse;
};

export function AppSidebar({ user }: Props) {
  const { theme, setTheme } = useTheme();
  const { isMobile } = useSidebar();

  return (
    <Sidebar variant="floating">
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton size="lg">
              <div className="flex aspect-square size-8 items-center justify-center rounded-lg bg-sidebar-primary text-sidebar-primary-foreground">
                <Piano className="size-4" />
              </div>
              <div className="flex flex-col gap-0.5 leading-none">
                <span className="font-medium text-xl">Woodshed</span>
              </div>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        {SIDEBAR_BLOCKS.map((block) => (
          <SidebarGroup key={block.title}>
            <SidebarGroupLabel>{block.title}</SidebarGroupLabel>
            <SidebarMenu>
              {block.items.map((item) => (
                <SidebarMenuItem key={item.id}>
                  <SidebarMenuButton>
                    <Link
                      to={item.link}
                      className="font-medium flex items-center gap-2"
                    >
                      <item.icon />
                      {item.name}
                    </Link>
                  </SidebarMenuButton>
                  {item.subitems && item.subitems.length > 0 && (
                    <SidebarMenuSub>
                      {item.subitems.map((subitem) => (
                        <SidebarMenuSubItem key={subitem.id}>
                          <SidebarMenuSubButton isActive={false}>
                            <Link
                              to={subitem.link}
                              className="font-medium flex items-center gap-2"
                            >
                              <subitem.icon />
                              {subitem.name}
                            </Link>
                          </SidebarMenuSubButton>
                        </SidebarMenuSubItem>
                      ))}
                    </SidebarMenuSub>
                  )}
                </SidebarMenuItem>
              ))}
            </SidebarMenu>
          </SidebarGroup>
        ))}
      </SidebarContent>
      <SidebarFooter>
        <SidebarMenu>
          <SidebarMenuItem>
            <DropdownMenu>
              <DropdownMenuTrigger
                render={
                  <SidebarMenuButton>
                    <Settings />
                    Settings
                  </SidebarMenuButton>
                }
              />
              <DropdownMenuContent
                align="start"
                side={isMobile ? "bottom" : "right"}
              >
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
              </DropdownMenuContent>
            </DropdownMenu>
          </SidebarMenuItem>
        </SidebarMenu>
        <GlassItem className="items-center gap-3 rounded-full">
          <UserAvatar
            nickname={user.nickName}
            imageUrl={user.imageUrl}
            size="lg"
          />
          <div className="flex min-w-0 flex-col leading-tight">
            <span className="truncate text-sm font-medium">{user.name}</span>
            <span className="truncate text-xs font-medium text-primary">
              @{user.nickName}
            </span>
          </div>
        </GlassItem>
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  );
}
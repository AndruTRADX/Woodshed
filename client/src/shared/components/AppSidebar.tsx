import UserAvatar from "@/app/layout/components/UserAvatar";
import GlassItem from "@/shared/components/ui/glass-item";
import { sidebarItems } from "@/shared/constants/components/AppSidebar";
import type { UserResponse } from "@/shared/schemas/response/UserResponse";
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarMenuSub,
  SidebarMenuSubButton,
  SidebarMenuSubItem,
  SidebarRail,
} from "@sharedUi/sidebar";
import { Piano } from "lucide-react";
import { Link } from "react-router";

type Props = {
  user: UserResponse;
};

export function AppSidebar({ user }: Props) {
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
        <SidebarGroup>
          <SidebarMenu>
            {sidebarItems.map((item, ii) => (
              <SidebarMenuItem key={`sidebar-item-${ii}-${item.id}`}>
                <SidebarMenuButton>
                  <Link
                    to={item.link}
                    className="font-medium flex items-center gap-2"
                  >
                    <item.icon />
                    {item.name}
                  </Link>
                </SidebarMenuButton>
                {item.subitems &&
                  item.subitems.length > 0 &&
                  item.subitems.map((subitem, ij) => (
                    <SidebarMenuSub>
                      <SidebarMenuSubItem
                        key={`sidebar-item-${ij}-${subitem.id}`}
                      >
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
                    </SidebarMenuSub>
                  ))}
              </SidebarMenuItem>
            ))}
          </SidebarMenu>
        </SidebarGroup>
      </SidebarContent>
      <SidebarFooter>
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

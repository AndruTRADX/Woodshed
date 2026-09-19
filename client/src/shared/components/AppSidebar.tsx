import UserAvatar from "@/app/layout/components/UserAvatar";
import GlassItem from "@/shared/components/ui/glass-item";
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
          <p className="text-sm text-">
            {user.name} - {user.nickName}
          </p>
        </SidebarGroup>
        <SidebarGroup>
          <SidebarMenu>
            <SidebarMenuItem key="item-1">
              <SidebarMenuButton>
                <a href="#" className="font-medium">
                  Item 1
                </a>
              </SidebarMenuButton>
              <SidebarMenuSub>
                <SidebarMenuSubItem key="sub-item-1">
                  <SidebarMenuSubButton isActive={false}>
                    <a href="#">sub item 1</a>
                  </SidebarMenuSubButton>
                </SidebarMenuSubItem>
              </SidebarMenuSub>
            </SidebarMenuItem>
          </SidebarMenu>
        </SidebarGroup>
      </SidebarContent>
      <SidebarFooter>
        <GlassItem>
          <UserAvatar nickname={user.nickName} imageUrl={user.imageUrl} />
        </GlassItem>
      </SidebarFooter>
      <SidebarRail />
    </Sidebar>
  );
}

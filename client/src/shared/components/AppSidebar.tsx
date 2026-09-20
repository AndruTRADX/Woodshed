import UserAvatar from "@/app/layout/components/UserAvatar";
import SidebarNavItem from "@/shared/components/common/sidebar/SidebarNavItem";
import SidebarSettingsMenu from "@/shared/components/common/sidebar/SidebarSettingsMenu";
import GlassItem from "@/shared/components/ui/glass-item";
import { Separator } from "@/shared/components/ui/separator";
import {
  SIDEBAR_BLOCKS,
  SIDEBAR_FOOTER_ITEMS,
} from "@/shared/constants/components/AppSidebar";
import type { UserResponse } from "@/shared/schemas/response/UserResponse";
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
        {SIDEBAR_BLOCKS.map((block) => (
          <SidebarGroup key={block.title}>
            <SidebarGroupLabel>{block.title}</SidebarGroupLabel>
            <SidebarMenu>
              {block.items.map((item) => (
                <SidebarNavItem key={item.id} item={item} />
              ))}
            </SidebarMenu>
          </SidebarGroup>
        ))}
      </SidebarContent>
      <SidebarFooter>
        <SidebarMenu>
          <SidebarSettingsMenu />
          {SIDEBAR_FOOTER_ITEMS.map((item) => (
            <SidebarNavItem key={item.id} item={item} />
          ))}
        </SidebarMenu>
        <Separator />
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

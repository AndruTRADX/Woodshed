import SidebarLink from "@/shared/components/common/sidebar/SidebarLink";
import {
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarMenuSub,
  SidebarMenuSubButton,
  SidebarMenuSubItem,
} from "@/shared/components/ui/sidebar";
import type { SIDEBAR_ITEM_TYPE } from "@/shared/constants/components/AppSidebar";
import { useAudio } from "@/shared/hooks/useAudio";

const ACTIVE_STYLES =
  "aria-[current=page]:bg-foreground aria-[current=page]:text-background " +
  "aria-[current=page]:hover:bg-foreground aria-[current=page]:hover:text-background " +
  "aria-[current=page]:active:bg-foreground aria-[current=page]:active:text-background";

const ACTIVE_SUB_STYLES = `${ACTIVE_STYLES} aria-[current=page]:[&>svg]:text-background`;

export default function SidebarNavItem({ item }: { item: SIDEBAR_ITEM_TYPE }) {
  const hasSubitems = !!item.subitems && item.subitems.length > 0;
  const { play } = useAudio();

  return (
    <SidebarMenuItem>
      <SidebarMenuButton
        asChild
        className={`font-medium ${ACTIVE_STYLES} transition-colors duration-150 ease-out`}
        onClick={() => {
          if (item.sound) play(item.sound);
        }}
      >
        <SidebarLink item={item} end={hasSubitems} />
      </SidebarMenuButton>
      {hasSubitems && (
        <SidebarMenuSub>
          {item.subitems!.map((subitem) => (
            <SidebarMenuSubItem key={subitem.id}>
              <SidebarMenuSubButton
                asChild
                className={`font-medium ${ACTIVE_SUB_STYLES}`}
                onClick={() => {
                  if (item.sound) play(item.sound);
                }}
              >
                <SidebarLink item={subitem} />
              </SidebarMenuSubButton>
            </SidebarMenuSubItem>
          ))}
        </SidebarMenuSub>
      )}
    </SidebarMenuItem>
  );
}

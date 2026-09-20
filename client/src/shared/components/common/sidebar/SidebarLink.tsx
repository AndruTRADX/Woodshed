import type { ComponentProps } from "react";
import { NavLink } from "react-router";
import type { SIDEBAR_LINK_TYPE } from "@/shared/constants/components/AppSidebar";

type Props = {
  item: SIDEBAR_LINK_TYPE;
  end?: boolean;
} & Omit<ComponentProps<"a">, "href">;

export default function SidebarLink({ item, end, ...props }: Props) {
  const content = (
    <>
      <item.icon />
      <span>{item.name}</span>
    </>
  );

  if (item.external) {
    return (
      <a href={item.link} target="_blank" rel="noopener noreferrer" {...props}>
        {content}
      </a>
    );
  }

  return (
    <NavLink to={item.link} end={end} {...props}>
      {content}
    </NavLink>
  );
}

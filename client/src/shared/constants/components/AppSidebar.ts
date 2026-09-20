import {
  FolderGit2,
  Heart,
  Laptop,
  ListMusic,
  MessageCircle,
  Moon,
  Sun,
  User,
  type LucideIcon,
} from "lucide-react";
import type { Theme } from "@/app/layout/ThemeProvider";

type THEME_OPTION_TYPE = {
  value: Theme;
  label: string;
  icon: LucideIcon;
};

export type SIDEBAR_LINK_TYPE = {
  id: string;
  link: string;
  name: string;
  icon: LucideIcon;
  external?: boolean;
};

export type SIDEBAR_ITEM_TYPE = SIDEBAR_LINK_TYPE & {
  subitems?: SIDEBAR_LINK_TYPE[];
};

type SIDEBAR_BLOCK_TYPE = {
  title: string;
  items: SIDEBAR_ITEM_TYPE[];
};

export const THEME_OPTIONS: THEME_OPTION_TYPE[] = [
  { value: "light", label: "Light", icon: Sun },
  { value: "dark", label: "Dark", icon: Moon },
  { value: "system", label: "System", icon: Laptop },
];

export const SIDEBAR_BLOCKS: SIDEBAR_BLOCK_TYPE[] = [
  {
    title: "My content",
    items: [
      { id: "MyAccount", link: "/account", name: "My Account", icon: User },
      { id: "Posts", link: "/posts", name: "Posts", icon: ListMusic },
      {
        id: "Messages",
        link: "/messages",
        name: "Messages",
        icon: MessageCircle,
      },
    ],
  },
];

export const SIDEBAR_FOOTER_ITEMS: SIDEBAR_ITEM_TYPE[] = [
  {
    id: "Repository",
    link: "https://github.com/AndruTRADX/Woodshed",
    name: "Repository",
    icon: FolderGit2,
    external: true,
  },
  {
    id: "Support",
    link: "https://github.com/AndruTRADX/Woodshed",
    name: "Support",
    icon: Heart,
    external: true,
  },
];

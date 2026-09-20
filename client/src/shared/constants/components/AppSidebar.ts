import {
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

export const THEME_OPTIONS: THEME_OPTION_TYPE[] = [
  { value: "light", label: "Light", icon: Sun },
  { value: "dark", label: "Dark", icon: Moon },
  { value: "system", label: "System", icon: Laptop },
];

type SIDEBAR_SUB_ITEM_TYPE = {
  id: string;
  link: string;
  name: string;
  icon: LucideIcon;
};

type SIDEBAR_ITEM_TYPE = {
  id: string;
  link: string;
  name: string;
  icon: LucideIcon;
  subitems?: SIDEBAR_SUB_ITEM_TYPE[];
};

type SIDEBAR_BLOCK_TYPE = {
  title: string;
  items: SIDEBAR_ITEM_TYPE[];
};

export const SIDEBAR_BLOCKS: SIDEBAR_BLOCK_TYPE[] = [
  {
    title: "Playground",
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

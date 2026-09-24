import {
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
import { SOUND_SOURCES } from "@/shared/lib/sounds";

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
  sound?: keyof typeof SOUND_SOURCES;
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
      {
        id: "MyAccount",
        link: "/account",
        name: "My Account",
        icon: User,
        sound: "do",
      },
      {
        id: "Posts",
        link: "/posts",
        name: "Posts",
        icon: ListMusic,
        sound: "re",
      },
      {
        id: "Messages",
        link: "/messages",
        name: "Messages",
        icon: MessageCircle,
        sound: "mi",
      },
    ],
  },
];

export const SIDEBAR_FOOTER_ITEMS: SIDEBAR_ITEM_TYPE[] = [
  {
    id: "Support",
    link: "https://github.com/AndruTRADX/Woodshed",
    name: "Support",
    icon: Heart,
    external: true,
  },
];

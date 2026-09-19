import { ListMusic, MessageCircle, User, type LucideProps } from "lucide-react";

type sidebarItemType = {
  id: string;
  link: string;
  name: string;
  icon: React.ForwardRefExoticComponent<
    Omit<LucideProps, "ref"> & React.RefAttributes<SVGSVGElement>
  >;
  subitems?: sidebarSubItemType[];
};

type sidebarSubItemType = {
  id: string;
  link: string;
  name: string;
  icon: React.ForwardRefExoticComponent<
    Omit<LucideProps, "ref"> & React.RefAttributes<SVGSVGElement>
  >;
};

export const sidebarItems: sidebarItemType[] = [
  {
    id: "MyAccount",
    link: "/account",
    name: "My Account",
    icon: User,
  },
  {
    id: "Posts",
    link: "/posts",
    name: "Posts",
    icon: ListMusic,
  },
  {
    id: "Messages",
    link: "/messages",
    name: "Messages",
    icon: MessageCircle,
  },
];

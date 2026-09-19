import {
  Avatar,
  AvatarFallback,
  AvatarImage,
} from "@/shared/components/ui/avatar";
import { User } from "lucide-react";

export default function UserAvatar({
  imageUrl,
  nickname,
}: {
  imageUrl?: string | null;
  nickname: string;
}) {
  if (!imageUrl) return <User className="text-primary min-w-5" />;

  return (
    <Avatar size="sm">
      <AvatarImage
        src={imageUrl.replace(
          "/upload/",
          "/upload/w_30,h_30,c_fill,f_auto,dpr_2/",
        )}
        alt={nickname}
      />
      <AvatarFallback>{nickname}</AvatarFallback>
    </Avatar>
  );
}

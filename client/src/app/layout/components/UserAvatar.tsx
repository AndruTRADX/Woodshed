import {
  Avatar,
  AvatarFallback,
  AvatarImage,
} from "@/shared/components/ui/avatar";
import { User } from "lucide-react";

type Props = {
  imageUrl?: string | null;
  nickname: string;
  size?: "default" | "sm" | "lg";
};

export default function UserAvatar({
  imageUrl,
  nickname,
  size = "default",
}: Props) {
  return (
    <Avatar size={size}>
      {imageUrl && (
        <AvatarImage
          src={imageUrl.replace(
            "/upload/",
            "/upload/w_30,h_30,c_fill,f_auto,dpr_2/",
          )}
          alt={nickname}
        />
      )}
      <AvatarFallback>
        {imageUrl ? (
          nickname.slice(0, 2).toUpperCase()
        ) : (
          <User className="size-1/2 text-primary" />
        )}
      </AvatarFallback>
    </Avatar>
  );
}

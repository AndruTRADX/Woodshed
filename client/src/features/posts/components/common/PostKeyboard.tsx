import { cn } from "@/shared/lib/utils";
import { Heart, MessageCircle, type LucideIcon } from "lucide-react";

const ACTION_BUTTON =
  "flex items-center gap-1.5 rounded-full px-3 py-1.5 text-sm text-neutral-400 outline-none " +
  "transition-[color,background-color,transform] hover:bg-white/5 hover:text-primary " +
  "focus-visible:ring-2 focus-visible:ring-primary active:scale-95 disabled:pointer-events-none disabled:opacity-50";

type ActionProps = {
  icon: LucideIcon;
  count: string | number;
  label: string;
  onClick?: () => void;
  active?: boolean;
  disabled?: boolean;
};

function Action({
  icon: Icon,
  count,
  label,
  onClick,
  active,
  disabled,
}: ActionProps) {
  return (
    <button
      type="button"
      className={cn(ACTION_BUTTON, active && "text-primary")}
      aria-label={`${label} (${count})`}
      aria-pressed={active}
      title={label}
      onClick={onClick}
      disabled={disabled}
    >
      <Icon className="size-4" fill={active ? "currentColor" : "none"} />
      <span className="tabular-nums">{count}</span>
    </button>
  );
}

type Props = {
  likesCount: string | number;
  commentsCount: string | number;
  isLiked?: boolean;
  isLikePending?: boolean;
  onLike?: () => void;
  onOpenComments?: () => void;
};

export default function PostActions({
  likesCount,
  commentsCount,
  isLiked,
  isLikePending,
  onLike,
  onOpenComments,
}: Props) {
  return (
    <div className="flex items-center gap-1">
      <Action
        icon={Heart}
        count={likesCount}
        label={isLiked ? "Unlike" : "Like"}
        onClick={onLike}
        active={isLiked}
        disabled={isLikePending}
      />
      <Action
        icon={MessageCircle}
        count={commentsCount}
        label="Comments"
        onClick={onOpenComments}
      />
    </div>
  );
}

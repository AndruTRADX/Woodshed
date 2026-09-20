import { Heart, MessageCircle, type LucideIcon } from "lucide-react";

const ACTION_BUTTON =
  "flex items-center gap-1.5 rounded-full px-3 py-1.5 text-sm text-neutral-400 outline-none " +
  "transition-[color,background-color,transform] hover:bg-white/5 hover:text-primary " +
  "focus-visible:ring-2 focus-visible:ring-primary active:scale-95";

type ActionProps = {
  icon: LucideIcon;
  count: string | number;
  label: string;
  onClick?: () => void;
};

function Action({ icon: Icon, count, label, onClick }: ActionProps) {
  return (
    <button
      type="button"
      className={ACTION_BUTTON}
      aria-label={`${label} (${count})`}
      title={label}
      onClick={onClick}
    >
      <Icon className="size-4" />
      <span className="tabular-nums">{count}</span>
    </button>
  );
}

type Props = {
  likesCount: string | number;
  commentsCount: string | number;
  onLike?: () => void;
  onOpenComments?: () => void;
};

export default function PostActions({
  likesCount,
  commentsCount,
  onLike,
  onOpenComments,
}: Props) {
  return (
    <div className="flex items-center gap-1">
      <Action icon={Heart} count={likesCount} label="Like" onClick={onLike} />
      <Action
        icon={MessageCircle}
        count={commentsCount}
        label="Comments"
        onClick={onOpenComments}
      />
    </div>
  );
}
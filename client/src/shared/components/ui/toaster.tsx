import { useLiquidGlass } from "@/shared/hooks/useLiquidGlass";
import { cn } from "@/shared/lib/utils";
import {
  useToastStore,
  type ToastItem,
  type ToastType,
} from "@/shared/stores/toastStore";
import {
  CircleCheckIcon,
  InfoIcon,
  OctagonXIcon,
  TriangleAlertIcon,
  XIcon,
  type LucideIcon,
} from "lucide-react";

const TOAST_ICONS: Record<ToastType, LucideIcon | null> = {
  success: CircleCheckIcon,
  error: OctagonXIcon,
  warning: TriangleAlertIcon,
  info: InfoIcon,
  default: null,
};

const TOAST_ACCENTS: Record<ToastType, string> = {
  success: "text-emerald-500",
  error: "text-destructive",
  warning: "text-amber-500",
  info: "text-primary",
  default: "text-primary",
};

function ToastCard({ toast }: { toast: ToastItem }) {
  const { ref: glassRef, style: glassStyle } = useLiquidGlass<HTMLElement>({
    preset: "large",
  });
  const dismiss = useToastStore((state) => state.dismiss);
  const remove = useToastStore((state) => state.remove);
  const Icon = TOAST_ICONS[toast.type];

  return (
    <div
      ref={glassRef}
      style={glassStyle}
      onAnimationEnd={() => {
        if (toast.isExiting) remove(toast.id);
      }}
      className={cn(
        "min-w-xs pointer-events-auto flex w-full items-start gap-3 rounded-xl bg-popover/70 px-4 py-3 shadow-lg dark:bg-popover/30",
        toast.isExiting
          ? "animate-out slide-out-to-right-full fade-out duration-300 ease-out"
          : "animate-in slide-in-from-right-full fade-in duration-300 ease-[cubic-bezier(0.34,1.56,0.64,1)]",
      )}
    >
      {Icon && (
        <Icon
          className={cn("mt-0.5 size-4 shrink-0", TOAST_ACCENTS[toast.type])}
        />
      )}
      <div className="flex-1 min-w-0">
        <p className="text-sm font-medium text-popover-foreground">
          {toast.title}
        </p>
        {toast.description && (
          <p className="mt-0.5 whitespace-pre-line text-xs text-muted-foreground">
            {toast.description}
          </p>
        )}
      </div>
      <button
        type="button"
        onClick={() => dismiss(toast.id)}
        aria-label="Dismiss"
        className="text-muted-foreground hover:text-foreground"
      >
        <XIcon className="size-4" />
      </button>
    </div>
  );
}

export function Toaster() {
  const toasts = useToastStore((state) => state.toasts);

  return (
    <div className="pointer-events-none fixed inset-x-4 bottom-4 z-50 mx-auto flex w-auto max-w-sm flex-col gap-2 sm:inset-x-auto sm:right-4 sm:left-auto">
      {toasts.map((toastItem) => (
        <ToastCard key={toastItem.id} toast={toastItem} />
      ))}
    </div>
  );
}

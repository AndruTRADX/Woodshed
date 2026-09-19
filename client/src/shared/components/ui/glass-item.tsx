import { useLiquidGlass } from "@/shared/hooks/useLiquidGlass";
import { cn } from "@/shared/lib/utils";
import type React from "react";

interface Props {
  children: React.ReactNode;
  className?: string;
}

export default function GlassItem({ children, className }: Props) {
  const { ref: glassRef, style: glassStyle } = useLiquidGlass<HTMLElement>();

  return (
    <div
      ref={glassRef}
      style={glassStyle}
      className={cn(
        "w-full flex justify-start px-4 sm:px-3 py-2.5 bg-background/40 rounded-xl",
        className,
      )}
    >
      {children}
    </div>
  );
}

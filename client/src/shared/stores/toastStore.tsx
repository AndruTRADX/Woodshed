import { create } from "zustand";

export type ToastType = "success" | "error" | "warning" | "info" | "default";

export interface ToastOptions {
  /** Toast heading. */
  title: string;

  /** Optional body copy below the title. Supports embedded `\n` line breaks. */
  description?: string;

  /** Visual/semantic variant - controls icon and accent color. Defaults to `"info"`. */
  type?: ToastType;

  /** How long the toast stays before auto-dismissing, in ms. `0` disables auto-dismiss. Defaults to `5000`. */
  duration?: number;
}

export interface ToastItem {
  id: string;
  title: string;
  description?: string;
  type: ToastType;
  duration: number;
  isExiting: boolean;
}

interface ToastState {
  toasts: ToastItem[];
  add: (options: ToastOptions) => string;
  /** Marks the toast as exiting so its exit animation can play; the card removes itself from state once that animation ends. */
  dismiss: (id: string) => void;
  /** Actually drops the toast from state. Call this from the exit animation's `onAnimationEnd`, not directly. */
  remove: (id: string) => void;
}

export const useToastStore = create<ToastState>((set, get) => ({
  toasts: [],

  add: (options) => {
    const id = crypto.randomUUID();
    const duration = options.duration ?? 5000;

    set((state) => ({
      toasts: [
        ...state.toasts,
        {
          id,
          title: options.title,
          description: options.description,
          type: options.type ?? "default",
          duration,
          isExiting: false,
        },
      ],
    }));

    if (duration > 0) {
      setTimeout(() => get().dismiss(id), duration);
    }

    return id;
  },

  dismiss: (id) => {
    set((state) => ({
      toasts: state.toasts.map((t) =>
        t.id === id ? { ...t, isExiting: true } : t,
      ),
    }));
  },

  remove: (id) => {
    set((state) => ({ toasts: state.toasts.filter((t) => t.id !== id) }));
  },
}));

export const toast = {
  add: (options: ToastOptions) => useToastStore.getState().add(options),
  dismiss: (id: string) => useToastStore.getState().dismiss(id),
};

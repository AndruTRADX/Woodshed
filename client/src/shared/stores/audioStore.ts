import { create } from "zustand";
import { persist } from "zustand/middleware";

interface AudioState {
  volume: number;
  muted: boolean;
  setVolume: (volume: number) => void;
  toggleMute: () => void;
  setMuted: (muted: boolean) => void;
}

export const useAudioStore = create<AudioState>()(
  persist(
    (set) => ({
      volume: 0.7,
      muted: false,

      setVolume: (volume) => set({ volume: Math.min(1, Math.max(0, volume)) }),

      toggleMute: () => set((state) => ({ muted: !state.muted })),

      setMuted: (muted) => set({ muted }),
    }),
    { name: "woodshed-audio-settings" },
  ),
);

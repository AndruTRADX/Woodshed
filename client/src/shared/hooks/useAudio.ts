import { useCallback } from "react";

import { useAudioStore } from "@/shared/stores/audioStore";
import {
  playSound,
  stopAllSounds,
  stopSound,
  type PlaySoundOptions,
  type SoundName,
} from "@/shared/lib/sounds";


export const useAudio = () => {
  const volume = useAudioStore((state) => state.volume);
  const muted = useAudioStore((state) => state.muted);

  const play = useCallback(
    (sound: SoundName, options?: PlaySoundOptions) => {
      playSound(sound, volume, muted, options);
    },
    [volume, muted],
  );

  return { play, stop: stopSound, stopAll: stopAllSounds };
};
export const SOUND_SOURCES = {
  login: "/sounds/login.mp4",
  post: "/sounds/post.mp4",
  postComment: "/sounds/post-comment.mp4",
  error: "/sounds/error.mp4",
} as const;

export type SoundName = keyof typeof SOUND_SOURCES;

export interface PlaySoundOptions {
  volume?: number;
  loop?: boolean;
  playbackRate?: number;
  onEnded?: () => void;

  restart?: boolean;
}

const audioElements = new Map<SoundName, HTMLAudioElement>();

function getAudioElement(sound: SoundName): HTMLAudioElement {
  let audio = audioElements.get(sound);

  if (!audio) {
    audio = new Audio(SOUND_SOURCES[sound]);
    audioElements.set(sound, audio);
  }

  return audio;
}

const clampVolume = (volume: number) => Math.min(1, Math.max(0, volume));


export function playSound(
  sound: SoundName,
  masterVolume: number,
  masterMuted: boolean,
  options: PlaySoundOptions = {},
) {
  const {
    volume = 1,
    loop = false,
    playbackRate = 1,
    onEnded,
    restart = true,
  } = options;

  const audio = getAudioElement(sound);

  if (restart) {
    audio.pause();
    audio.currentTime = 0;
  }

  audio.volume = clampVolume(masterVolume * volume);
  audio.muted = masterMuted;
  audio.loop = loop;
  audio.playbackRate = playbackRate;
  audio.onended = loop ? null : (onEnded ?? null);

  void audio.play().catch(() => {});
}

export function stopSound(sound: SoundName) {
  const audio = audioElements.get(sound);
  if (!audio) return;

  audio.pause();
  audio.currentTime = 0;
}

export function stopAllSounds() {
  audioElements.forEach((audio) => {
    audio.pause();
    audio.currentTime = 0;
  });
}
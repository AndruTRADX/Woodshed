type Sounds = {
  login: "sounds/login.mp4"
}

const mp4Sound = new Audio("sounds/login.mp4")

export const useAudio = () => {
  const reproduceAudio = () => {
    
    mp4Sound.play()
  }

  return {
    reproduceAudio
  }
}
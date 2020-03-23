using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicEnum { boardMusic, menuMusic, minigameMusic, winMusic };

public enum SoundEnum {/*START OF GAMESOUNDS*/ winSound, loseTurnSound, plusThreeSound, minusThreeSound, plusTenSound, chanceSlotSound, minigameSlotSound, minigameStartSound, tpPlayerSound, placeBagSound, tickingClockSound,  /*END OF GAMESOUNDS*/ /*START OF PLAYERSOUND*/walkSound, jumpSound, landSound /*END OF PLAYERSOUND*/ };

[System.Serializable]
public class Music
{
    [Header("Music")]
    public AudioClip boardMusic;
    public AudioClip menuMusic;
    public AudioClip minigameMusic;
    public AudioClip winMusic;
}

[System.Serializable]
public class PlayerSound
{
    [Header("PlayerSounds")]
    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
}

[System.Serializable]
public class GameSound
{
    [Header("GameSounds")]
    public AudioClip winSound;
    public AudioClip tickingClockSound;
    public AudioClip minigameStartsound;
    [Space(4)]
    [Header("ChanceSounds")]
    public AudioClip loseTurnSound;
    public AudioClip tpPlayerSound;
    public AudioClip placeBagSound;
    [Space(4)]
    [Header("SlotsSounds")]
    public AudioClip plusThreeSound;
    public AudioClip minusThreeSound;
    public AudioClip plusTenSound;
    public AudioClip chanceSlotSound;
    public AudioClip minigameSlotSound;
}

[System.Serializable]
public class Sources
{
    [Header("MusicSource")]
    public AudioSource boardSource;
    public AudioSource minigameSource;

    [Header("SoundSource")]
    public AudioSource soundSource;
}

public class AudioController : MonoBehaviour
{
    [HideInInspector] public MusicEnum musicEnum;
    [HideInInspector] public SoundEnum soundEnum;

    public Music music;

   

    public PlayerSound playerSound;

  

    public GameSound gameSound;

  

    public Sources source;

    private static bool keepFadingIn;
    private static bool keepFadingOut;

    public static AudioController instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySong(MusicEnum song)
    {
        switch (song)
        {
            case MusicEnum.boardMusic:
                source.boardSource.clip = music.boardMusic;
                break;
            case MusicEnum.menuMusic:
                //musicSource.clip = menuMusic;
                break;
            case MusicEnum.minigameMusic:
                source.minigameSource.clip = music.minigameMusic;
                break;
            case MusicEnum.winMusic:
                //musicSource.clip = winMusic;
                break;
            default:
                break;
        }
        //musicSource.Play();
        source.minigameSource.Play();
        source.boardSource.Play();
    }

    public void PlaySound(SoundEnum sound)
    {
        switch (sound)
        {
            //START OF GAMESOUND//
            case SoundEnum.winSound:
                source.soundSource.PlayOneShot(gameSound.winSound);
                break;
            case SoundEnum.loseTurnSound:
                Invoke("LoseSoundDelay", 1.8f);
                break;
            case SoundEnum.plusThreeSound:
                source.soundSource.PlayOneShot(gameSound.plusThreeSound);
                break;
            case SoundEnum.minusThreeSound:
                source.soundSource.PlayOneShot(gameSound.minusThreeSound);
                break;
            case SoundEnum.plusTenSound:
                source.soundSource.PlayOneShot(gameSound.plusTenSound);
                break;
            case SoundEnum.tpPlayerSound:
                Invoke("TpPlayerSoundDelay", 1.8f);
                break;
            case SoundEnum.placeBagSound:
                Invoke("PlaceBagSoundDelay", 1.8f);
                break;
            case SoundEnum.minigameStartSound:
                source.soundSource.PlayOneShot(gameSound.minigameStartsound);
                break;
            case SoundEnum.minigameSlotSound:
                source.soundSource.PlayOneShot(gameSound.minigameSlotSound);
                break;
            case SoundEnum.chanceSlotSound:
                source.soundSource.PlayOneShot(gameSound.chanceSlotSound);
                break;
            case SoundEnum.tickingClockSound:
                source.soundSource.PlayOneShot(gameSound.tickingClockSound);
                break;
            //END OF GAMESOUND//
            //START OF PLAYERSOUND//
            case SoundEnum.walkSound:
                source.soundSource.PlayOneShot(playerSound.walkSound);
                break;
            case SoundEnum.jumpSound:
                source.soundSource.PlayOneShot(playerSound.jumpSound);
                break;
            case SoundEnum.landSound:
                source.soundSource.PlayOneShot(playerSound.landSound);
                break;
            //END OF PLAYERSOUND//
            default:
                break;
        }
        source.soundSource.Play();
    }

    public void FadeInCaller(AudioSource songSource, float speed, float maxVolume)
    {
        instance.StartCoroutine(FadeIn(songSource, speed, maxVolume));
    }

    public void FadeOutCaller(AudioSource songSource, float speed, float minVolume)
    {
        instance.StartCoroutine(FadeOut(songSource, speed, minVolume));
    }

    public void UpdateMusicVolume (float musicThreshold)
    {
        if(GetComponent<MinigameController>().playedMinigameThisRound)
        {
            source.minigameSource.volume = musicThreshold;
        }
        else
        {
            source.boardSource.volume = musicThreshold;
        }
    }

    IEnumerator FadeIn(AudioSource songSource, float speed, float maxVolume)
    {
        keepFadingIn = true;

        float fadeInTimer = Time.deltaTime;

        songSource.volume = 0;
        float audioVolume = songSource.volume;

        if (keepFadingIn)
        {
            while (songSource.volume <= maxVolume && keepFadingIn)
            {
                if (songSource.volume == maxVolume)
                {
                    keepFadingIn = false;
                    yield break;
                }
                else
                {
                    audioVolume += speed * fadeInTimer;
                    songSource.volume = audioVolume;

                    yield return new WaitForSeconds(0.1f);
                }
            } 
        }
    }

    private float GetThreshold ()
    {
        return GetComponent<MinigameController>().musicThreshold;
    }

    IEnumerator FadeOut(AudioSource songSource, float speed, float minVolume)
    {
        keepFadingOut = true;

        float fadeOutTimer = Time.deltaTime;

        songSource.volume = GetThreshold();
        float audioVolume = songSource.volume;
       
        if (keepFadingOut)
        {
            while (songSource.volume >= minVolume && keepFadingOut)
            {
                if (songSource.volume == minVolume)
                {
                    keepFadingOut = false;
                    yield break;
                }
                else
                {
                    audioVolume -= speed * fadeOutTimer;
                    songSource.volume = audioVolume;

                    yield return new WaitForSeconds(0.1f);
                }     
            }
        }
    }

    void LoseSoundDelay()
    {
        source.soundSource.PlayOneShot(gameSound.loseTurnSound);
    }

    void PlaceBagSoundDelay()
    {
        source.soundSource.PlayOneShot(gameSound.placeBagSound);
    }

    void TpPlayerSoundDelay()
    {
        source.soundSource.PlayOneShot(gameSound.tpPlayerSound);
    }
}
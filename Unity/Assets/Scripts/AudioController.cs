using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicEnum { boardMusic, menuMusic, minigameMusic };

public enum SoundEnum {/*START OF GAMESOUNDS*/ winSound, loseTurnSound, plusThreeSound, minusThreeSound, plusTenSound, chanceSlotSound, minigameSlotSound, minigameStartSound, tpPlayerSound, placeBagSound,  /*END OF GAMESOUNDS*/ /*START OF PLAYERSOUND*/walkSound, jumpSound, landSound /*END OF PLAYERSOUND*/ };

public class AudioController : MonoBehaviour
{
    [HideInInspector] public MusicEnum musicEnum;
    [HideInInspector] public SoundEnum soundEnum;

    [Header("Music")]
    public AudioClip boardMusic;
    public AudioClip menuMusic;
    public AudioClip minigameMusic;

    [Header("PlayerSound")]
    public AudioClip walkSound;
    public AudioClip jumpSound;
    public AudioClip landSound;

    [Header("GameSound")]
    public AudioClip winSound;
    public AudioClip loseTurnSound;
    public AudioClip plusThreeSound;
    public AudioClip minusThreeSound;
    public AudioClip plusTenSound;
    public AudioClip chanceSlotSound;
    public AudioClip minigameSlotSound;
    public AudioClip minigameStartsound;
    public AudioClip tpPlayerSound;
    public AudioClip placeBagSound;


    [Header("MusicSource")]
    public AudioSource boardSource;
    public AudioSource minigameSource;

    [Header("SoundSource")]
    public AudioSource soundSource;

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
                boardSource.clip = boardMusic;
                break;
            case MusicEnum.menuMusic:
                //musicSource.clip = menuMusic;
                break;
            case MusicEnum.minigameMusic:
                minigameSource.clip = minigameMusic;
                break;
            default:
                break;
        }
        //musicSource.Play();
        minigameSource.Play();
        boardSource.Play();
    }

    public void PlaySound(SoundEnum sound)
    {
        switch (sound)
        {
            //START OF GAMESOUND//
            case SoundEnum.winSound:
                soundSource.PlayOneShot(winSound);
                break;
            case SoundEnum.loseTurnSound:
                //soundSource.PlayOneShot(loseTurnSound);
                Invoke("LoseSoundDelay", 1.8f);
                break;
            case SoundEnum.plusThreeSound:
                soundSource.PlayOneShot(plusThreeSound);
                break;
            case SoundEnum.minusThreeSound:
                soundSource.PlayOneShot(minusThreeSound);
                break;
            case SoundEnum.plusTenSound:
                soundSource.PlayOneShot(plusTenSound);
                break;
            case SoundEnum.tpPlayerSound:
                //soundSource.PlayOneShot(tpPlayerSound);
                Invoke("TpPlayerSoundDelay", 1.8f);
                break;
            case SoundEnum.placeBagSound:
                //soundSource.PlayOneShot(placeBagSound);
                Invoke("PlaceBagSoundDelay", 1.8f);
                break;
            case SoundEnum.minigameStartSound:
                soundSource.PlayOneShot(minigameStartsound);
                break;
            case SoundEnum.minigameSlotSound:
                soundSource.PlayOneShot(minigameSlotSound);
                break;
            case SoundEnum.chanceSlotSound:
                soundSource.PlayOneShot(chanceSlotSound);
                break;
            //END OF GAMESOUND//
            //START OF PLAYERSOUND//
            case SoundEnum.walkSound:
                soundSource.PlayOneShot(walkSound);
                break;
            case SoundEnum.jumpSound:
                soundSource.PlayOneShot(jumpSound);
                break;
            case SoundEnum.landSound:
                soundSource.PlayOneShot(landSound);
                break;
            //END OF PLAYERSOUND//
            default:
                break;
        }
        soundSource.Play();
    }

    public void FadeInCaller(AudioSource songSource, float speed, float maxVolume)
    {
        instance.StartCoroutine(FadeIn(songSource, speed, maxVolume));
    }

    public void FadeOutCaller(AudioSource songSource, float speed, float minVolume)
    {
        instance.StartCoroutine(FadeOut(songSource, speed, minVolume));
    }

    static IEnumerator FadeIn(AudioSource songSource, float speed, float maxVolume)
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

    static IEnumerator FadeOut(AudioSource songSource, float speed, float minVolume)
    {
        keepFadingOut = true;

        float fadeOutTimer = Time.deltaTime;

        songSource.volume = 1;
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
        soundSource.PlayOneShot(loseTurnSound);
    }

    void PlaceBagSoundDelay()
    {
        soundSource.PlayOneShot(placeBagSound);
    }

    void TpPlayerSoundDelay()
    {
        soundSource.PlayOneShot(tpPlayerSound);
    }
}
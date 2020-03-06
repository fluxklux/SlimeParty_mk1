using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicEnum { boardMusic, menuMusic, minigameMusic };

public enum SoundEnum { winSound, loseSound, plusThree, minusThree, walkSound, jumpSound };

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

    [Header("GameSound")]
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip plusThree;
    public AudioClip minusThree;

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
            case SoundEnum.winSound:
                soundSource.PlayOneShot(winSound);
                break;
            case SoundEnum.loseSound:
                soundSource.PlayOneShot(loseSound);
                break;
            case SoundEnum.plusThree:
                soundSource.PlayOneShot(plusThree);
                break;
            case SoundEnum.minusThree:
                soundSource.PlayOneShot(minusThree);
                break;
            case SoundEnum.walkSound:
                soundSource.PlayOneShot(walkSound);
                break;
            case SoundEnum.jumpSound:
                soundSource.PlayOneShot(jumpSound);
                break;
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
}
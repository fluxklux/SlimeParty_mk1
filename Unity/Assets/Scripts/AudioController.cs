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

    [Header("Source")]
    public AudioSource musicSource;
    public AudioSource soundSource;

    public void PlaySong(MusicEnum song)
    {
        switch (song)
        {
            case MusicEnum.boardMusic:
                musicSource.clip = boardMusic;
                break;
            case MusicEnum.menuMusic:
                musicSource.clip = menuMusic;
                break;
            case MusicEnum.minigameMusic:
                musicSource.clip = minigameMusic;
                break;
            default:
                break;
        }
        musicSource.Play();
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
}
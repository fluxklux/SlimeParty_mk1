using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMax : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] Clips;

    void Start()
    {
        source.PlayOneShot(Clips[Random.Range(0, Clips.Length)]);
    }
    public void talk() {
        source.PlayOneShot(Clips[Random.Range(0, Clips.Length)]);
    }

}

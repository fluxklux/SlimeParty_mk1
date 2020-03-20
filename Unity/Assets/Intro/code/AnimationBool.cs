using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBool : MonoBehaviour
{
    public void PlayAnimation()
    {
        GetComponent<Animator>().SetBool("Activate", true);
    }
    public void DePlayAnimation()
    {
        GetComponent<Animator>().SetBool("Activate", false);
    }
}

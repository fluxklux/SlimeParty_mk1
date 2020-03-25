using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    public void PlayAnimation()
    {
        GetComponent<Animator>().SetBool("Small", true);
    }
    public void DePlayAnimation()
    {
        GetComponent<Animator>().SetBool("Small", false);
    }
}

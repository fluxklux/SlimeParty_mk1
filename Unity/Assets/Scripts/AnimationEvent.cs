using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private GameController gc;

    private void Start ()
    {
        gc = Object.FindObjectOfType<GameController>();
    }

    #region EffectText
    public void ResetPlayTrigger()
    {
        gc.GetComponent<RoundController>().ResetPlayTrigger();
    }
    #endregion
}

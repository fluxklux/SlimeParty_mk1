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
    public void ResetDisplayTrigger()
    {
        gc.GetComponent<RoundController>().ResetRoundTrigger();
    }
    #endregion
}

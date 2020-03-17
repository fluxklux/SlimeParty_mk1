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

    public void ResetMinigameTrigger()
    {
        gc.GetComponent<RoundController>().ResetMinigameTrigger();
    }

    public void ResetChanceTrigger()
    {
        gc.GetComponent<RoundController>().ResetChanceTrigger();
    }

    public void ResetPlusThreeTrigger()
    {
        gc.GetComponent<RoundController>().ResetPlusThreeTrigger();
    }

    public void ResetPlusTenTrigger()
    {
        gc.GetComponent<RoundController>().ResetPlusTenTrigger();
    }

    public void ResetMinusThreeTrigger()
    {
        gc.GetComponent<RoundController>().ResetMinusThreeTrigger();
    }
    #endregion
}

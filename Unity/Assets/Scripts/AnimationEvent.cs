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

    #region p1 animations
    public void ResetAfterLanding ()
    {
        //detta kan skapa problem, vet inte hur Alexs jump system fungerar
        GetComponentInParent<PlayerController>().playerVariable.isJumping = false;
        GetComponentInParent<PlayerController>().playerVariable.isLanding = false;
        GetComponent<Animator>().SetBool("JumpingBool", false);
        GetComponent<Animator>().SetBool("LandingBool", false);
    }
    #endregion
}

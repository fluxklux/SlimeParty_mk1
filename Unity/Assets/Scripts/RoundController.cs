using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    [SerializeField] private Text overEndText = null;
    [SerializeField] private int roundMax = 30;
    [SerializeField] private GameObject roundsLeftEffect = null;

    [Header("Effects: ")]
    [SerializeField] private Sprite fiveRounds = null;
    [SerializeField] private Sprite lastRound = null;
    [SerializeField] private Sprite tenRounds = null;

    [HideInInspector]
    public int round;

    void Start()
    {
        round = 18;
    }

    #region EffectTriggerResets
    public void ResetPlayTrigger()
    {
        roundsLeftEffect.GetComponent<Animator>().ResetTrigger("Play");
    }

    public void ResetPlusThreeTrigger()
    {
        roundsLeftEffect.GetComponent<Animator>().ResetTrigger("PlayPlusThree");
    }

    public void ResetPlusTenTrigger()
    {
        roundsLeftEffect.GetComponent<Animator>().ResetTrigger("PlayPlusTen");
    }

    public void ResetMinusThreeTrigger()
    {
        roundsLeftEffect.GetComponent<Animator>().ResetTrigger("PlayMinusThree");
    }

    public void ResetMinigameTrigger()
    {
        roundsLeftEffect.GetComponent<Animator>().ResetTrigger("PlayMinigame");
    }

    public void ResetChanceTrigger()
    {
        roundsLeftEffect.GetComponent<Animator>().ResetTrigger("PlayChance");
    }
    #endregion

    public void DisplayRoundsLeft ()
    {
        Debug.Log("DisplayRoundsLeft");
        switch (round)
        {
            case 20:
                Debug.Log("10 rounds left");
                roundsLeftEffect.GetComponent<Image>().sprite = tenRounds;
                roundsLeftEffect.GetComponent<Animator>().SetTrigger("Play");
                break;
            case 25:
                Debug.Log("5 rounds left");
                roundsLeftEffect.GetComponent<Image>().sprite = fiveRounds;
                roundsLeftEffect.GetComponent<Animator>().SetTrigger("Play");
                break;
            case 29:
                Debug.Log("1 round left");
                roundsLeftEffect.GetComponent<Image>().sprite = lastRound;
                roundsLeftEffect.GetComponent<Animator>().SetTrigger("Play");
                break;
            default:
                break;
        }
    }

    public void UpdateRound()
    {
        round++;
        if(round >= roundMax)
        {
            Time.timeScale = 0f;
            GetComponent<PauseController>().EndGame();
            overEndText.gameObject.SetActive(true);
        }
    }
}

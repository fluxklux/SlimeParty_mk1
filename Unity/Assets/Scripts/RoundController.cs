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
        round = 1;
    }

    public void ResetRoundTrigger ()
    {
        roundsLeftEffect.GetComponent<Animator>().ResetTrigger("Play");
    }

    public void DisplayRoundsLeft ()
    {
        if(round == 20 || round == 25 || round == 29)
        {
            switch (round)
            {
                case 10:
                    roundsLeftEffect.GetComponent<Image>().sprite = tenRounds;
                    roundsLeftEffect.GetComponent<Animator>().SetTrigger("Play");
                    break;
                case 5:
                    roundsLeftEffect.GetComponent<Image>().sprite = fiveRounds;
                    roundsLeftEffect.GetComponent<Animator>().SetTrigger("Play");
                    break;
                case 1:
                    roundsLeftEffect.GetComponent<Image>().sprite = lastRound;
                    roundsLeftEffect.GetComponent<Animator>().SetTrigger("Play");
                    break;
                default:
                    break;
            }
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

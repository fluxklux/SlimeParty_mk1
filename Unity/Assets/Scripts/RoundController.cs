using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class SortingClass
{
   public int playerIndex;
   public int fruitAmount;
}

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

    private GameController gc;

    public SortingClass[] sortedFruits = { null, null, null, null };

    public GameObject winScreen;

    public GameObject[] flagsVisual;

    public GameObject[] playerVisual;

    public Sprite[] flagSprite;

    public Sprite[] playerSprite;

    public Text[] textVisual;

    void Start()
    {
        round = 1;

        gc = GetComponent<GameController>();
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

    public void DisplayRoundsLeft()
    {
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
        if (round >= roundMax)
        {
            Time.timeScale = 0f;
            GetComponent<PauseController>().EndGame();
            SortPlayer();

            //overEndText.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            SortPlayer();
        }
    }

    private void SortPlayer()
    {
        for (int k = 0; k < gc.playerFruits.Length; k++)
        {
            sortedFruits[k].playerIndex = k;
            sortedFruits[k].fruitAmount = gc.playerFruits[k];   
        }

        for (int i = 0; i < sortedFruits.Length - 1; i++)
        {
            for (int j = 0; j < sortedFruits.Length - 1 - i; j++)
            {
                if (sortedFruits[j].fruitAmount > sortedFruits[j + 1].fruitAmount)
                {
                    SortingClass tmp = sortedFruits[j + 1];
                    sortedFruits[j + 1] = sortedFruits[j];
                    sortedFruits[j] = tmp;
                }
            }
        }

        for (int q = 0; q < sortedFruits.Length; q++)
        {
            playerVisual[q].GetComponent<Image>().sprite = playerSprite[sortedFruits[q].playerIndex];
            flagsVisual[q].GetComponent<Image>().sprite = flagSprite[sortedFruits[q].playerIndex];
            textVisual[q].text = sortedFruits[q].fruitAmount.ToString();
        }

        winScreen.SetActive(true);
    }

}

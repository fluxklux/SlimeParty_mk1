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
    [SerializeField] private int roundMax = 30;
    [SerializeField] private GameObject roundsLeftEffect = null;

    [Header("Effects: ")]
    [SerializeField] private Sprite fiveRounds = null;
    [SerializeField] private Sprite lastRound = null;
    [SerializeField] private Sprite tenRounds = null;

    [HideInInspector]
    public int round;

    private GameController gc;

    private AudioController ac;

    public GameObject winScreen;

    public SortingClass[] sortedFruits = { null, null, null, null };

    public GameObject[] flagsVisual;

    public GameObject[] playerVisual;

    public Sprite[] flagSprite;

    public Sprite[] playerSprite;

    public Text[] textVisual;

    private bool doneOnce = false;

    public Text[] orderingText;

    public SortingClass[] playerOrder = { null, null, null, null };

    void Start()
    {
        round = 1;

        gc = GetComponent<GameController>();

        ac = GetComponent<AudioController>();
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
            //ac.FadeInCaller(ac.source.minigameSource, 0.75f, 1);
            //ac.FadeOutCaller(ac.source.boardSource, 0.75f, 0);
            //Time.timeScale = 0f;
            //GetComponent<PauseController>().EndGame();
            //SortPlayer();

            if (!doneOnce)
            {
                StartCoroutine(GameDone());
                doneOnce = true;
            }
        }
    }

    private IEnumerator GameDone()
    {
        ac.PlaySong(MusicEnum.endMusic);
        ac.FadeInCaller(ac.source.minigameSource, 1.75f, 1);
        ac.FadeOutCaller(ac.source.boardSource, 1.75f, 0);
        GetComponent<PauseController>().EndGame();
        SortPlayer();
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
    }

    public void SortPlayer()
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





        if (round >= roundMax)
        {
            for (int q = 0; q < sortedFruits.Length; q++)
            {
                playerVisual[q].GetComponent<Image>().sprite = playerSprite[sortedFruits[q].playerIndex];
                flagsVisual[q].GetComponent<Image>().sprite = flagSprite[sortedFruits[q].playerIndex];
                textVisual[q].text = sortedFruits[q].fruitAmount.ToString();
            }
            winScreen.SetActive(true);
        }
    }

    public void SortingPlayer()
    {
        for (int k = 0; k < gc.playerFruits.Length; k++)
        {
            playerOrder[k].playerIndex = k;
            playerOrder[k].fruitAmount = gc.playerFruits[k];
        }

        for (int i = 0; i < playerOrder.Length - 1; i++)
        {
            for (int j = 0; j < playerOrder.Length - 1 - i; j++)
            {
                if (playerOrder[j].fruitAmount > playerOrder[j + 1].fruitAmount)
                {
                    SortingClass tmp = playerOrder[j + 1];
                    playerOrder[j + 1] = playerOrder[j];
                    playerOrder[j] = tmp;
                }
            }
        }

        for (int l = playerOrder.Length - 1; l >= 0; l--)
        {
            //Debug.Log("INDEX: " + playerOrder[l].playerIndex + " INT: " + l);
            //orderingText[playerOrder[l].playerIndex].text = ((3 - l) + 1).ToString();

            switch (l)
            {
                case 0:
                    orderingText[playerOrder[l].playerIndex].text = ((3 - l) + 1) + "th";
                    break;
                case 1:
                    orderingText[playerOrder[l].playerIndex].text = ((3 - l) + 1) + "rd";
                    break;
                case 2:
                    orderingText[playerOrder[l].playerIndex].text = ((3 - l) + 1) + "nd";
                    break;
                case 3:
                    orderingText[playerOrder[l].playerIndex].text = ((3 - l) + 1) + "st";
                    break;
                default:
                    break;
            }
        }
    }
}
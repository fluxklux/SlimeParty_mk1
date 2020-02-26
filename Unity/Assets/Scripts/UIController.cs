using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Fruit Text")]
    public GameObject[] playerTexts;

    [Header("Connection Panel")]
    public GameObject[] playerJoined;
    public GameObject[] playerNotJoined;
    [SerializeField] private GameObject connectionPanel = null;

    [Header("Minigames")]
    [SerializeField] private GameObject resultPanel = null;
    [SerializeField] private Text winnerText = null;
    [SerializeField] private Text loserText = null;
    [SerializeField] private GameObject masherInstructionPanel = null;

    [Header("Timer & Dpad")]
    [SerializeField] private GameObject dPadObject = null;
    [SerializeField] private GameObject timerObject = null;
    [SerializeField] private Text timerText = null;
    [SerializeField] private Image pizzaFill = null;

    [Header("Order Text")]
    public GameObject[] playerQueueText;

    private GameController gc;

    private void Start()
    {
        gc = GetComponent<GameController>();
    }

    public void UpdatePlayerFruits(int[] playerFruits)
    {
        for (int i = 0; i < playerTexts.Length; i++)
        {
            playerTexts[i].GetComponentInChildren<Text>().text = "P" + (i + 1) + ": " + playerFruits[i];
        }
    }

    public void TogglePlayerUi(int index)
    {
        playerTexts[index].SetActive(true);
        playerNotJoined[index].SetActive(false);
        playerJoined[index].SetActive(true);
    }

    public void ToggleDpad (bool onOff)
    {
        dPadObject.SetActive(onOff);
    }

    public void ToggleTimer (bool onOff)
    {
        timerObject.SetActive(onOff);
    }

    public void ToggleConnectionUi ()
    {
        connectionPanel.SetActive(false);
    }

    public void UpdateTimerText(float timerValue, float timerMax)
    {
        timerText.text = timerValue.ToString("F1");

        float calcFloat = timerValue / timerMax;
        pizzaFill.fillAmount = calcFloat;
    }

    public void ToggleMasherInstructions (bool onOff)
    {
        masherInstructionPanel.SetActive(onOff);
    }

    public void DisplayMinigameWinner (bool onOff)
    {
        resultPanel.SetActive(onOff);

        winnerText.text = "Winner: \n Player " + (1 + gc.GetComponent<MinigameController>().minigamePlayers[gc.GetComponent<MinigameController>().winner]).ToString("");
        loserText.text = "Loser: \n Player " + (1 + gc.GetComponent<MinigameController>().minigamePlayers[gc.GetComponent<MinigameController>().loser]).ToString("");
    }

    public void UpdateQueueOrderUi(bool onOff)
    {
        for (int i = 0; i < gc.queueObjects.Count; i++)
        {
            int playerIndex = gc.queueObjects[i].playerIndex;

            playerQueueText[playerIndex].GetComponentInChildren<Text>().text = (i + 1).ToString("F0");

            playerQueueText[playerIndex].SetActive(onOff);

        }
    }
}
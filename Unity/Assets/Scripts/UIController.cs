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

    [Header("Timer & Dpad")]
    [SerializeField] private GameObject dPadObject = null;
    [SerializeField] private GameObject timerObject = null;
    [SerializeField] private Text timerText = null;

    public void UpdatePlayerFruits(int[] playerFruits)
    {
        for (int i = 0; i < playerTexts.Length; i++)
        {
            playerTexts[i].GetComponentInChildren<Text>().text = "P" + i + ": " + playerFruits[i];
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

    public void UpdateTimerText(float timerValue)
    {
        timerText.text = timerValue.ToString("F2");
    }
}

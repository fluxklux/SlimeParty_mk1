using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Fruit Text")]
    [SerializeField] private GameObject[] playerTexts;

    [Header("Connection Panel")]
    [SerializeField] private GameObject[] playerJoined;
    [SerializeField] private GameObject[] playerNotJoined;
    [SerializeField] private GameObject connectionPanel;

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

    public void ToggleConnectionUi ()
    {
        connectionPanel.SetActive(false);
    }
}

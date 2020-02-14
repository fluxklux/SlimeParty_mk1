using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject[] playerTexts;

    public void UpdatePlayerFruits(int[] playerFruits)
    {
        for (int i = 0; i < playerTexts.Length; i++)
        {
            playerTexts[i].GetComponentInChildren<Text>().text = "P" + i + ": " + playerFruits[i];
        }
    }

    public void TogglePlayerUi(int index)
    {

    }

    public void ToggleConnectionUi (bool onOff)
    {

    }
}

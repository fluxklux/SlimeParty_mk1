using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject[] playerTexts;

    public void UpdatePlayerFruits(int[] playerFruits)
    {
        for (int i = 0; i < playerTexts.Length; i++)
        {
            //Väntar på Canvas Element
            //playerTexts[i].GetComponentInChildren<Text>().text = "P" + i + ": " + playerFruits[i];
        }
    }

    public void TogglePlayerUi(int index)
    {

    }
    
    public void ToggleConnectionUi (bool onOff)
    {

    }
}

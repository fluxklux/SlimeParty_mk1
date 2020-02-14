using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] allSlots;
    public Slot[] allSlotTypes;

    int[] playerFruits = { 0, 0, 0, 0 };

    UIController uc;

    private void Start()
    {
        uc = GetComponent<UIController>();
    }

    public void ChangeFruitAmount(int playerIndex, int amount)
    {
        playerFruits[playerIndex] += amount;

        if (playerFruits[playerIndex] < 0)
        {
            playerFruits[playerIndex] = 0;
        }
        uc.UpdatePlayerFruits(playerFruits);
    }
}

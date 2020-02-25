using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceController : MonoBehaviour
{
    int chance;

    int slotToTp;

    GameController gc;
    MoveController mc;

    private void Start()
    {
        gc = GetComponent<GameController>();
        mc = GetComponent<MoveController>();
    }

    public void RandomiseChance(int playerIndex)
    {

        chance = Random.Range(1, 4);

        switch (chance)
        {
            case 1:
                MoveToRandomSlot(playerIndex);
                break;
            case 2:
                gc.ChangeFruitAmount(playerIndex, 3);
                break;
            case 3:
                gc.ChangeFruitAmount(playerIndex, -3);
                break;
            case 4:
                gc.ChangeFruitAmount(playerIndex, 10);
                break;
            default:
                break;
        }
    }

    void MoveToRandomSlot(int playerIndex)
    {
        slotToTp = Random.Range(0, gc.allSlots.Length);

        int calcIndex = mc.players[gc.queueObjects[playerIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + slotToTp;
        calcIndex = (int)Mathf.Repeat(calcIndex, gc.allSlots.Length);

        mc.updatePlayerSlotPosition(calcIndex, playerIndex);
    }
}
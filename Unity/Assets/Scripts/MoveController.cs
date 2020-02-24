using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public GameObject[] players;

    GameController gc;
    UIController uc;

    public int selectedSlot = 0;
    public int selectedPlayer = 0;


    void Start()
    {
        gc = GetComponent<GameController>();
        uc = GetComponent<UIController>();
    }

    public void MovePlayers()
    {
        for (int i = 0; i <= gc.queueObjects.Count - 1; i++)
        {
            int playerIndex = gc.queueObjects[i].playerIndex;
            int calcIndex = players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + gc.queueObjects[i].steps;
            calcIndex = (int)Mathf.Repeat(calcIndex, gc.allSlots.Length);
            selectedSlot = calcIndex;
            selectedPlayer = playerIndex;

            Debug.Log("Now operating queue object " + i + " with " + gc.queueObjects[i].steps + " steps!");

           

            if (gc.queueObjects[i].steps >= 5)
            {
                EngageJump(calcIndex, i);
            }
            else
            {
                EngageRun(calcIndex, i);
            }

            players[playerIndex].GetComponent<PlayerController>().UpdateScale(gc.allSlots[20].transform.position.y);

        }
    }


    private void EngageRun(int calcIndex, int queueIndex)
    {
        for(int i = 0; i < gc.queueObjects[queueIndex].steps; i++)
        {
            int calcSlot = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + 1;
            calcSlot = (int)Mathf.Repeat(calcSlot, gc.allSlots.Length);
            int amountOfPlayers = 0;
            for (int j = 0; j < players.Length; j++)
            {
                if (players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition == calcSlot)
                {
                    amountOfPlayers++;
                }
            }

            switch (amountOfPlayers)
            {
                case 1:
                    updatePlayerSlotPosition(calcSlot, queueIndex);
                    break;
                case 2:
                    updatePlayerSlotPosition(calcSlot, queueIndex);
                    break;
                case 3:
                    updatePlayerSlotPosition(calcSlot, queueIndex);
                    break;
                case 0:
                    updatePlayerSlotPosition(calcSlot, queueIndex);
                    break;
                default:
                    break;
            }
        }
    }

    private void EngageJump(int calcIndex, int queueIndex)
    {
        int calcSlot = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + gc.queueObjects[queueIndex].steps;
        calcSlot = (int)Mathf.Repeat(calcSlot, gc.allSlots.Length);
        int amountOfPlayers = 0;
        for (int j = 0; j < players.Length; j++)
        {
            if (players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition == calcSlot)
            {
                amountOfPlayers++;
            }
        }

        switch (amountOfPlayers)
        {
            case 1:
                updatePlayerSlotPosition(calcSlot, queueIndex);
                break;
            case 2:
                updatePlayerSlotPosition(calcSlot, queueIndex);
                break;
            case 3:
                updatePlayerSlotPosition(calcSlot, queueIndex);
                break;
            case 0:
                updatePlayerSlotPosition(calcSlot, queueIndex);
                break;
            default:
                break;
        }
    }

    int checkSelectedSlot(int queueIndex)
    {
        return 1;
    }

    public void updatePlayerSlotPosition(int targetSlot, int queueIndex)
    {
        Debug.Log("Player " + gc.queueObjects[queueIndex].playerIndex + " is being moved to slot " + targetSlot + "!");
        players[gc.queueObjects[queueIndex].playerIndex].transform.position = gc.allSlots[targetSlot].transform.position;
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition = targetSlot;
    }
}

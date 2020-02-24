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
            amountOfPlayers = checkSelectedSlot(calcSlot);

            switch (amountOfPlayers)
            {
                case 1:
                    updatePlayerSlotPosition(calcSlot, queueIndex);
                    //offsetPlayers(1);
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

    private int checkSelectedSlot(int calcSlot)
    {
        int amountOfPlayers = 0;
        for (int j = 0; j < players.Length; j++)
        {
            if (players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition == calcSlot)
            {
                amountOfPlayers++;
            }
        }
        return amountOfPlayers;
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

    public void updatePlayerSlotPosition(int targetSlot, int queueIndex)
    {
        Debug.Log("Player " + gc.queueObjects[queueIndex].playerIndex + " is being moved to slot " + targetSlot + "!");
        players[gc.queueObjects[queueIndex].playerIndex].transform.position = gc.allSlots[targetSlot].transform.position;
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition = targetSlot;
    }

    
    void offsetPlayers(int caseNumber)
    {
        /*
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerController>().wasFirst == true)
            {
                if (checkSelectedSlot(players[i].GetComponent<PlayerController>().currentSlotPosition) > 1)
                {
                    selectedPlayer = i;
                    updatePlayerPosition(new Vector3(-0.25f, 0.25f), players[i].GetComponent<PlayerController>().currentSlotPosition);
                }
            }

            if (checkSelectedSlot(players[i].GetComponent<PlayerController>().lastSlotIndex) == 1)
            {
                for (int j = 0; j < players.Length; j++)
                {
                    if (players[j].GetComponent<PlayerController>().currentSlotPosition == players[i].GetComponent<PlayerController>().lastSlotIndex)
                    {
                        selectedPlayer = j;
                        updatePlayerPosition(new Vector3(0, 0), players[j].GetComponent<PlayerController>().currentSlotPosition);
                    }
                }
            }
        }
        */
    }

}

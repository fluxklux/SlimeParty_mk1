using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    public GameObject[] players;
    private int currentPlayerIndex;
    private GameController gc;

    void Start()
    {
        gc = GetComponent<GameController>();
    }

    public void MovePlayers()
    {
        for (int i = 0; i < gc.queueObjects.Count; i++)
        {
            currentPlayerIndex = gc.queueObjects[i].playerIndex;
            if (gc.queueObjects[i].steps >= 5)
            {
                Jump(i);
            }
            else
            {
                Run(i);
            }
        }
    }

    public void Jump(int queueIndex)
    {
        int calcSlot = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + gc.queueObjects[queueIndex].steps;
        calcSlot = (int)Mathf.Repeat(calcSlot, gc.allSlots.Length);
        Vector3 offset = CheckOtherPlayers(queueIndex, calcSlot);
        UpdatePlayerPositionPlayerIndex(calcSlot, gc.queueObjects[queueIndex].playerIndex, offset);
        gc.allSlots[calcSlot].GetComponent<SlotController>().TriggerSlotBehaviour(gc.queueObjects[queueIndex].playerIndex);
    }

    private void Run(int queueIndex)
    {
        for (int i = 1; i <= gc.queueObjects[queueIndex].steps; i++)
        {
            int calcSlot = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + 1;
            calcSlot = (int)Mathf.Repeat(calcSlot, gc.allSlots.Length);
            Vector3 offset = CheckOtherPlayers(queueIndex, calcSlot);
            UpdatePlayerPositionPlayerIndex(calcSlot, gc.queueObjects[queueIndex].playerIndex, offset);
            gc.allSlots[calcSlot].GetComponent<SlotController>().TriggerSlotBehaviour(gc.queueObjects[queueIndex].playerIndex);
        }
    }

    public Vector3 CheckOtherPlayers(int queueIndex, int calcSlot)
    {
        Vector3 offset = Vector3.zero;
        int amountOfPlayers = CheckSelectedSlot(calcSlot);
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.lastSlotOrder = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder;

        if (CheckSelectedSlot(players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition) > 1)
        {
            for (int j = 0; j < players.Length; j++)
            {
                if (players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition == players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition)
                {
                    Vector3 temp = Vector3.zero;
                    if (players[j].GetComponent<PlayerController>().playerVariable.currentSlotOrder > players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder)
                    {
                        players[j].GetComponent<PlayerController>().playerVariable.currentSlotOrder--;
                    }

                    switch (players[j].GetComponent<PlayerController>().playerVariable.currentSlotOrder)
                    {
                        case 1:
                            if (CheckSelectedSlot(players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition) > 2)
                            {
                                temp = new Vector3(-0.25f, 0.25f);
                            }
                            else
                            {
                                temp = new Vector3(0, 0);
                            }
                            break;
                        case 2:
                            temp = new Vector3(0.25f, 0.25f);
                            break;
                        case 3:
                            temp = new Vector3(-0.25f, -0.25f);
                            break;
                        case 4:
                            temp = new Vector3(0.25f, -0.25f);
                            break;
                        default:
                            break;
                    }
                    UpdatePlayerPositionPlayerIndex(players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition, j, temp);
                }
            }
        }

        switch (amountOfPlayers)
        {
            case 1:
                offset = new Vector3(0.25f, 0.25f);
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst = false;
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 2;
                for (int j = 0; j < players.Length; j++)
                {
                    if (players[j].GetComponent<PlayerController>().playerVariable.wasFirst == true && players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition == calcSlot)
                    {
                        Vector3 temp = new Vector3(-0.25f, 0.25f);
                        UpdatePlayerPositionPlayerIndex(players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition, j, temp); //OVANST�ENDE
                    }
                }
                break;
            case 2:
                offset = new Vector3(-0.25f, -0.25f);
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst = false;
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 3;
                break;
            case 3:
                offset = new Vector3(0.25f, -0.25f);
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst = false;
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 4;
                break;
            case 0:
                offset = new Vector3(0, 0);
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst = true;
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 1;
                break;
            default:
                break;
        }
        return offset;
    }

    private int CheckSelectedSlot(int calcSlot)
    {
        int amountOfPlayers = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerController>().playerVariable.currentSlotPosition == calcSlot)
            {
                amountOfPlayers++;
            }
        }
        return amountOfPlayers;
    }

    public void UpdatePlayerPositionPlayerIndex(int targetedSlot, int playerIndex, Vector3 offset)
    {
        players[playerIndex].GetComponent<PlayerController>().Lerp(playerIndex, gc.allSlots[targetedSlot].transform.position + offset);
        players[playerIndex].GetComponent<PlayerController>().UpdatePosition(targetedSlot);
    }
}
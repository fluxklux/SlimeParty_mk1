using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public GameObject[] players;
    public float damping;

    private GameController gc;
    private UIController uc;

    public int selectedSlot = 0;
    public int selectedPlayer = 0;

    private void Start()
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

            //Debug.Log("Now operating queue object " + i + " with " + gc.queueObjects[i].steps + " steps!");
            
            if (gc.queueObjects[i].steps >= 5)
            {
                EngageJump(calcIndex, i);
            }
            else
            {
                EngageRun(calcIndex, i);
            }

            //players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().Lerp(gc.queueObjects[i].playerIndex, gc.allSlots[players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition].transform);
            //players[playerIndex].GetComponent<PlayerController>().UpdateScale(gc.allSlots[20].transform.position.y);
        }

        OffsetPlayers();
    }

    private int CheckSelectedSlot(int calcSlot)
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

    private void EngageRun(int calcIndex, int queueIndex)
    {
        for(int i = 0; i < gc.queueObjects[queueIndex].steps; i++)
        {
            int calcSlot = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + 1;
            calcSlot = (int)Mathf.Repeat(calcSlot, gc.allSlots.Length);
            int amountOfPlayers = 0;
            amountOfPlayers = CheckSelectedSlot(calcSlot);

            switch (amountOfPlayers)
            {
                case 1:
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 2;
                    UpdatePlayerSlotPosition(calcSlot, queueIndex);
                    
                    break;
                case 2:
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 3;
                    UpdatePlayerSlotPosition(calcSlot, queueIndex);
                    break;
                case 3:
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 4;
                    UpdatePlayerSlotPosition(calcSlot, queueIndex);
                    break;
                case 0:
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 1;
                    UpdatePlayerSlotPosition(calcSlot, queueIndex);
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
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 2;
                UpdatePlayerSlotPosition(calcSlot, queueIndex);
                break;
            case 2:
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 3;
                UpdatePlayerSlotPosition(calcSlot, queueIndex);
                break;
            case 3:
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 4;
                UpdatePlayerSlotPosition(calcSlot, queueIndex);
                break;
            case 0:
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 1;
                UpdatePlayerSlotPosition(calcSlot, queueIndex);
                break;
            default:
                break;
        }
    }

    public void UpdatePlayerSlotPosition(int targetSlot, int queueIndex)
    {
        //TODO: I ENGAGERUN SKA DEN GÅ ETT STEG I TAGET. NU ÄR FORLOOPEN SNABBARE ÄN LERPEN SÅ HAN GÅR DIREKT TILL SISTA POSITIONEN.
        //players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().Lerp(gc.queueObjects[queueIndex].playerIndex, gc.allSlots[targetSlot].transform);

        //players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().Lerp(gc.queueObjects[queueIndex].playerIndex, gc.allSlots[players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition].transform);
        //Debug.Log("Player " + gc.queueObjects[queueIndex].playerIndex + " is being moved to slot " + targetSlot + "!");
        selectedSlot = targetSlot;
        //players[gc.queueObjects[queueIndex].playerIndex].transform.position = gc.allSlots[targetSlot].transform.position;
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().UpdatePosition(targetSlot);
        gc.allSlots[targetSlot].GetComponent<SlotController>().TriggerSlotBehaviour(gc.queueObjects[queueIndex].playerIndex);
    }

    void OffsetPlayers()
    {
        for (int i = 0; i <= gc.queueObjects.Count - 1; i++) //SKA LOOPA SPELARE SENARE
        {

            if (CheckSelectedSlot(players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().playerVariable.lastSlotIndex) >= 1)
            {
                for (int j = 0; j < players.Length - 1; j++)
                {
                    if (players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition == players[i].GetComponent<PlayerController>().playerVariable.lastSlotIndex)
                    {
                        players[gc.queueObjects[j].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder--; //DENNA LOOPAR QUEUE ARRAYEN UTIFRÅN ALLA SPELARE, GER FEL!
                    }
                }
            }

            Debug.Log(players[gc.queueObjects[i].playerIndex] + " has the slot order " + players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder);

            switch (players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder)
            {
                case 1:
                    if (CheckSelectedSlot(players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition) > 1)
                    {
                        MoveToOffset(i, new Vector3(-0.25f, 0.25f));
                    }
                    else if (CheckSelectedSlot(players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition) == 1)
                    {
                        MoveToOffset(i, new Vector3(0, 0));
                    }
                    break;
                case 2:
                    MoveToOffset(i, new Vector3(0.25f, 0.25f));
                    break;
                case 3:
                    MoveToOffset(i, new Vector3(-0.25f, -0.25f));
                    break;
                case 4:
                    MoveToOffset(i, new Vector3(0.25f, -0.25f));
                    break;
                default:
                    break;
            }

        }

        //:( :)

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

    void MoveToOffset(int queueIndex, Vector3 offsetValue)
    {
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().Lerp(gc.queueObjects[queueIndex].playerIndex, gc.allSlots[selectedSlot].transform.position + offsetValue);
        //players[gc.queueObjects[queueIndex].playerIndex].transform.position += offsetValue;
    }
}
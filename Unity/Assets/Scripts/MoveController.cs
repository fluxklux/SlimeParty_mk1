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

            Debug.Log("Now operating queue object " + i + " with " + gc.queueObjects[i].steps + " steps!");

           

            if (gc.queueObjects[i].steps >= 5)
            {
                EngageJump(calcIndex, i);
            }
            else
            {
                EngageRun(calcIndex, i);
            }

            //players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().Lerp(gc.queueObjects[i].playerIndex, gc.allSlots[players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition].transform);
            players[playerIndex].GetComponent<PlayerController>().UpdateScale(gc.allSlots[20].transform.position.y);

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
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 2;
                    updatePlayerSlotPosition(calcSlot, queueIndex);
                    //offsetPlayers(1);
                    break;
                case 2:
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 3;
                    updatePlayerSlotPosition(calcSlot, queueIndex);
                    break;
                case 3:
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 4;
                    updatePlayerSlotPosition(calcSlot, queueIndex);
                    break;
                case 0:
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 1;
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

    public void updatePlayerSlotPosition(int targetSlot, int queueIndex)
    {
        //TODO: I ENGAGERUN SKA DEN GÅ ETT STEG I TAGET. NU ÄR FORLOOPEN SNABBARE ÄN LERPEN SÅ HAN GÅR DIREKT TILL SISTA POSITIONEN.
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().Lerp(gc.queueObjects[queueIndex].playerIndex, gc.allSlots[targetSlot].transform);

        //players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().Lerp(gc.queueObjects[queueIndex].playerIndex, gc.allSlots[players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition].transform);
        //Debug.Log("Player " + gc.queueObjects[queueIndex].playerIndex + " is being moved to slot " + targetSlot + "!");
        //players[gc.queueObjects[queueIndex].playerIndex].transform.position = gc.allSlots[targetSlot].transform.position;
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition = targetSlot;
    }

    
    void offsetPlayers(int caseNumber)
    {
        for (int i = 0; i <= gc.queueObjects.Count - 1; i++)
        {
            if (players[gc.queueObjects[i].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder == 1)
            {
                if (checkSelectedSlot(players[i].GetComponent<PlayerController>().playerVariable.currentSlotPosition) > 1)
                {
                    MoveToOffset(i, new Vector3(-0.25f, 0.25f));
                }
            }

            if(checkSelectedSlot(players[i].GetComponent<PlayerController>().playerVariable.lastSlotIndex) == 1)
            {
                for (int j = 0; j < gc.queueObjects.Count - 1; j++)
                {
                    if (players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition == players[i].GetComponent<PlayerController>().playerVariable.lastSlotIndex)
                    {
                        MoveToOffset(j, new Vector3(0, 0));
                    }
                }
            }
        }
        //:(
        //GÖR EN ARRAY MED ALLA VÄRDEN SOM ALLA minus ettas OM LASTSLOTINDEX VAR ÖVER ETT (=>) VID EN MOVE??

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
        players[gc.queueObjects[queueIndex].playerIndex].transform.position += offsetValue;
    }
}
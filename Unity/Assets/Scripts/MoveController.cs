/*
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
            players[playerIndex].GetComponent<PlayerController>().UpdateScale(gc.allSlots[20].transform.position.y);
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
                        //players[gc.queueObjects[j].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder--; //DENNA LOOPAR QUEUE ARRAYEN UTIFRÅN ALLA SPELARE, GER FEL!
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
    }

    void MoveToOffset(int queueIndex, Vector3 offsetValue)
    {
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().Lerp(gc.queueObjects[queueIndex].playerIndex, gc.allSlots[players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition].transform.position + offsetValue);
    }
}
*/

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
            Debug.Log("MovePlayers, looping for queueIndex " + i);
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

    private void Jump(int queueIndex)
    {
        Debug.Log("K�r jump!");
        int calcSlot = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + gc.queueObjects[queueIndex].steps;
        Debug.Log("...med " + gc.queueObjects[queueIndex].steps + " steps!");
        calcSlot = (int)Mathf.Repeat(calcSlot, gc.allSlots.Length);
        Vector3 offset = Vector3.zero;
        int amountOfPlayers = CheckSelectedSlot(calcSlot);
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.lastSlotOrder = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder;

        //uppdatera spelaren som man ankommer till SAMT uppdatera sig sj�lv according till m�ngden av dem
        switch (amountOfPlayers)
        {
            case 1:
                offset = new Vector3(0.25f, 0.25f);
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst = false;
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 2;
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].GetComponent<PlayerController>().playerVariable.wasFirst == true && players[i].GetComponent<PlayerController>().playerVariable.currentSlotPosition == calcSlot)
                    {
                        Vector3 temp = new Vector3(-0.25f, 0.25f);
                        UpdatePlayerPositionPlayerIndex(players[i].GetComponent<PlayerController>().playerVariable.currentSlotPosition, i, temp); //OVANST�ENDE
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

        //uppdaterade l�mnade spelare
        if (CheckSelectedSlot(players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition) > 1)
        {
            Debug.Log("The moving player's slot has more than one player on it!");
            for (int i = 0; i < players.Length; i++)
            {
                Debug.Log("Now rolling player " + i);
                if (players[i].GetComponent<PlayerController>().playerVariable.currentSlotPosition == players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition
                    && players[i].GetComponent<PlayerController>().playerVariable.currentSlotOrder > players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder)
                {
                    Debug.Log("Player " + i + " has a higher slotOrder than the moving player, specifically it's" + players[i].GetComponent<PlayerController>().playerVariable.currentSlotOrder);
                    players[i].GetComponent<PlayerController>().playerVariable.currentSlotOrder--;
                    Debug.Log("Player " + i + " has lowered their slotOrder since a player is about to move that is placed in front of it! Now " + players[i].GetComponent<PlayerController>().playerVariable.currentSlotOrder);
                    Vector3 temp = Vector3.zero;
                    switch (players[i].GetComponent<PlayerController>().playerVariable.currentSlotOrder)
                    {
                        case 1:
                            Debug.Log("The player with a lowered slotOrder has been placed in case 1");
                            if (CheckSelectedSlot(players[i].GetComponent<PlayerController>().playerVariable.currentSlotPosition) > 1)
                            {
                                temp = new Vector3(-0.25f, 0.25f);
                            }
                            else
                            {
                                temp = new Vector3(0, 0);
                            }
                            break;
                        case 2:
                            Debug.Log("The player with a lowered slotOrder has been placed in case 2");
                            temp = new Vector3(0.25f, 0.25f);
                            break;
                        case 3:
                            Debug.Log("The player with a lowered slotOrder has been placed in case 3");
                            temp = new Vector3(-0.25f, -0.25f);
                            break;
                        case 4:
                            Debug.Log("The player with a lowered slotOrder has been placed in case 4");
                            temp = new Vector3(0.25f, -0.25f);
                            break;
                        default:
                            break;
                    }
                    Debug.Log("Now updating the position and lerping to the aquired offset " + temp + " of player" + i);
                    UpdatePlayerPositionPlayerIndex(players[i].GetComponent<PlayerController>().playerVariable.currentSlotPosition, i, temp); //ANLEDNINGEN TILL NY FUNKTION: TARGET M�STE EJ VARA I QUEUE. DETTA KAN OCKS� VARA SANT F�R OVANST�ENDE???
                }
            }
        }
        Debug.Log("NoW updating the position and lerping to the aquired offset " + offset + " of player" + gc.queueObjects[queueIndex].playerIndex);
        UpdatePlayerPositionPlayerIndex(calcSlot, gc.queueObjects[queueIndex].playerIndex, offset);
        gc.allSlots[calcSlot].GetComponent<SlotController>().TriggerSlotBehaviour(gc.queueObjects[queueIndex].playerIndex);
    }

    private void Run(int queueIndex)
    {
        for (int i = 1; i <= gc.queueObjects[queueIndex].steps; i++)
        {
            int calcSlot = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + 1;
            calcSlot = (int)Mathf.Repeat(calcSlot, gc.allSlots.Length);
            Vector3 offset = Vector3.zero;
            int amountOfPlayers = CheckSelectedSlot(calcSlot);
            players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.lastSlotOrder = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder;

            switch (amountOfPlayers)
            {
                case 1:
                    Debug.Log("1 player on the slot you're about to move to!");
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
                    Debug.Log("2 player on the slot you're about to move to!");
                    offset = new Vector3(-0.25f, -0.25f);
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst = false;
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 3;
                    break;
                case 3:
                    Debug.Log("3 player on the slot you're about to move to!");
                    offset = new Vector3(0.25f, -0.25f);
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst = false;
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 4;
                    break;
                case 0:
                    Debug.Log("NO players on the slot you're about to move to!");
                    offset = new Vector3(0, 0);
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst = true;
                    players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 1;
                    break;
                default:
                    break;
            }

            if (CheckSelectedSlot(players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition) > 1)
            {
                for (int j = 0; j < players.Length; j++)
                {
                    Debug.Log("Checking player number " + j + "...");
                    if (players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition == players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition
                    && players[j].GetComponent<PlayerController>().playerVariable.currentSlotOrder > players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder)
                    {
                        Debug.Log("Player " + j + " is on the slot that is about to be left & has a higher slotOrder"); //FORTSÄTT ATT DEBUGGA FRAMMÅT EFTER DENNA RADEN KOD, ALLT BAKOM FUNGERAR SOM DET SKA! SPELARE SOM LÄMNAS, OM ENSAMMA, SÄTTS INTE TILL INGEN OFFSET
                        players[j].GetComponent<PlayerController>().playerVariable.currentSlotOrder--;
                        Vector3 temp = Vector3.zero;

                        switch (players[j].GetComponent<PlayerController>().playerVariable.currentSlotOrder)
                        {
                            case 1:
                                if (CheckSelectedSlot(players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition) > 1)
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
            UpdatePlayerPositionPlayerIndex(calcSlot, gc.queueObjects[queueIndex].playerIndex, offset);
            gc.allSlots[calcSlot].GetComponent<SlotController>().TriggerSlotBehaviour(gc.queueObjects[queueIndex].playerIndex);
        }
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

    public void UpdatePlayerPosition(int targetedSlot, int queueIndex, Vector3 offset)
    {
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().Lerp(gc.queueObjects[queueIndex].playerIndex, gc.allSlots[targetedSlot].transform.position + offset);
        players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().UpdatePosition(targetedSlot);
    }

    public void UpdatePlayerPositionPlayerIndex(int targetedSlot, int playerIndex, Vector3 offset)
    {
        players[playerIndex].GetComponent<PlayerController>().Lerp(playerIndex, gc.allSlots[targetedSlot].transform.position + offset);
        players[playerIndex].GetComponent<PlayerController>().UpdatePosition(targetedSlot);
    }
}
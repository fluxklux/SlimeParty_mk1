using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{

    public GameObject[] players;
    private GameController gc;
    private TimerController tc;

    private float timer = 0;
    private float runTimer = 0;

    private float playerWaitTime;
    private float runWaitTime = 0.08f;

    private bool waitingBetweenPlayer = false;
    private bool waitingBetweenRun = false;

    public int globalSequencer = 0;
    public int runSequencer = 1;

    private AudioController ac;

    void Start()
    {
        gc = GetComponent<GameController>();

        ac = GetComponent<AudioController>();
    }

    public void MovePlayers() //Ändra DAMPING i PlayerController beroende på mängden queue objects, tillsammans med detta addera (någon slags) waiting-system för ett en effektiv dynamisk tid
    {
        CalculateTimings();
        globalSequencer = 0;
        if (gc.queueObjects.Count > 0)
        {
            LoopPlayer(0);
        }
    }

    public void CalculateTimings()
    {
        playerWaitTime = 0f;
        switch (gc.queueObjects.Count)
        {
            case 1:
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<PlayerController>().damping = 0.09f;
                }
                playerWaitTime = 1.4f;
                break;
            case 2:
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<PlayerController>().damping = 0.1f;
                }
                playerWaitTime = 1.4f;
                break;
            case 3:
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<PlayerController>().damping = 0.12f;
                }
                playerWaitTime = 1f;
                break;
            case 4:
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<PlayerController>().damping = 0.14f;
                }
                playerWaitTime = 1f;
                break;
            case 0:
                break;
            default:
                break;
        }
    }

    private void LoopPlayer(int queueIndex)
    {
        if (globalSequencer < gc.queueObjects.Count)
        {
            if (gc.queueObjects[queueIndex].steps >= 5)
            {
                Jump(queueIndex);
            }
            else
            {
                Run(queueIndex);
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
        waitingBetweenPlayer = true;
        globalSequencer++;

        ac.PlaySound(SoundEnum.jumpSound);
    }

    private void Run(int queueIndex)
    {
        if (runSequencer <= gc.queueObjects[queueIndex].steps)
        {
            Step(queueIndex, runSequencer);
        }
        else
        {
            runSequencer = 1;
            globalSequencer++;
            //LoopPlayer(globalSequencer);
            waitingBetweenPlayer = true;

            ac.PlaySound(SoundEnum.walkSound);
        }

        //sluta snacka östgötska Carl!

        /*
        int calcSlot = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + 1;
        calcSlot = (int)Mathf.Repeat(calcSlot, gc.allSlots.Length);
        Vector3 offset = CheckOtherPlayers(queueIndex, calcSlot);
        UpdatePlayerPositionPlayerIndex(calcSlot, gc.queueObjects[queueIndex].playerIndex, offset);
        gc.allSlots[calcSlot].GetComponent<SlotController>().TriggerSlotBehaviour(gc.queueObjects[queueIndex].playerIndex);
        waitingBetweenRun = true;
        */


    }

    private void Step(int queueIndex, int i)
    {
        int calcSlot = players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + 1;
        calcSlot = (int)Mathf.Repeat(calcSlot, gc.allSlots.Length);
        Vector3 offset = CheckOtherPlayers(queueIndex, calcSlot);
        UpdatePlayerPositionPlayerIndex(calcSlot, gc.queueObjects[queueIndex].playerIndex, offset);
        gc.allSlots[calcSlot].GetComponent<SlotController>().TriggerSlotBehaviour(gc.queueObjects[queueIndex].playerIndex);
        runSequencer++;
        waitingBetweenRun = true;
    }

    private void Update()
    {
        if (waitingBetweenPlayer)
        {
            timer += Time.deltaTime;
            if (timer >= playerWaitTime)
            {
                timer = 0;
                waitingBetweenPlayer = false;
                if (globalSequencer < gc.queueObjects.Count)
                {
                    LoopPlayer(globalSequencer);
                }
            }
        }

        if (waitingBetweenRun)
        {
            runTimer += Time.deltaTime;
            if (runTimer >= runWaitTime)
            {
                runTimer = 0;
                waitingBetweenRun = false;
                Run(globalSequencer);
            }
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
                                temp = new Vector3(-0.2f, 0.2f);
                            }
                            else
                            {
                                temp = new Vector3(0, 0);
                            }
                            break;
                        case 2:
                            temp = new Vector3(0.2f, 0.2f);
                            break;
                        case 3:
                            temp = new Vector3(-0.2f, -0.2f);
                            break;
                        case 4:
                            temp = new Vector3(0.2f, -0.2f);
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
                offset = new Vector3(0.2f, 0.2f);
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst = false;
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 2;
                for (int j = 0; j < players.Length; j++)
                {
                    if (players[j].GetComponent<PlayerController>().playerVariable.wasFirst == true && players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition == calcSlot)
                    {
                        Vector3 temp = new Vector3(-0.2f, 0.2f);
                        UpdatePlayerPositionPlayerIndex(players[j].GetComponent<PlayerController>().playerVariable.currentSlotPosition, j, temp); //OVANST?ENDE
                    }
                }
                break;
            case 2:
                offset = new Vector3(-0.2f, -0.2f);
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.wasFirst = false;
                players[gc.queueObjects[queueIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotOrder = 3;
                break;
            case 3:
                offset = new Vector3(0.2f, -0.2f);
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
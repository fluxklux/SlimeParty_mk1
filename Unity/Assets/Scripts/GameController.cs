using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QueueObject
{
    public int playerIndex; //player index, p1 = 0, p2 = 1 .....
    public int steps; //amount of steps forward
}

public class GameController : MonoBehaviour
{
    public GameObject[] allSlots;
    public Slot[] allSlotTypes;

    public List<QueueObject> queueObjects = new List<QueueObject>();
    public bool queueFinished = true;

    int[] playerFruits = { 0, 0, 0, 0 };

    UIController uc;
    InputController ic;
    Dpad dpad;


    private void Start()
    {
        uc = GetComponent<UIController>();
        ic = GetComponent<InputController>();
        dpad = GetComponent<Dpad>();
    }


    public void HandleQueueInputs(int indexedPlayer, int dpadIndex)
    {
        AddToQueue(indexedPlayer, dpad.GetDPadNum(dpadIndex));
        ic.hasPressedKey[indexedPlayer] = true;
    }

    public void AddToQueue(int playerIndex, int steps)
    {
        QueueObject newQueueObject = new QueueObject();
        newQueueObject.playerIndex = playerIndex;
        newQueueObject.steps = steps;

        queueObjects.Add(newQueueObject);

        //Fortsätt arbeta och implementera QUEUE SYSTEMET genom state machines. Alla nödvändiga funktioner bör finnas i GC, men de ska kallas från respektive STATES när de behövs. Gör diagram
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

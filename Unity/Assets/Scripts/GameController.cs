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

    [HideInInspector] public int[] playerFruits = { 0, 0, 0, 0 };

    UIController uc;
    InputController ic;
    Dpad dpad;
    AudioController ac;
    MoveController mc;
    RoundController rc;

    public bool[] turnSkipped = { false, false, false, false };

    private void Awake()
    {
        uc = GetComponent<UIController>();
        ic = GetComponent<InputController>();
        dpad = GetComponent<Dpad>();
        ac = GetComponent<AudioController>();
        mc = GetComponent<MoveController>();
        rc = GetComponent<RoundController>();
    }

    public void HandleQueueInputs(int indexedPlayer, int dpadIndex)
    {
        AddToQueue(indexedPlayer, dpad.GetDPadNum(dpadIndex));
        ic.hasPressedKey[indexedPlayer] = true;
    }

    public void AddToQueue(int playerIndex, int steps)
    {
        //Debug.Log("Adding player " + playerIndex + " to the queue with " + steps + " steps");
        QueueObject newQueueObject = new QueueObject();
        newQueueObject.playerIndex = playerIndex;
        newQueueObject.steps = steps;

        queueObjects.Add(newQueueObject);

        uc.UpdateQueueOrderUi(true);
    }

    public void CycleQueue()
    {

    }

    public void ResetQueue()
    {
        queueObjects.Clear();

        for (int i = 0; i < ic.hasPressedKey.Length; i++)
        {
            ic.hasPressedKey[i] = false;

            if (turnSkipped[i] == true)
            {
                ic.allPlayers[i].GetComponent<PlayerController>().playerVariable.skip = false;
                ic.hasPressedKey[i] = false;
                turnSkipped[i] = false;
                ic.allPlayers[i].GetComponent<PlayerController>().RemoveStun();
            }
            else if (ic.allPlayers[i].GetComponent<PlayerController>().playerVariable.skip == true)
            {
                ic.hasPressedKey[i] = true;

                turnSkipped[i] = true;
            }
        }
    }

    public void ChangeFruitAmount(int playerIndex, int amount)
    {
        playerFruits[playerIndex] += amount;

        if (playerFruits[playerIndex] < 0)
        {
            playerFruits[playerIndex] = 0;
        }

        if (mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.actionType == ActionType.PlusFruit3)
        {
            ac.PlaySound(SoundEnum.plusThreeSound);
        }

        if (mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.actionType == ActionType.MinusFruit3)
        {
            ac.PlaySound(SoundEnum.minusThreeSound);
        }

        if (mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.actionType == ActionType.PlusFruit10)
        {
            ac.PlaySound(SoundEnum.plusTenSound);
        }

        rc.SortingPlayer();

        uc.UpdatePlayerFruits(playerFruits);
    }
}
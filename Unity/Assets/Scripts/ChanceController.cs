using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceController : MonoBehaviour
{
    public GameObject fruitBag;

    [HideInInspector]
    public List<int> threePlusSlots = new List<int>();
    private int slotToTp;
    private int slotToPlace;
    private Vector2 slotPos;

    private GameController gc;
    private MoveController mc;
    private InputController ic;

    AudioController ac;

    private void Start()
    {
        gc = GetComponent<GameController>();
        mc = GetComponent<MoveController>();
        ic = GetComponent<InputController>();

        ac = GetComponent<AudioController>();

        AddPlusThreeSlotsToRange();
        //Debug.Log("There is " + threePlusSlots.Count + " plusThree slots on the board");
    }

    public void RandomiseChance(int playerIndex)
    {
        int chance;

        chance = Random.Range(1, 4);

        switch (chance)
        {
            case 1:
                //bra effekt, 3 food bags som ger +10 ENBART PÅ PLUS SLOT
                PlaceRandomBags();
                break;
            case 2:
                //Neutral effekt
                MoveToRandomSlot(playerIndex);
                break;
            case 3:
                //Dålig effekt, förlora sin tur
                SkipPlayerTurn(playerIndex);
                break;
            default:
                break;
        }
    }

    void AddPlusThreeSlotsToRange()
    {
        for (int i = 0; i < gc.allSlots.Length; i++)
        {
            if (gc.allSlots[i].GetComponent<SlotController>().currentSlot.slotType == SlotType.plusThree)
            {
                threePlusSlots.Add(i);
            }
        }
    }

    void GetSlotVector(int listIndex)
    {
        slotPos = gc.allSlots[threePlusSlots[listIndex]].transform.position;
    }

    void MoveToRandomSlot(int index)
    {
        slotToTp = Random.Range(0, gc.allSlots.Length);

        //Debug.Log("about to TELEPORT with playerindex: " + index);
        int calcIndex = mc.players[index].GetComponent<PlayerController>().playerVariable.currentSlotPosition + slotToTp;
        int slotSteps = calcIndex - mc.players[index].GetComponent<PlayerController>().playerVariable.currentSlotPosition;
        if (slotSteps < 0)
        {
            slotSteps *= -1;
        }
        for (int i = 0; i < gc.queueObjects.Count; i++)
        {
            if (gc.queueObjects[i].playerIndex == index)
            {
                gc.queueObjects[i].steps = slotSteps;
                mc.Jump(i);
            }
        }
    }

    void PlaceRandomBags()
    {
        for (int i = 0; i < 3; i++)
        {
            slotToPlace = Random.Range(0, threePlusSlots.Count);

            GetSlotVector(slotToPlace);

            
            if (gc.allSlots[threePlusSlots[slotToPlace]].GetComponent<SlotController>().hasBag == true)
            {
                slotToPlace = Random.Range(0, threePlusSlots.Count);
            }
            else
            {
                GameObject instance = Instantiate(fruitBag, slotPos, Quaternion.identity);
                gc.allSlots[threePlusSlots[slotToPlace]].GetComponent<SlotController>().hasBag = true;
                gc.allSlots[threePlusSlots[slotToPlace]].GetComponent<SlotController>().fruitBagObject = instance;
            }

            //Debug.Log("placed random bag on slot " + slotToPlace);
        }
    }

    void SkipPlayerTurn(int playerIndex)
    {
        if (gc.turnSkipped[playerIndex] == false)
        {
            ic.allPlayers[playerIndex].GetComponent<PlayerController>().playerVariable.skip = true;
        }  
        //Debug.Log("player " + (playerIndex  + 1)+ " lost a turn!");
    }
}
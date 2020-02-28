using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceController : MonoBehaviour
{
    int slotToTp;
    int slotToPlace;

    GameController gc;
    MoveController mc;
    InputController ic;

    public GameObject fruitBag;

    public List<GameObject> threePlusSlots = new List<GameObject>();

    Vector2 slotPos;

    private void Start()
    {
        gc = GetComponent<GameController>();
        mc = GetComponent<MoveController>();
        ic = GetComponent<InputController>();

        AddPlusThreeSlotsToRange();
        Debug.Log("There is " + threePlusSlots.Count + " plusThree slots on the board");
    }

    public void RandomiseChance(int playerIndex)
    {
        int chance;

        chance = Random.Range(1, 3);

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
                threePlusSlots.Add(gc.allSlots[i]);
            }
        }
    }

    void GetSlotVector(int listIndex)
    {
        slotPos = threePlusSlots[listIndex].gameObject.transform.position;
    }

    void MoveToRandomSlot(int playerIndex)
    {
        slotToTp = Random.Range(0, gc.allSlots.Length);

        int calcIndex = mc.players[gc.queueObjects[playerIndex].playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + slotToTp;
        calcIndex = (int)Mathf.Repeat(calcIndex, gc.allSlots.Length);

        mc.UpdatePlayerPositionPlayerIndex(calcIndex, gc.queueObjects[playerIndex].playerIndex, Vector3.zero); //OM CHANS FLYTTAR SPELARE TILL ETT RANDOM SLUT SÅ RÄKNAR DENNA INTE UT ORDERN ÄNNU, EFTERSOM DETTA GÖRS TIDIGARE I MOVE SKRIPTET
    }

    void PlaceRandomBags()
    {
        for (int i = 0; i < 3; i++)
        {
            slotToPlace = Random.Range(0, threePlusSlots.Count);

            GetSlotVector(slotToPlace);

            GameObject instance = Instantiate(fruitBag, slotPos, Quaternion.identity);

            //threePlusSlots[slotToPlace].GetComponent<SlotController>().hasBag = true;

            Debug.Log("placed random bag on slot " + slotToPlace);
        }
    }

    void SkipPlayerTurn(int playerIndex)
    {
        if (gc.turnSkipped[playerIndex] == false)
        {
            ic.allPlayers[playerIndex].GetComponent<PlayerController>().playerVariable.skip = true;
        }  
        Debug.Log("player " + playerIndex + " lost a turn!");
    }
}
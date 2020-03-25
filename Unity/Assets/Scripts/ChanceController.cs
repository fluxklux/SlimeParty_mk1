using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceController : MonoBehaviour
{
    public GameObject fruitBag;

    [SerializeField] private GameObject bagEffect = null; 

    [HideInInspector]
    public List<int> threePlusSlots = new List<int>();
    private int slotToTp;
    private int slotToPlace;
    private Vector2 slotPos;

    private GameController gc;
    private MoveController mc;
    private InputController ic;
    private AudioController ac;
    private PlayerController pc;

    public GameObject stunEffect;

    GameObject effect;

    private void Start()
    {
        gc = GetComponent<GameController>();
        mc = GetComponent<MoveController>();
        ic = GetComponent<InputController>();
        ac = GetComponent<AudioController>();

        AddPlusThreeSlotsToRange();
    }

    public void RandomiseChance(int playerIndex)
    {
        pc = Object.FindObjectOfType<PlayerController>();

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

    public void UpdateBagScale(GameObject objectTransform)
    {
        float maxY = 5;

        float yPos = objectTransform.transform.position.y;

        float differents;

        differents = maxY - yPos;

        Vector3 minScale = new Vector3(0.7f, 0.7f, 0.7f);

        Vector3 maxScale = new Vector3(1f, 1f, 1f);

        float bagScale;

        bagScale = Mathf.Lerp(minScale.y, maxScale.y, differents - 4);

        objectTransform.transform.localScale = new Vector3(bagScale, bagScale, 0.0f);
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

        int calcIndex = mc.players[index].GetComponent<PlayerController>().playerVariable.currentSlotPosition + slotToTp;
        calcIndex = (int)Mathf.Repeat(calcIndex, gc.allSlots.Length);

        int slotSteps = calcIndex - mc.players[index].GetComponent<PlayerController>().playerVariable.currentSlotPosition;

        //Debug.Log(calcIndex);
        gc.allSlots[calcIndex].GetComponent<SlotController>().hasEffect = true;
        Debug.Log("HAR DU FEL I KODEN? DETTA KAN VARA PROBLEMET");

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

        ac.PlaySound(SoundEnum.tpPlayerSound);
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
                GameObject instanceBag = Instantiate(bagEffect, slotPos, Quaternion.identity);
                UpdateBagScale(instance);
                gc.allSlots[threePlusSlots[slotToPlace]].GetComponent<SlotController>().hasBag = true;
                gc.allSlots[threePlusSlots[slotToPlace]].GetComponent<SlotController>().fruitBagObject = instance;
            }
            ac.PlaySound(SoundEnum.placeBagSound);
        }
    }

    void SkipPlayerTurn(int playerIndex)
    {
        if (gc.turnSkipped[playerIndex] == false)
        {
            ic.allPlayers[playerIndex].GetComponent<PlayerController>().playerVariable.skip = true;
        }

        effect = Instantiate(stunEffect) as GameObject;
        effect.transform.position = new Vector3(
                ic.allPlayers[playerIndex].transform.position.x,
                ic.allPlayers[playerIndex].transform.position.y + 0.5f, 0);


        effect.transform.parent = ic.allPlayers[playerIndex].transform;

        ac.PlaySound(SoundEnum.loseTurnSound);
    }
}
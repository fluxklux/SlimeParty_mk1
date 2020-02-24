using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    public Slot currentSlot;

    GameObject gameController;
    GameController gc;
    UIController uc;
    //MinigameController mgc;
    new SpriteRenderer renderer;

    ChanceController cc;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");

        renderer = GetComponentInChildren<SpriteRenderer>();
        gc = gameController.GetComponent<GameController>();
        uc = gameController.GetComponent<UIController>();
        //mgc = gameController.GetComponent<MinigameController>();
        currentSlot = gc.allSlotTypes[Random.Range(0, gc.allSlotTypes.Length)];
        renderer.color = currentSlot.color;

        cc = GameObject.Find("GameController").GetComponent<ChanceController>();

    }

    public void TriggerSlotBehaviour(int playerIndex)
    {
        switch (currentSlot.slotType)
        {
            case SlotType.plusThree:
                Debug.Log("wow plus 3");
                gc.ChangeFruitAmount(playerIndex, 3);
                break;
            case SlotType.minusThree:
                Debug.Log("wow minus 3");
                gc.ChangeFruitAmount(playerIndex, -3);
                break;
            case SlotType.plusTen:
                Debug.Log("wow plus 10");
                gc.ChangeFruitAmount(playerIndex, 10);
                break;
            case SlotType.miniGame:
                Debug.Log("wow minigame");
                //mgc.RandomizeMinigame();
                break;

            case SlotType.chance:
                //Debug.Log("wow chans");
                //cc.RandomiseChance(playerIndex);
                break;

            default:
                break;
        }
    }
}

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

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");

        renderer = GetComponentInChildren<SpriteRenderer>();
        gc = gameController.GetComponent<GameController>();
        uc = gameController.GetComponent<UIController>();
        //mgc = gameController.GetComponent<MinigameController>();
        currentSlot = gc.allSlotTypes[Random.Range(0, gc.allSlotTypes.Length)];
        renderer.color = currentSlot.color;
    }

    public void TriggerSlotBehaviour(int playerIndex)
    {
        switch (currentSlot.slotType)
        {
            case SlotType.plusThree:
                //Add 3 fruits to player
                gc.ChangeFruitAmount(playerIndex, 3);
                break;
            case SlotType.minusThree:
                //Remove 3 fruits from player
                gc.ChangeFruitAmount(playerIndex, -3);
                break;
            case SlotType.plusTen:
                //Add 10 fruits to player
                gc.ChangeFruitAmount(playerIndex, 10);
                break;
            case SlotType.miniGame:
                //minigame
                //mgc.RandomizeMinigame();
                break;
            default:
                break;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    public Slot currentSlot;
    
    private GameObject gameController;
    private GameController gc;
    private MoveController mc;
    private UIController uc;
    private ChanceController cc;
    new SpriteRenderer renderer;

    public bool hasBag;
    public GameObject fruitBagObject;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");

        renderer = GetComponentInChildren<SpriteRenderer>();
        gc = gameController.GetComponent<GameController>();
        mc = gameController.GetComponent<MoveController>();
        uc = gameController.GetComponent<UIController>();
        cc = gameController.GetComponent<ChanceController>();

        //currentSlot = gc.allSlotTypes[Random.Range(0, gc.allSlotTypes.Length)];
        renderer.color = currentSlot.color;
    }

    public void TriggerSlotBehaviour(int playerIndex)
    {
        switch (currentSlot.slotType)
        {
            case SlotType.plusThree:
                mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.actionType = ActionType.PlusFruit3;

                if(hasBag)
                {
                    mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.extraFruits = 10;
                }
                break;
            case SlotType.minusThree:
                mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.actionType = ActionType.MinusFruit3;
                break;
            case SlotType.plusTen:
                mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.actionType = ActionType.PlusFruit10;
                break;
            case SlotType.miniGame:
                mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.actionType = ActionType.Minigame;
                break;
            case SlotType.chance:
                mc.players[playerIndex].GetComponent<PlayerController>().playerVariable.actionType = ActionType.Chance;
                //Debug.Log(currentSlot.slotType);
                break;
            default:
                break;
        }
    }
}
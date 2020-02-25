﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionState : GameState
{
    public ActionState(InputController newIc, TimerController newTc, Dpad newDp,
        GameController newGc, ChanceController newCc, UIController newUc, MoveController newMc, SlotController newSc, PlayerController newPc)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
        this.gc = newGc;
        this.cc = newCc;
        this.uc = newUc;
        this.mc = newMc;
        this.sc = newSc;
        this.pc = newPc;

        gc.allSlots[mc.selectedSlot].GetComponent<SlotController>().TriggerSlotBehaviour(mc.selectedPlayer);
    }

    /*public override void Update()
    {
        gc.allSlots[mc.selectedSlot].GetComponent<SlotController>().TriggerSlotBehaviour(mc.selectedPlayer);
    }*/

    public override GameState NextState()
    {
        ResultState resultState = new ResultState(ic, tc, dp, gc, cc, uc, mc, sc, pc);
        resultState.time = 27;
        return resultState;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreatheState : GameState
{
    public BreatheState(InputController newIc, TimerController newTc, Dpad newDp,
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

        uc.UpdateQueueOrderUi(false);
        newGc.GetComponent<ActionController>().ResetActionList();
        gc.ResetQueue();
        uc.DisplayMinigameWinner(false);
        gc.GetComponent<MinigameController>().playedMinigameThisRound = false;
    }

    public override GameState NextState()
    {
        tc.SetStartValues(30);

        InputState inputState = new InputState(ic, tc, dp, gc, cc, uc, mc, sc, pc);
        inputState.type = 0;
        inputState.time = 3;
        return inputState;
    }
}
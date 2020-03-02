using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultState : GameState
{
    public ResultState(InputController newIc, TimerController newTc, Dpad newDp,
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

        uc.DisplayMinigameWinner(true);
        if(gc.GetComponent<MinigameController>().playedMinigameThisRound)
        {
            gc.ChangeFruitAmount(gc.GetComponent<MinigameController>().minigamePlayers[gc.GetComponent<MinigameController>().winner], 10);
        }
        //Debug.Log(gc.GetComponent<MinigameController>().minigamePlayers[gc.GetComponent<MinigameController>().winner]);
    }

    public override GameState NextState()
    {
        BreatheState breatheState = new BreatheState(ic, tc, dp, gc, cc, uc, mc, sc, pc);
        breatheState.type = 4;
        breatheState.time = 30;
        return breatheState;
    }
}

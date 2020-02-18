using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultState : GameState
{
    public ResultState(InputController newIc, TimerController newTc, Dpad newDp, GameController newGc, ChanceController newCc)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
        this.gc = newGc;
        this.cc = newCc;
    }

    public override GameState NextState()
    {
        BreatheState breatheState = new BreatheState(ic, tc, dp, gc, cc);
        breatheState.time = 30;
        return breatheState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultState : GameState
{
    public ResultState(InputController newIc, TimerController newTc, Dpad newDp,
        GameController newGc, ChanceController newCc, UIController newUc)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
        this.gc = newGc;
        this.cc = newCc;
        this.uc = newUc;
    }

    public override GameState NextState()
    {
        BreatheState breatheState = new BreatheState(ic, tc, dp, gc, cc, uc);
        breatheState.time = 30;
        return breatheState;
    }
}

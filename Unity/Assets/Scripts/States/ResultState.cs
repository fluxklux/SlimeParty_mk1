using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultState : GameState
{
    public ResultState(InputController newIc, TimerController newTc, Dpad newDp, GameController newGc)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
        this.gc = newGc;
    }

    public override GameState NextState()
    {
        BreatheState breatheState = new BreatheState(ic, tc, dp, gc);
        breatheState.time = 30;
        return breatheState;
    }
}

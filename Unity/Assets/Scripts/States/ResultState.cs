using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultState : GameState
{
    public ResultState(InputController newIc, TimerController newTc, Dpad newDp)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
    }

    public override GameState NextState()
    {
        BreatheState breatheState = new BreatheState(ic, tc, dp);
        breatheState.time = 30;
        return breatheState;
    }
}

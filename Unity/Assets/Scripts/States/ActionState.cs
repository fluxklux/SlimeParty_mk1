using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionState : GameState
{
    public ActionState(InputController newIc, TimerController newTc, Dpad newDp)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
    }

    public override GameState NextState()
    {
        ResultState resultState = new ResultState(ic, tc, dp);
        resultState.time = 27;
        return resultState;
    }
}

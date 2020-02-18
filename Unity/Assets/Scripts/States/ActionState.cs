using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionState : GameState
{
    public ActionState(InputController newIc, TimerController newTc, Dpad newDp, GameController newGc, ChanceController newCc)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
        this.gc = newGc;
        this.cc = newCc;
    }

    public override GameState NextState()
    {
        ResultState resultState = new ResultState(ic, tc, dp, gc, cc);
        resultState.time = 27;
        return resultState;
    }
}

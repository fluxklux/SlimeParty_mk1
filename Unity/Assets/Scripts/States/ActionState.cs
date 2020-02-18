using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionState : GameState
{
    public ActionState(InputController newIc, TimerController newTc, Dpad newDp, GameController newGc)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
        this.gc = newGc;
    }

    public override GameState NextState()
    {
        ResultState resultState = new ResultState(ic, tc, dp, gc);
        resultState.time = 27;
        return resultState;
    }
}

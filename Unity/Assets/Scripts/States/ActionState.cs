using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionState : GameState
{
    public ActionState(InputController newIc, TimerController newTc, Dpad newDp,
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
        ResultState resultState = new ResultState(ic, tc, dp, gc, cc, uc);
        resultState.time = 27;
        return resultState;
    }
}

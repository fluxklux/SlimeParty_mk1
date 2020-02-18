using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveState : GameState
{
    public MoveState(InputController newIc, TimerController newTc, Dpad newDp,
        GameController newGc, ChanceController newCc, UIController newUc)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
        this.gc = newGc;
        this.cc = newCc;
        this.uc = newUc;

        uc.ToggleDpad(false);
    }

    public override GameState NextState()
    {
        ActionState actionState = new ActionState(ic, tc, dp, gc, cc, uc);
        actionState.time = 23;
        return actionState;
    }
}

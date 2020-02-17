using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveState : GameState
{
    public MoveState(InputController newIc, TimerController newTc, Dpad newDp)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
    }

    public override GameState NextState()
    {
        ActionState actionState = new ActionState(ic, tc, dp);
        actionState.time = 23;
        return actionState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveState : GameState
{
    public MoveState(InputController newIc, TimerController newTc, Dpad newDp, GameController newGc)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
        this.gc = newGc;
    }

    public override GameState NextState()
    {
        ActionState actionState = new ActionState(ic, tc, dp, gc);
        actionState.time = 23;
        return actionState;
    }
}

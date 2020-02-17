using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveState : GameState
{
    public MoveState (InputController newIc)
    {
        this.ic = newIc;
    }

    public override GameState NextState()
    {
        ActionState actionState = new ActionState(ic);
        actionState.time = 23;
        return actionState;
    }
}

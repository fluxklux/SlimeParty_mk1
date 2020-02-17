using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputState : GameState
{
    public InputState (InputController newIc)
    {
        this.ic = newIc;
    }

    public override void Update ()
    {
        ic.GetInput();
    }

    public override GameState NextState()
    {
        MoveState moveState = new MoveState(ic);
        moveState.time = 8;
        return moveState;
    }
}

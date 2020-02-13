using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputState : GameState
{
    public InputState()
    {

    }

    public override GameState NextState()
    {
        MoveState moveState = new MoveState();
        moveState.time = 10;
        return moveState;
    }
}

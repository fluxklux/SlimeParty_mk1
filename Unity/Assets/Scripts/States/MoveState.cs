using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveState : GameState
{
    public MoveState ()
    {
        
    }

    public override GameState NextState()
    {
        ActionState actionState = new ActionState();
        actionState.time = 25;
        return actionState;
    }
}

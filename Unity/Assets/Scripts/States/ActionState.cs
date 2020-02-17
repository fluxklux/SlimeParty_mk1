using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionState : GameState
{
    public ActionState(InputController newIc)
    {
        this.ic = newIc;
    }

    public override GameState NextState()
    {
        ResultState resultState = new ResultState(ic);
        resultState.time = 27;
        return resultState;
    }
}

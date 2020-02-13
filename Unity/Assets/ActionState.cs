using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionState : GameState
{
    public ActionState()
    {

    }

    public override GameState NextState()
    {
        ResultState resultState = new ResultState();
        resultState.time = 30;
        return resultState;
    }
}

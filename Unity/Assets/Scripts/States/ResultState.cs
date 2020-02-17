using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultState : GameState
{
    public ResultState(InputController newIc)
    {
        this.ic = newIc;
    }

    public override GameState NextState()
    {
        BreatheState breatheState = new BreatheState(ic);
        breatheState.time = 30;
        return breatheState;
    }
}

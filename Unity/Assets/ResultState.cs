using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultState : GameState
{
    public ResultState()
    {

    }

    public override GameState NextState()
    {
        BreatheState breatheState = new BreatheState();
        breatheState.time = 35;
        return breatheState;
    }
}

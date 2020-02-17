using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreatheState : GameState
{
    public BreatheState(InputController newIc, TimerController newTc, Dpad newDp)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
    }

    public override GameState NextState()
    {
        tc.SetValues(30);

        InputState inputState = new InputState(ic, tc, dp);
        inputState.time = 3;
        Debug.Log("Round is done!");
        return inputState;
    }
}

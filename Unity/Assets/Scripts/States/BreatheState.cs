using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreatheState : GameState
{
    public BreatheState(InputController newIc, TimerController newTc, Dpad newDp, GameController newGc)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
        this.gc = newGc;

        gc.ResetQueue();
    }

    public override GameState NextState()
    {
        tc.SetValues(30);

        InputState inputState = new InputState(ic, tc, dp, gc);
        inputState.time = 3;
        Debug.Log("Round is done!");
        return inputState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreatheState : GameState
{
    public BreatheState(InputController newIc)
    {
        this.ic = newIc;
    }

    public override GameState NextState()
    {
        InputState inputState = new InputState(ic);
        inputState.time = 100;
        Debug.Log("Round is done!");
        return inputState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BreatheState : GameState
{
    public BreatheState()
    {

    }

    public override GameState NextState()
    {
        InputState inputState = new InputState();
        inputState.time = 100;
        Debug.Log("Round is done!");
        return inputState;
    }
}

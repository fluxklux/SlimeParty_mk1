using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("Current State: ")]
    public GameState currentState = null;

    [Header("All States: ")]
    public InputState inputState = new InputState();
    public MoveState moveState = new MoveState();
    public ActionState actionState = new ActionState();
    public ResultState resultState = new ResultState();
    public BreatheState breatheState = new BreatheState();

    [HideInInspector]
    public bool nextStateBool = true;

    private TimerController tc;

    private void Awake()
    {
        tc = GetComponent<TimerController>();

        currentState = inputState;
        tc.SetValues(inputState.time + moveState.time + actionState.time + resultState.time + breatheState.time);
    }

    private void Update()
    {
        currentState.Update();

        if (GetComponent<TimerController>().elapsedTime > currentState.time)
        {
            if (nextStateBool)
            {
                nextStateBool = false;
                currentState = currentState.NextState();
                nextStateBool = true;
            }
        }
    }
}

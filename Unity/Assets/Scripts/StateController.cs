using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("Current State: ")]
    public GameState currentState = null;

    [Header("All States: ")]
    public InputState inputState;
    public MoveState moveState;
    public ActionState actionState;
    public ResultState resultState;
    public BreatheState breatheState; 

    [HideInInspector]
    public bool nextStateBool = true;

    private TimerController tc;
    private InputController ic;
    private Dpad dp;
    private GameController gc;
    private ChanceController cc;

    private void Awake()
    {
        ic = GetComponent<InputController>();
        dp = GetComponentInChildren<Dpad>();
        tc = GetComponent<TimerController>();
        gc = GetComponent<GameController>();
        cc = GetComponent<ChanceController>();

        inputState = new InputState(ic, tc, dp, gc, cc);
        inputState.time = 3;
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

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

    [HideInInspector]
    public bool canSkip = false;

    private TimerController tc;
    private InputController ic;
    private Dpad dp;
    private GameController gc;
    private ChanceController cc;
    private UIController uc;
    private MoveController mc;
    private SlotController sc;
    private PlayerController pc;
    private ActionController ac;

    private void Awake()
    {
        ic = GetComponent<InputController>();
        dp = GetComponentInChildren<Dpad>();
        tc = GetComponent<TimerController>();
        gc = GetComponent<GameController>();
        cc = GetComponent<ChanceController>();
        uc = GetComponent<UIController>();
        mc = GetComponent<MoveController>();
        sc = GetComponent<SlotController>();
        pc = GetComponent<PlayerController>();
        ac = GetComponent<ActionController>();

        inputState = new InputState(ic, tc, dp, gc, cc, uc, mc, sc, pc);
        inputState.time = 3;
        currentState = inputState;
        tc.SetStartValues(inputState.time + moveState.time + actionState.time + resultState.time + breatheState.time);
    }

    public void UpdateTimerValues (float amount)
    {
        float calcTimer = tc.startTimer - amount;
        //Debug.Log("[amount]: " + amount + " [calctimer]: " + calcTimer);

        tc.SetTimerValues(calcTimer);
    }

    private void Update()
    {
        currentState.Update();

        if (tc.elapsedTime > currentState.time)
        {
            if (nextStateBool)
            {
                nextStateBool = false;
                currentState = currentState.NextState();
                nextStateBool = true;
            }
        }

        //Debug.Log("canSkip: " + canSkip);

        //skip state if no actions where done.
        if(canSkip)
        {
            if (currentState.type == 2 && ac.actionClasses.Count == 0)
            {
                //Debug.Log("ActionState is empty");

                if (nextStateBool)
                {
                    //Debug.Log("skipped action state");
                    nextStateBool = false;
                    UpdateTimerValues(currentState.time);
                    currentState = currentState.NextState();
                    nextStateBool = true;
                }
            }
        }

        //skip results if no minigame was played
        if (currentState.type == 3 && !ac.GetComponent<MinigameController>().playedMinigameThisRound)
        {
            if (nextStateBool)
            {
                //Debug.Log("skipped results state");
                nextStateBool = false;
                UpdateTimerValues(currentState.time);
                currentState = currentState.NextState();
                nextStateBool = true;
            }
        }
    }
}

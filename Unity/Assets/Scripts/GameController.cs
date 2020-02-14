using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Current State: ")]
    public GameState currentState = null;

    [Header("All States: ")]
    public InputState inputState = new InputState();
    public MoveState moveState = new MoveState();
    public ActionState actionState = new ActionState();
    public ResultState resultState = new ResultState();
    public BreatheState breatheState = new BreatheState();

    private float timer = 0.0f;
    private float startTimer = 0.0f;
    private float elapsedTime = 0.0f;

    [HideInInspector]
    public bool nextStateBool = true;

    private void Awake()
    {
        currentState = inputState;
        timer = inputState.time + moveState.time + actionState.time + resultState.time + breatheState.time;
        startTimer = inputState.time + moveState.time + actionState.time + resultState.time + breatheState.time;

        Debug.Log(currentState + ": " + currentState.time);
    }

    private void Update()
    {
        currentState.Update();

        //borde vara timeController's timer.
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            elapsedTime = startTimer - timer;
        }
        //

        if (elapsedTime > currentState.time)
        {
            if(nextStateBool)
            {
                nextStateBool = false;
                currentState = currentState.NextState();
                Debug.Log(currentState + ": " + currentState.time);
                nextStateBool = true;
            }
        }
    }

    private void OnGUI()
    {
        string text = "TIMER: " + timer.ToString("F2") + "\n" + "ELAPSED TIME: " + elapsedTime.ToString("F2") + "\n" + currentState.ToString();
        GUI.Label(new Rect(10, 10, 200, 200), text);
    }
}

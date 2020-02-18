using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputState : GameState
{
    public InputState(InputController newIc, TimerController newTc, Dpad newDp,
        GameController newGc, ChanceController newCc, UIController newUc)
    {
        this.ic = newIc;
        this.tc = newTc;
        this.dp = newDp;
        this.gc = newGc;
        this.cc = newCc;
        this.uc = newUc;

        dp.Randomize();
        uc.ToggleDpad(true);
        uc.ToggleTimer(true);
    }

    public override void Update ()
    {
        ic.GetInput();
    }

    public override GameState NextState()
    {
        MoveState moveState = new MoveState(ic, tc, dp, gc, cc, uc);
        moveState.time = 8;
        return moveState;
    }
}

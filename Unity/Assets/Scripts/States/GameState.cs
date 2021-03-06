﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public float time;
    public int type;

    public InputController ic;
    public TimerController tc;
    public Dpad dp;
    public GameController gc;
    public ChanceController cc;
    public UIController uc;
    public MoveController mc;
    public SlotController sc;
    public PlayerController pc;

    public GameState ()
    {

    }

    public virtual void Update ()
    {

    }

    public virtual GameState NextState ()
    {
        return this;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public float time;

    public virtual void Update ()
    {

    }

    public virtual GameState NextState ()
    {
        return this;
    }
}

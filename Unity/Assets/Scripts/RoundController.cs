using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    private int round;

    public int roundMax;

    void Start()
    {
        round = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateRound()
    {
        round++;
        if(round >= roundMax)
        {
            Time.timeScale = 0f;
        }
    }
}

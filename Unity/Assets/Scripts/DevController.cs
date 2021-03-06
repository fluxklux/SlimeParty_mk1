﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevController : MonoBehaviour
{
    [Header("Objects to hide: ")]
    public GameObject[] debugObjects;

    [Header("Visuals: ")]
    public GUIStyle style;

    [HideInInspector]
    public bool debugBool = false;
    private TimerController tc;
    private StateController sc;

    private void Start()
    {
        sc = GetComponent<StateController>();
        tc = GetComponent<TimerController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangeDebugMode();
        }
    }

    private void ChangeDebugMode ()
    {
        debugBool = !debugBool;

        if(debugBool)
        {
            for (int i = 0; i < debugObjects.Length; i++)
            {
                debugObjects[i].GetComponentInChildren<SpriteRenderer>().enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < debugObjects.Length; i++)
            {
                debugObjects[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void OnGUI()
    {
        if(debugBool)
        {
            string stateText = "TIMER: " + tc.timer.ToString("F2") + "\n" + "ELAPSED TIME: " + 
                tc.elapsedTime.ToString("F2") + "\n" + sc.currentState.ToString() + " " + 
                sc.currentState.type.ToString() + "\n" + "Round: " + GetComponent<RoundController>().round.ToString();
            GUI.Label(new Rect(10, 10, 200, 200), stateText, style);
        }
    }
}

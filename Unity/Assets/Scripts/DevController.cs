﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevController : MonoBehaviour
{
    [Header("Objects to hide: ")]
    public GameObject[] debugObjects;

    [Header("Visuals: ")]
    public GUIStyle style;

    private bool debugBool = true;
    private TimerController tc;
    private GameController gc;

    private void Start()
    {
        gc = GetComponent<GameController>();
        tc = GetComponent<TimerController>();

        ChangeDebugMode();
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
                debugObjects[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < debugObjects.Length; i++)
            {
                debugObjects[i].SetActive(true);
            }
        }
    }

    private void OnGUI()
    {
        if(debugBool)
        {
            string stateText = "TIMER: " + tc.timer.ToString("F2") + "\n" + "ELAPSED TIME: " + tc.elapsedTime.ToString("F2") + "\n" + gc.currentState.ToString();
            GUI.Label(new Rect(10, 10, 200, 200), stateText, style);
        }
    }
}
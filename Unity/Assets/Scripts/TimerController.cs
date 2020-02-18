﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private float startTimer = 0.0f;

    [HideInInspector]
    public float timer = 0.0f;
    [HideInInspector]
    public float elapsedTime = 0.0f;
    [HideInInspector]
    public bool count = false;

    private UIController uc;

    private void Start()
    {
        uc = GetComponent<UIController>();
    }

    public void SetValues (float value)
    {
        timer = value;
        startTimer =value;
    }

    private void Update()
    {
        if(timer > 0 && count)
        {
            timer -= Time.deltaTime;
            elapsedTime = startTimer - timer;

            uc.UpdateTimerText(timer);
        }

        timer = Mathf.Clamp(timer, 0, 100);
    }
}

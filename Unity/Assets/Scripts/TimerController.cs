using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    public Color defaultColor;
    public Color halfWayColor;
    public Color lastSecondsColor;

    public Text timerText;

    private void Start()
    {
        uc = GetComponent<UIController>();

        timerText.color = defaultColor;
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

            uc.UpdateTimerText(timer, startTimer);
            ChangeTimerColor(timer);
        }

        timer = Mathf.Clamp(timer, 0, 100);
    }

    public void ChangeTimerColor(float timer)
    {
        if (timer >= 30)
        {
            timerText.color = defaultColor;
        }

        if (timer <= 15)
        {
            timerText.color = halfWayColor;
        }
        else
        {
            timerText.color = defaultColor;
        }

        if (timer <= 5)
        {
            timerText.color = lastSecondsColor;
        }
        else if (timer >= 5 && timer <= 15)
        {
            timerText.color = halfWayColor;
        }
    }
}
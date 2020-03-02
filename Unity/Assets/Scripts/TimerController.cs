using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerController : MonoBehaviour
{
    [HideInInspector]
    public float startTimer = 0.0f;
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

    public Text minigameTimerText;

    private void Start()
    {
        uc = GetComponent<UIController>();

        timerText.color = defaultColor;
        minigameTimerText.color = defaultColor;
    }

    public void SetStartValues (float value)
    {
        timer = value;
        startTimer =value;
    }

    public void SetTimerValues (float value)
    {
        timer = value;
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
        if (timer >= startTimer)
        {
            timerText.color = defaultColor;
        }

        if (timer <= startTimer/2)
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
        else if (timer >= 5 && timer <= startTimer/2)
        {
            timerText.color = halfWayColor;
        }
    }

    public void ChangeMinigameTimerColor(float timer)
    {
        if (timer >= 5)
        {
            minigameTimerText.color = defaultColor;
        }

        if (timer <= 4)
        {
            minigameTimerText.color = halfWayColor;
        }
        else
        {
            minigameTimerText.color = defaultColor;
        }

        if (timer <= 2)
        {
            minigameTimerText.color = lastSecondsColor;
        }
        else if (timer >= 2 && timer <= 4)
        {
            minigameTimerText.color = halfWayColor;
        }
    }

}
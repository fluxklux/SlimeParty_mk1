using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerColor : MonoBehaviour
{
    public Text timerText;

    [Header("Start color")]
    public Color defaultColor;

    [Header("Half color")]
    public Color halfWayColor;

    [Header("End color")]
    public Color lastSecondsColor;

    // Start is called before the first frame update
    void Start()
    {
        timerText.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {

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
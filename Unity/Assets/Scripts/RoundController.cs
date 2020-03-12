using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour
{
    private int round;
    public Text overEndText;

    public int roundMax;

    void Start()
    {
        round = 1;
    }

    public void UpdateRound()
    {
        round++;
        if(round >= roundMax)
        {
            Time.timeScale = 0f;
            GetComponent<PauseController>().EndGame();
            overEndText.gameObject.SetActive(true);
        }
    }
}

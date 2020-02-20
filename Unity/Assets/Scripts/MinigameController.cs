using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MinigameController : MonoBehaviour
{
    public Minigame[] allMinigames;
    public Text timerText;

    [Header("0_Masher")]
    public GameObject masherPanel;
    public Text[] masherTexts;

    public int[] masherInts = { 0, 0 };
    private int maxInt = 0;

    private float timer; //local minigame timer
    private bool minigameActive = false; //are minigames playing at the moment?
    private int minigameIndex; //what minigame is active
    private bool doOnce = true;

    public void RandomizeMinigame()
    {
        int random = UnityEngine.Random.Range(0, allMinigames.Length);

        switch (random)
        {
            case 0:
                Debug.Log("Masher");
                MasherTogglePanel(true);
                break;
            case 1:
                Debug.Log("Reaction");
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            RandomizeMinigame();
        }

        if (minigameActive)
        {
            if (timer <= 0)
            {
                minigameActive = false;
            }

            //minigame is active and running
            switch (minigameIndex)
            {
                case 0: //0_MASHER
                    MasherEvent();
                    timer -= Time.fixedDeltaTime;
                    timerText.text = Mathf.Round(timer).ToString("F2");
                    break;
                case 1: //1_REACTION

                    break;
                default:
                    Debug.Log("Something went wrong!");
                    break;
            }
        }
        else
        {
            if (!doOnce)
            {
                doOnce = true;
                //Minigame is done
                switch (minigameIndex)
                {
                    case 0: //0_MASHER
                            //display winner of masher minigame
                        Array.Sort(masherInts);
                        maxInt = masherInts[masherInts.Length - 1];
                        timerText.text = maxInt.ToString("F0");
                        //Debug.Log("WINNER OF MASH IS: " + maxInt);
                        StartCoroutine(ResetMinigame());
                        break;
                    case 1: //1_REACTION

                        break;
                    default:
                        Debug.Log("Something went wrong!");
                        break;
                }
            }

        }
    }

    private IEnumerator ResetMinigame()
    {
        yield return new WaitForSeconds(1f);
        StopAllCoroutines(); //farlig function
        TimerToggle(false);
        Debug.Log("odododod");
        MasherTogglePanel(false);

        for (int i = 0; i < masherInts.Length; i++)
        {
            masherInts[i] = 0;
        }
    }

    //global for all minigames
    private void TimerToggle(bool onOff)
    {
        timerText.gameObject.SetActive(onOff);
    }

    #region 0_Masher
    private void MasherTogglePanel(bool onOff)
    {
        timer = allMinigames[0].time;
        TimerToggle(true);
        minigameActive = true;
        doOnce = false;
        minigameIndex = allMinigames[0].index;
        masherPanel.SetActive(onOff);
    }

    private void MasherEvent()
    {
        switch (Input.inputString)
        {
            case "1":
                masherInts[0]++;
                masherTexts[0].text = masherInts[0].ToString("F2");
                break;
            case "2":
                masherInts[1]++;
                masherTexts[1].text = masherInts[1].ToString("F2");
                break;
            default:
                break;
        }

        if (Input.GetButtonDown("C1 Select"))
        {
            masherInts[0]++;
            masherTexts[0].text = masherInts[0].ToString("F2");
        }

        if (Input.GetButtonDown("C2 Select"))
        {
            masherInts[1]++;
            masherTexts[1].text = masherInts[1].ToString("F2");
        }
    }
    #endregion
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MinigameController : MonoBehaviour
{
    public Minigame[] allMinigames;
    public Text timerText;
    public Text[] playerTexts;

    [Header("0_Masher")]
    public GameObject masherPanel;
    public Text[] masherTexts;

    public int[] masherInts = { 0, 0 };

    private int[] minigamePlayers = { 0, 1 };

    private float timer; //local minigame timer
    private bool minigameActive = false; //are minigames playing at the moment?
    private int minigameIndex; //what minigame is active
    private bool doOnce = true;
    private InputController ic;

    private int amount;

    private void Start()
    {
        ic = GetComponent<InputController>();
    }

    public void SetMinigamePlayers (int playerIndex, int controlIndex)
    {
        minigamePlayers[playerIndex] = controlIndex;
    }

    public void SelectPlayer (int playerIndex)
    {
        for (int i = 0; i < ic.hasJoined.Length; i++)
        {
            if(ic.hasJoined[i] == true)
            {
                amount++;
            }
        }

        int randomPlayer = RandomizePlayer();

        while(randomPlayer == playerIndex)
        {
            Debug.Log("While loop");
            randomPlayer = RandomizePlayer();
        }

        SetMinigamePlayers(0, playerIndex);
        SetMinigamePlayers(1, randomPlayer);

        RandomizeMinigame();

        switch (minigamePlayers[0])
        {
            case 0:
                playerTexts[0].text = "Player 1";
                playerTexts[0].color = ic.allPlayers[0].GetComponent<PlayerController>().playerVariable.color;
                break;
            case 1:
                playerTexts[0].text = "Player 2";
                playerTexts[0].color = ic.allPlayers[1].GetComponent<PlayerController>().playerVariable.color;
                break;
            case 2:
                playerTexts[0].text = "Player 3";
                playerTexts[0].color = ic.allPlayers[2].GetComponent<PlayerController>().playerVariable.color;
                break;
            case 3:
                playerTexts[0].text = "Player 4";
                playerTexts[0].color = ic.allPlayers[3].GetComponent<PlayerController>().playerVariable.color;
                break;
            default:
                break;
        }

        switch (minigamePlayers[1])
        {
            case 0:
                playerTexts[1].text = "Player 1";
                playerTexts[1].color = ic.allPlayers[0].GetComponent<PlayerController>().playerVariable.color;
                break;
            case 1:
                playerTexts[1].text = "Player 2";
                playerTexts[1].color = ic.allPlayers[1].GetComponent<PlayerController>().playerVariable.color;
                break;
            case 2:
                playerTexts[1].text = "Player 3";
                playerTexts[1].color = ic.allPlayers[2].GetComponent<PlayerController>().playerVariable.color;
                break;
            case 3:
                playerTexts[1].text = "Player 4";
                playerTexts[1].color = ic.allPlayers[3].GetComponent<PlayerController>().playerVariable.color;
                break;
            default:
                break;
        }

        //Debug.Log(minigamePlayers[0].ToString() + ", " + minigamePlayers[1].ToString());
        //Debug.Log("Players in game: " + amount);
    }

    private int RandomizePlayer ()
    {
        int ran = UnityEngine.Random.Range(0, amount);
        return ran;
    }

    public void RandomizeMinigame()
    {
        MasherTogglePanel(true);

        /*int random = UnityEngine.Random.Range(0, allMinigames.Length);

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
        }*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SelectPlayer(0);
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

                switch (minigameIndex)
                {
                    case 0: //0_MASHER

                        if (masherInts[0] > masherInts[1])
                        {
                            masherTexts[0].color = Color.green;
                            masherTexts[1].color = Color.red;
                        }
                        else if(masherInts[1] > masherInts[0])
                        {
                            masherTexts[1].color = Color.green;
                            masherTexts[0].color = Color.red;
                        }
                        else
                        {
                            masherTexts[0].color = Color.red;
                            masherTexts[1].color = Color.red;
                        }

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
        MasherTogglePanel(false);
        minigameActive = false;
        amount = 0;
        doOnce = true;
        TimerToggle(false);

        for (int i = 0; i < masherInts.Length; i++)
        {
            masherInts[i] = 0;
            masherTexts[i].color = Color.white;
            masherTexts[i].text = "0";
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

        if (Input.GetButtonDown("C" + (minigamePlayers[0] + 1) + " Select"))
        {
            masherInts[0]++;
            masherTexts[0].text = masherInts[0].ToString("F2");
        }

        if (Input.GetButtonDown("C" + (minigamePlayers[1] + 1) + " Select"))
        {
            masherInts[1]++;
            masherTexts[1].text = masherInts[1].ToString("F2");
        }
    }
    #endregion
}
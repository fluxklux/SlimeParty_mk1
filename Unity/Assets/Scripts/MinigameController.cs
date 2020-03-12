using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MinigameController : MonoBehaviour
{
    public Minigame[] allMinigames;
    public Text[] playerTexts;
    public Text timerText;

    [SerializeField] private Text player1SelectedText = null;
    [SerializeField] private Text player2SelectedText = null;

    [SerializeField] private GameObject instructionsPanel = null;
    [SerializeField] private Text instructionText = null;
    [SerializeField] private Text headerText = null;
    [SerializeField] private Text countdownText = null;

    [Header("Instructions variables")]
    [SerializeField] private string[] instructionsHeaders = { "", "" };
    [SerializeField] private string[] instructionsDescription = { "", "" };

    //--------------------------------------------------------------

    [Header("1_Rotation")]
    [SerializeField] private GameObject rotationPanel = null;

    private bool[] player1InputSequence = { false, false, false, false };
    private bool[] player2InputSequence = { false, false, false, false };

    [Header("0_Masher")]
    [SerializeField] private GameObject masherPanel = null;
    [SerializeField] private Text[] pointText = { null, null };

    
    //--------------------------------------------------------------

    [HideInInspector]
    public int[] minigamePlayers = { 0, 1 };

    private int[] points = { 0, 0 };
    private float timer; //local minigame timer
    private bool minigameActive = false; //are minigames playing at the moment?
    private int minigameIndex; //what minigame is active
    private bool doOnce = true;
    private InputController ic;
    private TimerController tc;
    private int amount;
    private float countdown = 5;
    private AudioController ac;

    [HideInInspector]
    public int winner, loser; //Sätt til 3 om det blir lika
    [HideInInspector]
    public bool countdownBool = false;
    [HideInInspector]
    public bool playedMinigameThisRound = false;

    private void Start()
    {
        ic = GetComponent<InputController>();
        tc = GetComponent<TimerController>();
        ac = GetComponent<AudioController>();
    }

    public void SetMinigamePlayers (int playerIndex, int controlIndex)
    {
        minigamePlayers[playerIndex] = controlIndex;
    }

    public void SelectPlayer (int mainPlayer)
    {
        for (int i = 0; i < ic.hasJoined.Length; i++)
        {
            if (ic.hasJoined[i] == true)
            {
                amount++;
            }
        }

        int randomPlayer = RandomizePlayer();

        while(randomPlayer == mainPlayer)
        {
            randomPlayer = RandomizePlayer();
        }

        SetMinigamePlayers(0, mainPlayer);
        SetMinigamePlayers(1, randomPlayer);

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
        int random = UnityEngine.Random.Range(0, allMinigames.Length);

        switch (random)
        {
            case 0:
                Debug.Log("Masher");
                minigameIndex = allMinigames[0].index;
                break;
            case 1:
                Debug.Log("Reaction");
                minigameIndex = allMinigames[1].index;
                break;
            default:
                break;
        }
    }

    public IEnumerator Instructions (int playerIndex)
    {

        ac.FadeInCaller(ac.minigameSource, 1.5f, 1);
        ac.FadeOutCaller(ac.boardSource, 1.5f, 0);

        SelectPlayer(playerIndex);
        RandomizeMinigame();

        player1SelectedText.text = "P" + (minigamePlayers[0] + 1).ToString();
        player1SelectedText.color = ic.allPlayers[minigamePlayers[0]].GetComponent<PlayerController>().playerVariable.color;

        player2SelectedText.text = "P" + (minigamePlayers[1] + 1).ToString();
        player2SelectedText.color = ic.allPlayers[minigamePlayers[1]].GetComponent<PlayerController>().playerVariable.color;

        instructionsPanel.SetActive(true);
        instructionText.text = instructionsDescription[minigameIndex];
        headerText.text = instructionsHeaders[minigameIndex];

        countdownBool = true;
        yield return new WaitForSeconds(5);
        instructionsPanel.SetActive(false);
        countdownBool = false;
        countdown = 5;

        StartMinigame(minigameIndex);
        ac.PlaySound(SoundEnum.minigameStartSound);
       

    }

    private void StartMinigame (int index)
    {
        switch(index)
        {
            case 0:
                MasherTogglePanel(true);
                break;
            case 1:
                RotationTogglePanel(true);
                break;
            default:
                Debug.Log("Something went wrong!");
            break;
        }
    }

    private void Update()
    {
        //Debug.Log("playedMinigameThisRound: " + playedMinigameThisRound);

        if(countdownBool)
        {
            countdown -= 1 * Time.deltaTime;
            countdownText.text = countdown.ToString("F2");
        }

        if (minigameActive)
        {
            if (timer <= 0)
            {
                minigameActive = false;
            }

            tc.ChangeMinigameTimerColor(timer);
            timer -= Time.fixedDeltaTime;
            timerText.text = Mathf.Round(timer).ToString("F2");

            //minigame is active and running
            switch (minigameIndex)
            {
                case 0: //0_MASHER
                    MasherEvent();
                    break;
                case 1: //1_REACTION
                    RotationEvent();
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

                        if (points[0] > points[1])
                        {
                            pointText[0].color = Color.green;
                            pointText[1].color = Color.red;
                            winner = 0;
                            loser = 1;
                        }
                        else if(points[1] > points[0])
                        {
                            pointText[1].color = Color.green;
                            pointText[0].color = Color.red;
                            winner = 1;
                            loser = 0;
                        }
                        else
                        {
                            //Debug.Log("DRAW");
                            pointText[0].color = Color.yellow;
                            pointText[1].color = Color.yellow;
                            winner = 3;
                            loser = 3;
                        }

                        StartCoroutine(ResetMinigame());
                        break;
                    case 1: //1_REACTION
                        if (points[0] > points[1])
                        {
                            pointText[0].color = Color.green;
                            pointText[1].color = Color.red;
                            winner = 0;
                            loser = 1;
                        }
                        else if (points[1] > points[0])
                        {
                            pointText[1].color = Color.green;
                            pointText[0].color = Color.red;
                            winner = 1;
                            loser = 0;
                        }
                        else
                        {
                            pointText[0].color = Color.yellow;
                            pointText[1].color = Color.yellow;
                            winner = 3;
                            loser = 3;
                        }

                        StartCoroutine(ResetMinigame());
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
        RotationTogglePanel(false);
        minigameActive = false;
        amount = 0;
        doOnce = true;
        TimerToggle(false);

        ac.PlaySound(SoundEnum.winSound);

        ac.FadeInCaller(ac.boardSource, 1.5f, 1);
        ac.FadeOutCaller(ac.minigameSource, 1.5f, 0);

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = 0;
            pointText[i].color = Color.white;
            pointText[i].text = "0";
        }

        tc.GetComponent<ActionController>().ResetActionList();
    }

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

        pointText[0].gameObject.SetActive(onOff);
        pointText[1].gameObject.SetActive(onOff);

        playerTexts[0].gameObject.SetActive(onOff);
        playerTexts[1].gameObject.SetActive(onOff);
    }

    private void MasherEvent()
    {
        switch (Input.inputString)
        {
            case "1":
                points[0]++;
                pointText[0].text = points[0].ToString("F2");
                break;
            case "2":
                points[1]++;
                pointText[1].text = points[1].ToString("F2");
                break;
            default:
                break;
        }

        if (Input.GetButtonDown("C" + (minigamePlayers[0] + 1) + " Select"))
        {
            points[0]++;
            pointText[0].text = points[0].ToString("F2");
        }

        if (Input.GetButtonDown("C" + (minigamePlayers[1] + 1) + " Select"))
        {
            points[1]++;
            pointText[1].text = points[1].ToString("F2");
        }
    }
    #endregion

    #region 1_Rotation
    private void RotationTogglePanel(bool onOff)
    {
        timer = allMinigames[1].time;
        TimerToggle(true);
        minigameActive = true;
        doOnce = false;
        minigameIndex = allMinigames[1].index;
        rotationPanel.SetActive(onOff);

        pointText[0].gameObject.SetActive(onOff);
        pointText[1].gameObject.SetActive(onOff);

        playerTexts[0].gameObject.SetActive(onOff);
        playerTexts[1].gameObject.SetActive(onOff);
    }

    private void RotationEvent()
    {
        //DEBUG keyboard
        switch (Input.inputString)
        {
            case "1":
                points[0]++;
                pointText[0].text = points[0].ToString("F2");
                break;
            case "2":
                points[1]++;
                pointText[1].text = points[1].ToString("F2");
                break;
            default:
                break;
        }

        //Player 1
        var p1Horizontal = Input.GetAxis("C" + (minigamePlayers[0] + 1) + " Horizontal");
        var p1Vertical = Input.GetAxis("C" + (minigamePlayers[0] + 1) + " Vertical");

        switch (p1Horizontal)
        {
            case 1:
                
                if(player1InputSequence[0] == true)
                {
                    player1InputSequence[1] = true;
                }
                break;
            case -1:
                if (player1InputSequence[2] == true)
                {
                    player1InputSequence[3] = true;

                    points[0]++;
                    pointText[0].text = points[0].ToString("F2");

                    for (int j = 0; j < player1InputSequence.Length; j++)
                    {
                        player1InputSequence[j] = false;
                    }
                }
                break;
            default:
                break;
        }

        switch (p1Vertical)
        {
            case 1:
                player1InputSequence[0] = true;
                break;
            case -1:
                if (player1InputSequence[1] == true)
                {
                    player1InputSequence[2] = true;
                }
                break;
            default:
                break;
        }

        //Player 2
        var p2Horizontal = Input.GetAxis("C" + (minigamePlayers[1] + 1) + " Horizontal");
        var p2Vertical = Input.GetAxis("C" + (minigamePlayers[1] + 1) + " Vertical");

        switch (p2Horizontal)
        {
            case 1:

                if (player2InputSequence[0] == true)
                {
                    player2InputSequence[1] = true;
                }
                break;
            case -1:
                if (player2InputSequence[2] == true)
                {
                    player2InputSequence[3] = true;

                    points[1]++;
                    pointText[1].text = points[1].ToString("F2");
                    for (int i = 0; i < player2InputSequence.Length; i++)
                    {
                        player2InputSequence[i] = false;
                    }
                }
                break;
            default:
                break;
        }

        switch (p2Vertical)
        {
            case 1:
                player2InputSequence[0] = true;
                break;
            case -1:
                if (player2InputSequence[1] == true)
                {
                    player2InputSequence[2] = true;
                }
                break;
            default:
                break;
        }
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //make private later, public for debugging reasons
    public int playerCount = 0;
    public bool[] hasPressedKey = { false, false, false, false };
    public bool[] hasJoined = { false, false, false, false };

    public GameObject[] allPlayers = { };

    [Header("DEBUG")]
    public GUIStyle style;

    private GameController gc;
    private Dpad dc;
    private UIController uc;
    private MoveController mc;

    private bool canTakeInput = true;
    private bool gameStarted = false;

    AudioController ac;

    void Start()
    {
        mc = GetComponent<MoveController>();
        dc = GetComponent<Dpad>();
        gc = GetComponent<GameController>();
        uc = GetComponent<UIController>();

        ac = GetComponent<AudioController>();

        uc.ToggleDpad(false);
        uc.ToggleTimer(false);
    }

    public void ChangeTakeInputBool(bool onOff)
    {
        canTakeInput = onOff;
    }

    public void AddPlayer(int index)
    {
        if (playerCount < 4)
        {
            playerCount++;
            hasJoined[index] = true;
            uc.TogglePlayerUi(index);
            mc.players[index].SetActive(true);
        }
        else
        {
            Debug.Log("max player count reached!");
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        GetComponent<TimerController>().count = true;
        uc.ToggleDpad(true);
        uc.ToggleTimer(true);
        uc.ToggleConnectionUi();

        ac.PlaySong(MusicEnum.boardMusic);
        ac.PlaySong(MusicEnum.minigameMusic);
        ac.minigameSource.volume = 0;
    }

    public void GetInput()
    {
        //add controllers
        if (!gameStarted)
        {
            //Start Game
            if (playerCount >= 2)
            {
                //Kan inte använda switch i getbutton down.
                if (Input.GetButtonDown("C1 Start"))
                {
                    StartGame();
                }

                if (Input.GetButtonDown("C2 Start"))
                {
                    StartGame();
                }

                if (Input.GetButtonDown("C3 Start"))
                {
                    StartGame();
                }

                if (Input.GetButtonDown("C4 Start"))
                {
                    StartGame();
                }

                //keyboard
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    StartGame();
                }
            }

            //Connect players
            if (Input.GetButtonDown("C1 Select") && !hasJoined[0])
            {
                AddPlayer(0);
            }

            if (Input.GetButtonDown("C2 Select") && !hasJoined[1] && hasJoined[0])
            {
                AddPlayer(1);
            }

            if (Input.GetButtonDown("C3 Select") && !hasJoined[2] && hasJoined[1])
            {
                AddPlayer(2);
            }

            if (Input.GetButtonDown("C4 Select") && !hasJoined[3] && hasJoined[2])
            {
                AddPlayer(3);
            }

            //keyboard
            switch (Input.inputString)
            {
                case "q":
                    if (!hasJoined[0])
                    {
                        AddPlayer(0);
                    }
                    break;
                case "w":
                    if (!hasJoined[1] && hasJoined[0])
                    {
                        AddPlayer(1);
                    }
                    break;
                case "e":
                    if (!hasJoined[2] && hasJoined[1])
                    {
                        AddPlayer(2);
                    }
                    break;
                case "r":
                    if (!hasJoined[3] && hasJoined[2])
                    {
                        AddPlayer(3);
                    }
                    break;
                default:
                    break;
            }
        }
        else if(gameStarted)
        {
            //take input mid game
            //för plebsen utan xbox controllers. lmao
            switch (Input.inputString)
            {
                case "1":
                    if (!hasPressedKey[0])
                    {
                        gc.HandleQueueInputs(0, 0);
                    }
                    break;
                case "2":
                    if (!hasPressedKey[1])
                    {
                        gc.HandleQueueInputs(1, 0);
                    }
                    break;
                case "3":
                    if (!hasPressedKey[2])
                    {
                        gc.HandleQueueInputs(2, 0);
                    }
                    break;
                case "4":
                    if (!hasPressedKey[3])
                    {
                        gc.HandleQueueInputs(3, 0);
                    }
                    break;
                default:
                    break;
            }

            //p1
            if (hasJoined[0] && !hasPressedKey[0])
            {
                var c1Horizontal = Input.GetAxis("C1 Horizontal");
                var c1Vertical = Input.GetAxis("C1 Vertical");

                switch (c1Horizontal)
                {
                    case 1:
                        gc.HandleQueueInputs(0, 1); //right
                        break;
                    case -1:
                        gc.HandleQueueInputs(0, 3);//left
                        break;
                    default:
                        break;
                }

                switch (c1Vertical)
                {
                    case 1:
                        gc.HandleQueueInputs(0, 0); //up
                        break;
                    case -1:
                        gc.HandleQueueInputs(0, 2);//down
                        break;
                    default:
                        break;
                }
            }

            //p2
            if (hasJoined[1] && !hasPressedKey[1])
            {
                var c2Horizontal = Input.GetAxis("C2 Horizontal");
                var c2Vertical = Input.GetAxis("C2 Vertical");

                switch (c2Horizontal)
                {
                    case 1:
                        gc.HandleQueueInputs(1, 1); //right
                        break;
                    case -1:
                        gc.HandleQueueInputs(1, 3);//left
                        break;
                    default:
                        break;
                }

                switch (c2Vertical)
                {
                    case 1:
                        gc.HandleQueueInputs(1, 0); //up
                        break;
                    case -1:
                        gc.HandleQueueInputs(1, 2);//down
                        break;
                    default:
                        break;
                }
            }

            //p3
            if (hasJoined[2] && !hasPressedKey[2])
            {
                var c3Horizontal = Input.GetAxis("C3 Horizontal");
                var c3Vertical = Input.GetAxis("C3 Vertical");

                switch (c3Horizontal)
                {
                    case 1:
                        gc.HandleQueueInputs(2, 1); //right
                        break;
                    case -1:
                        gc.HandleQueueInputs(2, 3);//left
                        break;
                    default:
                        break;
                }

                switch (c3Vertical)
                {
                    case 1:
                        gc.HandleQueueInputs(2, 0); //up
                        break;
                    case -1:
                        gc.HandleQueueInputs(2, 2);//down
                        break;
                    default:
                        break;
                }
            }

            //p4
            if (hasJoined[3] && !hasPressedKey[3])
            {
                var c4Horizontal = Input.GetAxis("C4 Horizontal");
                var c4Vertical = Input.GetAxis("C4 Vertical");

                switch (c4Horizontal)
                {
                    case 1:
                        gc.HandleQueueInputs(3, 1); //right
                        break;
                    case -1:
                        gc.HandleQueueInputs(3, 3);//left
                        break;
                    default:
                        break;
                }

                switch (c4Vertical)
                {
                    case 1:
                        gc.HandleQueueInputs(3, 0); //up
                        break;
                    case -1:
                        gc.HandleQueueInputs(3, 2);//down
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void OnGUI()
    {
        if (GetComponent<DevController>().debugBool)
        {
            var c1Hor = Input.GetAxis("C1 Horizontal");
            var c2Hor = Input.GetAxis("C2 Horizontal");
            var c3Hor = Input.GetAxis("C3 Horizontal");
            var c4Hor = Input.GetAxis("C4 Horizontal");

            var c1Ver = Input.GetAxis("C1 Vertical");
            var c2Ver = Input.GetAxis("C2 Vertical");
            var c3Ver = Input.GetAxis("C3 Vertical");
            var c4Ver = Input.GetAxis("C4 Vertical");

            string debugString = "P1: " + c1Hor.ToString("F2") + ", " + c1Ver.ToString("F2")
                + "\n" + "P2: " + c2Hor.ToString("F2") + ", " + c2Ver.ToString("F2")
                + "\n" + "P3: " + c3Hor.ToString("F2") + ", " + c3Ver.ToString("F2")
                + "\n" + "P4: " + c4Hor.ToString("F2") + ", " + c4Ver.ToString("F2");

            GUI.Label(new Rect(10, Screen.height - 100, 250, 100), debugString, style);
        }
    }
}

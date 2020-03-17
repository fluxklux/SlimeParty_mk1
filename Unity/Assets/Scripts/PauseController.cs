using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel = null;
    [SerializeField] private GameObject selectionMarker = null;
    [SerializeField] private GameObject[] pauseButtons = { null, null };

    [Header("Options Stuff")]
    [SerializeField] private GameObject optionsPanel = null;
    [SerializeField] private GameObject[] optionsSliders = { null, null };

    [Header("Sound Stuff")]
    [SerializeField] private Slider soundSlider = null;
    [SerializeField] private Slider musicSlider = null;

    [SerializeField] private AudioSource soundSource = null;

    [HideInInspector]
    public bool canPause = false;
    [HideInInspector]
    public bool paused = false;

    private bool gameEnded = false;
    private int playerInControll = 0;
    private int selectedButton = 0;

    private void Update()
    {
        if (Input.GetButtonDown("C1 Start"))
        {
            TogglePause(0);
        }

        if (Input.GetButtonDown("C2 Start"))
        {
            TogglePause(1);
        }

        if (Input.GetButtonDown("C3 Start"))
        {
            TogglePause(2);
        }

        if (Input.GetButtonDown("C4 Start"))
        {
            TogglePause(3);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(0);
        }

        if(paused)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Application.Quit();
            }

            if(!optionsPanel.activeSelf)
            {
                //player who paused will controll the menu
                var playerVertical = Input.GetAxis("C" + (playerInControll + 1) + " Vertical");

                switch (playerVertical)
                {
                    case 1:
                        MoveSelectionMarker(-1);
                        break;
                    case -1:
                        MoveSelectionMarker(1);
                        break;
                    default:
                        break;
                }

                if (Input.GetButtonDown("C" + (playerInControll + 1) + " Select"))
                {
                    switch (selectedButton)
                    {
                        case 0:
                            pausePanel.SetActive(false);
                            optionsPanel.SetActive(true);
                            selectionMarker.transform.position = optionsSliders[0].transform.position;
                            break;
                        case 1:
                            Application.Quit();
                            break;
                        default:
                            Debug.Log("Something went wrong");
                            break;
                    }
                }
            }
            else
            {
                //player who paused will controll the menu
                var playerVertical = Input.GetAxis("C" + (playerInControll + 1) + " Vertical");

                switch (playerVertical)
                {
                    case 1:
                        MoveSelectionMarkerOptions(-1);
                        break;
                    case -1:
                        MoveSelectionMarkerOptions(1);
                        break;
                    default:
                        break;
                }

                //sound slider
                if(selectedButton == 0)
                {
                    var playerHorizontal = Input.GetAxis("C" + (playerInControll + 1) + " Horizontal");

                    switch (playerHorizontal)
                    {
                        case 1:
                            soundSlider.value += 0.5f * Time.unscaledDeltaTime;
                            break;
                        case -1:
                            soundSlider.value -= 0.5f * Time.unscaledDeltaTime;
                            break;
                        default:
                            break;
                    }
                }

                //music slider
                if (selectedButton == 1)
                {
                    var playerHorizontal = Input.GetAxis("C" + (playerInControll + 1) + " Horizontal");

                    switch (playerHorizontal)
                    {
                        case 1:
                            musicSlider.value += 0.5f * Time.unscaledDeltaTime;
                            GetComponent<AudioController>().UpdateMusicVolume(GetComponent<MinigameController>().musicThreshold);
                            break;
                        case -1:
                            musicSlider.value -= 0.5f * Time.unscaledDeltaTime;
                            GetComponent<AudioController>().UpdateMusicVolume(GetComponent<MinigameController>().musicThreshold);
                            break;
                        default:
                            break;
                    }
                }

                if (Input.GetButtonDown("C" + (playerInControll + 1) + " Select"))
                {
                    switch (selectedButton)
                    {
                        case 0:
                            //nothing
                            break;
                        case 1:
                            //nothing
                            break;
                        case 2:
                            pausePanel.SetActive(true);
                            optionsPanel.SetActive(false);
                            selectionMarker.transform.position = pauseButtons[0].transform.position;
                            break;
                        default:
                            Debug.Log("Something went wrong");
                            break;
                    }
                }
            }
        }
    }

    public void OnChangeSoundVolume ()
    {
        soundSource.volume = soundSlider.value;
    }

    public void OnChangeMusicVolume()
    {
        GetComponent<MinigameController>().musicThreshold = musicSlider.value;
        //minigameMusicSource.volume = musicSlider.value;
    }

    private void MoveSelectionMarkerOptions (int dir)
    {
        selectedButton += dir;
        selectedButton = Mathf.Clamp(selectedButton, 0, optionsSliders.Length - 1);
        //Debug.Log(selectedButton);

        selectionMarker.transform.position = pauseButtons[selectedButton].transform.position;
    }

    private void MoveSelectionMarker (int dir)
    {
        selectedButton += dir;
        selectedButton = Mathf.Clamp(selectedButton, 0, pauseButtons.Length - 1);
        //Debug.Log(selectedButton);

        selectionMarker.transform.position = pauseButtons[selectedButton].transform.position;
    }

    public IEnumerator delayPause ()
    {
        yield return new WaitForSeconds(0.5f);
        canPause = true;
    }

    public void EndGame ()
    {
        gameEnded = true;
    }

    private void TogglePause(int playerIndex)
    {
        playerInControll = playerIndex;

        if (!gameEnded && canPause)
        {
            paused = !paused;
            selectionMarker.SetActive(paused);
            pausePanel.SetActive(paused);

            if (paused)
            {
                optionsPanel.SetActive(false);
                Time.timeScale = 0.0f;
            }
            else
            {
                optionsPanel.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseText = null;
    private bool paused = false;
    private bool gameEnded = false;

    private void Update()
    {
        if (Input.GetButtonDown("C1 Start"))
        {
            TogglePause();
        }

        if (Input.GetButtonDown("C2 Start"))
        {
            TogglePause();
        }

        if (Input.GetButtonDown("C3 Start"))
        {
            TogglePause();
        }

        if (Input.GetButtonDown("C4 Start"))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if(paused)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Application.Quit();
            }
        }
    }

    public void EndGame ()
    {
        gameEnded = true;
    }

    private void TogglePause()
    {
        if(!gameEnded)
        {
            paused = !paused;
            pauseText.SetActive(paused);

            if (paused)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }
}

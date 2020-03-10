using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseText = null;
    private bool paused = false;

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
            paused = TogglePause();
            pauseText.SetActive(paused);
        }

        if(paused)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                Application.Quit();
            }
        }
    }

    private bool TogglePause()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
}

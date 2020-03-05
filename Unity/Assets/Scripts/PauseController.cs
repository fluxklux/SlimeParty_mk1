using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool paused = false;

    [Header("Visuals: ")]
    public GUIStyle style;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = TogglePause();
        }
    }

    private bool TogglePause()
    {
        if(Time.timeScale == 0f)
        {
            Debug.Log("About to unpause the game!");
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Debug.Log("About to pause the game!");
            Time.timeScale = 0f;
            return (true);
        }
    }

    private void OnGUI()
    {
        if (paused)
        { 
            string pauseText = "Game" + "\n" + "Paused";
            GUI.Label(new Rect(Screen.width/2 - 150, Screen.height/2 - 75, 300, 150), pauseText, style);

            if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 75, 300, 150), "Click here to unpause!", style))
            {
                paused = TogglePause();
            }
        }
    }
}

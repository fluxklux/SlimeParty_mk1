using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevController : MonoBehaviour
{
    [Header("Objects to hide: ")]
    public GameObject[] debugObjects;

    [Header("Visuals: ")]
    public GUIStyle style;

    private bool debugBool = false;
    private TimerController tc;
    private StateController sc;

    private void Start()
    {
        sc = GetComponent<StateController>();
        tc = GetComponent<TimerController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangeDebugMode();
        }
    }

    private void ChangeDebugMode ()
    {
        debugBool = !debugBool;

        if(debugBool)
        {
            for (int i = 0; i < debugObjects.Length; i++)
            {
                debugObjects[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < debugObjects.Length; i++)
            {
                debugObjects[i].SetActive(false);
            }
        }
    }

    private void OnGUI()
    {
        if(debugBool)
        {
            string stateText = "TIMER: " + tc.timer.ToString("F2") + "\n" + "ELAPSED TIME: " + tc.elapsedTime.ToString("F2") + "\n" + sc.currentState.ToString();
            GUI.Label(new Rect(10, 10, 200, 200), stateText, style);
        }
    }
}

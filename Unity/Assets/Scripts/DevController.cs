using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevController : MonoBehaviour
{
    public GameObject[] debugObjects;

    private bool debugBool = true;

    private void Start()
    {
        ChangeDebugMode();
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
                debugObjects[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < debugObjects.Length; i++)
            {
                debugObjects[i].SetActive(true);
            }
        }
    }
}

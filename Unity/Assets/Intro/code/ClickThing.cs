using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickThing : MonoBehaviour
{

    void Update()
    {
        var pointer = new PointerEventData(EventSystem.current);
        if (Input.GetButtonDown("C1 Select")) {
            ExecuteEvents.Execute(this.gameObject, pointer, ExecuteEvents.submitHandler);
        }
    }

    public void StartGame ()
    {
        SceneManager.LoadScene(1);
    }
}

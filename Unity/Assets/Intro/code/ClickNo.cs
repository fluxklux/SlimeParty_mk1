using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ClickNo : MonoBehaviour
{

    void Update()
    {
        var pointer = new PointerEventData(EventSystem.current);
        if (Input.GetButtonDown("C1 Back"))
        {
            ExecuteEvents.Execute(this.gameObject, pointer, ExecuteEvents.submitHandler);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour
{
    int selectedButton = 0;
    public GameObject selectionMarker;
    public Button[] Buttons;
    bool pressed = false;
    bool wentBack = true;

    void Update()
    {
        if (wentBack == true)
        {
            selectionMarker.transform.position = Buttons[selectedButton].transform.position;
            wentBack = false;
            Buttons[selectedButton].animator.SetBool("Normal", false);
            Buttons[selectedButton].animator.SetBool("Highlighted", true);
        }

        
        var playerVertical = Input.GetAxis("C1 Vertical");
        switch (playerVertical)
        {
            case 1:
                if (pressed == false)
                {
                    Buttons[selectedButton].animator.SetBool("Normal", true);
                    Buttons[selectedButton].animator.SetBool("Highlighted", false);
                    MoveSelectionMarker(-1);
                    Buttons[selectedButton].animator.SetBool("Normal", false);
                    Buttons[selectedButton].animator.SetBool("Highlighted", true);
                    pressed = true;
                }
                break;
            case -1:
                if (pressed == false)
                {

                    Buttons[selectedButton].animator.SetBool("Normal", true);
                    Buttons[selectedButton].animator.SetBool("Highlighted", false);
                    MoveSelectionMarker(1);
                    Buttons[selectedButton].animator.SetBool("Normal", false);
                    Buttons[selectedButton].animator.SetBool("Highlighted", true);
                    pressed = true;
                }
                break;
            default:
                if (pressed == true)
                {
                    pressed = false;
                }
                break;
        }



        if (Input.GetButtonDown("C1 Select")){
            wentBack = true;
            Buttons[selectedButton].onClick.Invoke();

        }
        if (Input.GetButtonDown("C1 Back"))
        {
            wentBack = true;
            MoveSelectionMarker(1);
            MoveSelectionMarker(1);
            MoveSelectionMarker(1);
            MoveSelectionMarker(1);
            MoveSelectionMarker(1);
            Buttons[selectedButton].onClick.Invoke();
        }

    }
    private void MoveSelectionMarker(int dir)
    {
        selectedButton += dir;
        selectedButton = Mathf.Clamp(selectedButton, 0, Buttons.Length - 1);

        selectionMarker.transform.position = Buttons[selectedButton].transform.position;
    }
}

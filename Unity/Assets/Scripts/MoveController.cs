using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public GameObject[] players;

    GameController gc;
    UIController uc;


    void Start()
    {
        gc = GetComponent<GameController>();
        uc = GetComponent<UIController>();
    }

    public void MovePlayers()
    {
        for (int i = 0; i < gc.queueObjects.Count; i++)
        {
            int playerIndex = gc.queueObjects[i].playerIndex;
            int calcIndex = players[playerIndex].GetComponent<PlayerController>().playerVariable.currentSlotPosition + gc.queueObjects[i].steps;
            calcIndex = (int)Mathf.Repeat(calcIndex, gc.allSlots.Length);

            if(gc.queueObjects[i].steps >= 5)
            {
                EngageJump(calcIndex);
            }
            else
            {
                EngageRun(calcIndex);
            }
        }
    }


    private void EngageRun(int calcIndex)
    {

    }

    private void EngageJump(int calcIndex)
    {

    }
}

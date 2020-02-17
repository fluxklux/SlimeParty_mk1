using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerVariables
{
    public int index;
    public int steps;

    public int currentSlotPosition = 0;
    public Slot currentSlotType = null;
    public int lastSlotIndex = 0;
    public bool isAlone = false;
    public bool wasFirst = false;
}

public class PlayerController : MonoBehaviour
{
    public PlayerVariables playerVariable;

    public void UpdatePosition(int newIndex)
    {
        playerVariable.lastSlotIndex = GetPositionIndex();
        playerVariable.currentSlotPosition = newIndex;
    }

    public int GetPositionIndex()
    {
        return playerVariable.currentSlotPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerVariables
{
    public int index;
    public int steps;
}

public class PlayerController : MonoBehaviour
{
    public PlayerVariables playerVariable;

    public int currentSlotPosition = 0;
    public Slot currentSlotType = null;
    public int lastSlotIndex = 0;
    public bool isAlone = false;
    public bool wasFirst = false;

    public void UpdatePosition(int newIndex)
    {
        lastSlotIndex = GetPositionIndex();
        currentSlotPosition = newIndex;
    }

    public int GetPositionIndex()
    {
        return currentSlotPosition;
    }
}

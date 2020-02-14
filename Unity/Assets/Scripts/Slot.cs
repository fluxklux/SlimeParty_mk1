using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType { plusThree, minusThree, plusTen, miniGame, chans };

[CreateAssetMenu(fileName = "new Slot", menuName = "Custom/Slot")]
public class Slot : ScriptableObject
{
    public SlotType slotType;
    public Color color;
}

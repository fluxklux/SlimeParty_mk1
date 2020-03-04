using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinigameType { Masher, Rotation }

[CreateAssetMenu(fileName = " new Minigame", menuName = "Custom/Minigame")]
public class Minigame : ScriptableObject
{
    public int index;
    public MinigameType minigameType;
    public float time;
}

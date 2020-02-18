using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceController : MonoBehaviour
{
    //TODO spice:a upp lite

    int chance = Random.Range(1, 100);

    public int randomEffect()
    {
        return this.chance;
    }
}

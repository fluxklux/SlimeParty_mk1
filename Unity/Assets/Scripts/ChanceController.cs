using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceController : MonoBehaviour
{
    //TODO spice:a upp lite
    int chance;

    GameController gc;


    private void Start()
    {
        gc = GetComponent<GameController>();
    }

    public void RandomResult()
    {

        chance = Random.Range(1, 4);

        switch (chance)
        {
            case 1:

                Debug.Log("do this: " + chance);

                break;

            case 2:

                Debug.Log("do that: " + chance);

                break;

            case 3:

                Debug.Log("do there: " + chance);

                break;

            case 4:

                Debug.Log("do their: " + chance);

                break;

            default:
                break;
        }
    }

    //public int randomEffect()
    //{
    //    return this.chance;
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dpad : MonoBehaviour
{
    public Text[] numbers;

    private int randomInt;
    private List<int> TakeNumbList = new List<int>();

    public int GetDPadNum(int index)
    {
        return TakeNumbList[index];
    }

    public void Randomize()
    {
        TakeNumbList = new List<int>(new int[numbers.Length]);

        for (int i = 0; i < numbers.Length; i++)
        {
            randomInt = Random.Range(1, 9);
            while (TakeNumbList.Contains(randomInt))
            {
                randomInt = Random.Range(1, 9);
            }

            TakeNumbList[i] = randomInt;
            numbers[i].text = TakeNumbList[i].ToString();
        }
    }
}
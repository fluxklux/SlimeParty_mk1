using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public bool vertical = false;
    bool pressed = false;
    float newposition = 0f;
    public float oldposition;
    public float scrollSpeed = 0.1f;
    public float maxamount = 0.99f;
    public float minamount = 0.01f;

    private void Start()
    {
        if (vertical == true)
        {
            oldposition = 0f;
        } 
        else 
        {
            oldposition = 1f;
        }
    }

    void Update()
    {

        if (vertical == true)
        {
            var playerVertical = Input.GetAxis("C1 Horizontal");
            switch (playerVertical)
            {


                case 1:
                    if (pressed == false)
                    {
                        if (oldposition < maxamount)
                        {
                            newposition = oldposition + scrollSpeed;
                            oldposition = newposition;
                        }
                        this.GetComponent<ScrollRect>().normalizedPosition = new Vector2(newposition, 1);
                        pressed = true;

                    }
                    break;
                case -1:
                    if (pressed == false)
                    {
                        if (oldposition > minamount)
                        {
                            newposition = oldposition - scrollSpeed;
                            oldposition = newposition;
                        }
                        this.GetComponent<ScrollRect>().normalizedPosition = new Vector2(newposition, 1);
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
        }
        else {
            var playerVertical = Input.GetAxis("C1 Vertical");
            switch (playerVertical)
            {

                case 1:
                    if (pressed == false)
                    {
                        if (oldposition < maxamount)
                        {
                            newposition = oldposition + scrollSpeed;
                            oldposition = newposition;
                        }
                        this.GetComponent<ScrollRect>().normalizedPosition = new Vector2(1, newposition);
                        pressed = true;

                    }
                    break;
                case -1:
                    if (pressed == false)
                    {
                        if (oldposition > minamount)
                        {
                            newposition = oldposition - scrollSpeed;
                            oldposition = newposition;
                        }
                        this.GetComponent<ScrollRect>().normalizedPosition = new Vector2(1, newposition);
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
        }
    }

}

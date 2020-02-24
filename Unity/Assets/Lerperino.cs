using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerperino : MonoBehaviour
{
    [SerializeField] private float damping = 0;
    [SerializeField] private Transform point = null;

    bool lerpDone = true;

    private void Update()
    {
        if(!lerpDone)
        {
            Lerp();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            MovePlayer();
        }
    }

    public void MovePlayer()
    {
        lerpDone = false;
    }

    private void Lerp ()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, damping);

        float dist = Vector2.Distance(transform.position, point.position);
        if (dist < 0.1f)
        {
            lerpDone = true;
        }
    }
}

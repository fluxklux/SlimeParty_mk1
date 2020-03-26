using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunApple : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform target;
    private int playerIndex;

    private void Update()
    {
        GetDistance();
        UpdateScale();

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
    }

    public void SetPlayerIndex (int index)
    {
        playerIndex = index;
    }

    public void SetTarget (Transform newTarget)
    {
        target = newTarget;
    }

    private void GetDistance ()
    {
        float dist = Vector2.Distance(target.position, transform.position);

        if(dist < 0.1f)
        {
            FindObjectOfType<ChanceController>().SkipPlayerTurn(playerIndex);
            Destroy(gameObject);
        }
    }

    private void UpdateScale()
    {
        float maxY = 5;
        float yPos = transform.position.y;
        float differents;

        differents = maxY - yPos;

        Vector3 minScale = new Vector3(1f, 1f, 1f);
        Vector3 maxScale = new Vector3(1.5f, 1.5f, 1.5f);
        float playerScale;

        playerScale = Mathf.Lerp(minScale.y, maxScale.y, differents - 4);
        transform.localScale = new Vector3(playerScale, playerScale, 0.0f);
    }
}

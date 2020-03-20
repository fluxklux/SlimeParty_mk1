using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform[] wayPoints;

    private int currentWayPoint;

    public float speed;

    public float timeToWait;

    private float waitTimer;

    private int pointToWaitAt;

    bool isWalking = true;

    private void Start()
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i].transform.SetParent(null, false);
        }

        SetRandomPointToWait();
    }

    void Update()
    {
        MoveToWayPoint();

        if (!isWalking)
        {
            waitTimer += Time.fixedDeltaTime;

            if (waitTimer >= timeToWait)
            {
                isWalking = true;
                waitTimer = 0;
                SetRandomPointToWait();
            }
        }
    }

    void MoveToWayPoint()
    {
        if (transform.position == wayPoints[currentWayPoint].position)
        {
            currentWayPoint++;

            if (currentWayPoint == pointToWaitAt)
            {
                isWalking = false;

                Debug.Log("HELLO");
            }

            if (currentWayPoint == wayPoints.Length)
            {
                currentWayPoint = currentWayPoint - wayPoints.Length;
            }
            else
            {
                Debug.Log(currentWayPoint);
                Move();
            }
        }
        else
        {
            Move();
        }
    }

    void SetRandomPointToWait()
    {
        pointToWaitAt = Random.Range(1, wayPoints.Length);

        Debug.Log("randomPoint is " + pointToWaitAt);
    }

    void Move()
    {
        if (isWalking)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPoint].position, speed * Time.fixedDeltaTime);
        }
        else
        {
            Debug.Log("am not moving!");
        }
    }
}
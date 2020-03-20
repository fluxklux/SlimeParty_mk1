using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float speed;
    public float timeToWait;
    public bool onlyIdle;
    public Transform[] wayPoints;

    private int currentWayPoint;
    private int pointToWaitAt;
    private float waitTimer;
    private bool isWalking = true;
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();

        if (wayPoints.Length > 0)
        {
            for (int i = 0; i < wayPoints.Length; i++)
            {
                wayPoints[i].transform.SetParent(null, false);
            }
            SetRandomPointToWait();
        }
        else
        {
            onlyIdle = true;
        }

    }

    void Update()
    {
        MoveToWayPoint();
        UpdateScale();

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
        if (!onlyIdle)
        {
            if (transform.position == wayPoints[currentWayPoint].position)
            {
                currentWayPoint++;

                if (currentWayPoint == pointToWaitAt)
                {
                    isWalking = false;
                }

                if (currentWayPoint == wayPoints.Length)
                {
                    currentWayPoint = currentWayPoint - wayPoints.Length;
                }
                else
                {
                    Move();
                }
            }
            else
            {
                Move();
            }
        }
    }

    void SetRandomPointToWait()
    {
        pointToWaitAt = Random.Range(1, wayPoints.Length);
    }

    void Move()
    {
        if (isWalking && !onlyIdle)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPoint].position, speed * Time.fixedDeltaTime);
            anim.SetBool("WalkingBool", true);
        }
        else
        {
            anim.SetBool("WalkingBool", false);
        }
    }

    public void UpdateScale()
    {
        float maxY = 5;

        float yPos = transform.position.y;

        float differents;

        differents = maxY - yPos;

        Vector3 minScale = new Vector3(1f, 1f, 1f);

        Vector3 maxScale = new Vector3(1.5f, 1.5f, 1.5f);

        float nonPlayableCharacterScale;

        nonPlayableCharacterScale = Mathf.Lerp(minScale.y, maxScale.y, differents - 4);

        transform.localScale = new Vector3(nonPlayableCharacterScale, nonPlayableCharacterScale, 0.0f);
    }

}
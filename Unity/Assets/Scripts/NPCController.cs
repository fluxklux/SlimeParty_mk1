using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NpcType { Idle, Walking};

public class NPCController : MonoBehaviour
{
    public NpcType npcType;

    public Transform[] wayPoints;
    public float speed;
    public float timeToWait;

    private int currentWayPoint;
    private float waitTimer;
    private int pointToWaitAt;
    private bool isWalking = true;
    private Animator anim;
    private int timeBeforePlayAnim;
    private SpriteRenderer npcSprite;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();

        npcSprite = GetComponentInChildren<SpriteRenderer>();

        if (wayPoints.Length <= 0)
        {
            isWalking = false;
            npcType = NpcType.Idle;
        }
        else
        {
            for (int i = 0; i < wayPoints.Length; i++)
            {
                wayPoints[i].transform.SetParent(null, true);
            }
        }

        if (npcType == NpcType.Walking)
        {
            SetRandomPointToWait();
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

    private void Update()
    {
        if (npcType == NpcType.Walking)
        {
            MoveToWayPoint();
        }

        UpdateScale();

        if (npcType == NpcType.Idle)
        {
            anim.SetBool("Moving", false);
        }


        if (!isWalking)
        {
            anim.SetBool("Moving", false);
            waitTimer += Time.fixedDeltaTime;

            if (waitTimer >= timeToWait)
            {
                isWalking = true;
                waitTimer = 0;
                SetRandomPointToWait();
            }
        }
    }

    private void MoveToWayPoint()
    {
        if (isWalking)
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

    private void SetRandomPointToWait()
    {
        pointToWaitAt = Random.Range(1, wayPoints.Length);
    }

    private void Move()
    {
        if (isWalking)
        {
            anim.SetBool("Moving", true);
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPoint].position, speed * Time.fixedDeltaTime);
        }
    }
}
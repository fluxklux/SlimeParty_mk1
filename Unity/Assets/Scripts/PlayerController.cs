using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType { PlusFruit3, PlusFruit10, MinusFruit3, Chance, Minigame }

[System.Serializable]
public class PlayerVariables
{
    public ActionType actionType;
    public int steps;
    public int extraFruits; //alltid 10 atm.

    public bool wasFirst;
    public int currentSlotPosition = 0;
    public int currentSlotOrder = 0;
    public int lastSlotOrder = 0;
    public int lastSlotIndex = 0;
    public Color color;

    public bool skip = false;
}

public class PlayerController : MonoBehaviour
{
    public float damping = 0.5f;
    public PlayerVariables playerVariable;

    private InputController ic;
    private Vector2 targetPos;
    private int targetPlayer;
    private bool lerp;

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        ic = Object.FindObjectOfType<InputController>();
    }

    public void Lerp(int playerIndex, Vector2 targetSlot)
    {
        lerp = true;
        targetPlayer = playerIndex;
        targetPos = targetSlot;
    }

    private void Update()
    {
        if (lerp)
        {
            anim.SetFloat("Speed", 1);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, damping);
            UpdateScale();

            float dist = Vector2.Distance(transform.position, targetPos);
            if (dist < 0.1f)
            {
                lerp = false;
            }
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }

    public void UpdatePosition(int newIndex)
    {
        playerVariable.lastSlotIndex = GetPositionIndex();
        playerVariable.currentSlotPosition = newIndex;
    }

    public void UpdateScale()
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

    public int GetPositionIndex()
    {
        return playerVariable.currentSlotPosition;
    }
}
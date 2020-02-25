using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerVariables
{
    public int index;
    public int steps;

    public int currentSlotPosition = 0;
    public int currentSlotOrder = 0;
    public Slot currentSlotType = null;
    public int lastSlotIndex = 0;
    public bool isAlone = false;
    public bool wasFirst = false;
    public Color color;
}

public class PlayerController : MonoBehaviour
{
    public float damping = 0.5f;
    public PlayerVariables playerVariable;

    private InputController ic;
    private Transform targetPos;
    private int targetPlayer;
    private bool lerp;

    private void Start()
    {
        ic = Object.FindObjectOfType<InputController>();
    }

    public void Lerp(int playerIndex, Transform targetSlot)
    {
        lerp = true;
        targetPlayer = playerIndex;
        targetPos = targetSlot;
    }

    private void Update()
    {
        if (lerp)
        {
            Debug.Log(">:(");
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, damping);

            float dist = Vector2.Distance(transform.position, targetPos.position);
            if (dist < 0.1f)
            {
                lerp = false;
            }
        }
    }

    public void UpdatePosition(int newIndex)
    {
        playerVariable.lastSlotIndex = GetPositionIndex();
        playerVariable.currentSlotPosition = newIndex;
    }

    public void UpdateScale(float highestSlotY)
    {

        float maxY = highestSlotY;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] private float damping;

    private new SpriteRenderer renderer;
    private Color goalColor;

    private void Start ()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        goalColor = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        renderer.color = Color.Lerp(renderer.color, goalColor, damping);
    }
}

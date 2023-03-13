using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OverworldCollectible : MonoBehaviour
{
    new public Collider2D collider;
    new public SpriteRenderer renderer;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        collider.enabled = true;
        var oldColor = renderer.color;
        oldColor.a = 1.0f;
        renderer.color = oldColor;
    }

    public void Deactivate()
    {
        collider.enabled = false;
        var oldColor = renderer.color;
        oldColor.a = 0.3f;
        renderer.color = oldColor;
    }
}

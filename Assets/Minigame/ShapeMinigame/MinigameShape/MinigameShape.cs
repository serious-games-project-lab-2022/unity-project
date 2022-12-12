using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinigameShape : MonoBehaviour
{
    [HideInInspector] public Collider2D hitbox;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    private Vector2 grabOffset = Vector2.zero;
    private Vector2 oldPosition = Vector2.zero;
    private Vector2 olderPosition = Vector2.zero;

    [HideInInspector] public bool isBeingDragged = false;
    private bool isIntersecting = false;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();

        oldPosition = new Vector2(body.position.x, body.position.y);
        olderPosition = new Vector2(body.position.x, body.position.y);
    }

    public void Move(Vector2 to)
    {
        if (isBeingDragged)
        {
            if (!isIntersecting)
            {
                if (oldPosition != body.position)
                {
                    olderPosition = new Vector2(oldPosition.x, oldPosition.y);
                    oldPosition = new Vector2(body.position.x, body.position.y);
                }
            }

            var newPosition = Snapping.Snap(to - grabOffset, Vector2.one * 0.5f);
            body.MovePosition(newPosition);
        }
    }

    public void Grab(Vector2 at)
    {
        if (!isBeingDragged)
        {
            isBeingDragged = true;
            grabOffset = at - body.position;
            DrawOnTopOfOthers();
        }
    }

    public void Release()
    {
        isBeingDragged = false;
        grabOffset = Vector2.zero;
        DrawOnSameLevelAsOthers();
        ResetOpacity();
        if (isIntersecting)
        {
            body.position = olderPosition;
        }
    }

    private void DrawOnTopOfOthers()
    {
        GetComponent<Renderer>().sortingOrder = 1;
    }

    private void DrawOnSameLevelAsOthers()
    {
        GetComponent<Renderer>().sortingOrder = 0;
    }

    private void DecreaseOpacity()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
    }

    private void ResetOpacity()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBeingDragged && collision.gameObject.tag == "MinigameShape")
        {
            isIntersecting = true;
            DecreaseOpacity();
        }

        if (collision.gameObject.tag == "ShapeMinigameArea")
        {
            body.position = olderPosition;
            Release();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MinigameShape")
        {
            isIntersecting = false;
            ResetOpacity();
        }
    }
}

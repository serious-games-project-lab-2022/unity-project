using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinigameShape : MonoBehaviour
{
    [HideInInspector] public Collider2D hitbox;
    public Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;
    private TilemapRenderer tilemapRenderer;

    private Vector2 oldPosition = Vector2.zero;
    private Vector2 olderPosition = Vector2.zero;

    [HideInInspector] public bool isBeingDragged = false;
    private bool isIntersecting = false;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<TilemapCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
        tilemapRenderer = GetComponent<TilemapRenderer>();

        oldPosition = new Vector2(body.position.x, body.position.y);
        olderPosition = new Vector2(body.position.x, body.position.y);
    }

    public void Move(Vector2 to)
    {
        var newPosition = to;
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
            body.MovePosition(newPosition);
        }
    }

    public void Grab()
    {
        if (!isBeingDragged)
        {
            isBeingDragged = true;
            DrawOnTopOfOthers();
        }
    }

    public void Release()
    {
        isBeingDragged = false;
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
        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0.5f);
    }

    private void ResetOpacity()
    {
        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 1f);
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

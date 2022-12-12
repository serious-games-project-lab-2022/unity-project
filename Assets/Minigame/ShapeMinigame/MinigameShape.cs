using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinigameShape : MonoBehaviour
{
    public TilemapCollider2D hitbox;
    private Rigidbody2D body;
    private TilemapRenderer tilemapRenderer;
    private Tilemap tilemap;

    private Vector2 grabOffset = Vector2.zero;
    private Vector2 oldPosition = Vector2.zero;
    private Vector2 olderPosition = Vector2.zero;

    public bool isBeingDragged = false;
    private bool isIntersecting = false;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<TilemapCollider2D>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
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

            var newPosition = Snapping.Snap(to - grabOffset, Vector2.one);
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
        if (isIntersecting)
        {
            body.position = olderPosition;
        }
    }

    private void DrawOnTopOfOthers()
    {
        tilemapRenderer.sortingOrder = 1;
    }

    private void DrawOnSameLevelAsOthers()
    {
        tilemapRenderer.sortingOrder = 0;
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBeingDragged && collision.gameObject.tag == "MinigameShape")
        {
            isIntersecting = true;
            tilemap.color += new Color(0, 0, 0, -0.5f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isIntersecting = false;
        tilemap.color += new Color(0, 0, 0, 0.5f);
    }
}

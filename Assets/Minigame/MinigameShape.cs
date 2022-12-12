using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinigameShape : MonoBehaviour
{

    private Rigidbody2D body;
    private TilemapCollider2D partCollider;

    private Vector2 mouseOffset = Vector2.zero;
    private Vector2 oldPosition = Vector2.zero;
    private Vector2 olderPosition = Vector2.zero;
    private bool isBeingDragged = false;
    private bool isIntersecting = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        partCollider = GetComponent<TilemapCollider2D>();
        oldPosition = new Vector2(body.position.x, body.position.y);
        olderPosition = new Vector2(body.position.x, body.position.y);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (partCollider.OverlapPoint(mousePosition))
            {
                if (!isBeingDragged)
                {
                    isBeingDragged = true;
                    mouseOffset = mousePosition - body.position;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isBeingDragged = false;
            mouseOffset = Vector2.zero;
            if (isIntersecting)
            {
                body.position = olderPosition;
            }
        }
    }

    void FixedUpdate()
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

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var newPosition = Snapping.Snap(mousePosition - mouseOffset, Vector2.one);
            body.MovePosition(newPosition);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBeingDragged && collision.gameObject.tag == "MinigameShape")
        {
            isIntersecting = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isIntersecting = false;
    }
}

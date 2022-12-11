using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinigamePart : MonoBehaviour
{

    private Rigidbody2D body;
    private TilemapCollider2D partCollider;

    private Vector2 mouseOffset = Vector2.zero;
    private bool isBeingDragged = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        partCollider = GetComponent<TilemapCollider2D>();
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
        }
    }

    void FixedUpdate()
    {
        if (isBeingDragged)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var newPosition = Snapping.Snap(mousePosition - mouseOffset, Vector2.one);
            body.MovePosition(newPosition);
        }
    }

    void OnTrigger()
    {
        Debug.Log("Intersecting");
    }
}

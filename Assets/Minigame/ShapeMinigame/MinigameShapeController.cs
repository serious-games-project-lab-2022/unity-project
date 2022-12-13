using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameShapeController : MonoBehaviour
{
    private MinigameShape[] shapes;
    private MinigameShape currentlyDraggedShape = null;
    private Vector2 grabOffset = Vector2.zero;
    private Grid grid;

    void Start()
    {
        shapes = FindObjectsOfType<MinigameShape>();
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && currentlyDraggedShape == null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (var shape in shapes)
            {
                if (shape.hitbox.OverlapPoint(mousePosition))
                {
                    currentlyDraggedShape = shape;
                    grabOffset = mousePosition - shape.body.position;
                    currentlyDraggedShape.Grab();
                    break;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (currentlyDraggedShape != null)
            {
                currentlyDraggedShape.Release();
                grabOffset = Vector2.zero;
                currentlyDraggedShape = null;
            }
        }
    }

    void FixedUpdate()
    {
        if (currentlyDraggedShape != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var newPosition = Snapping.Snap(mousePosition - grabOffset, (Vector2) grid.cellSize);
            currentlyDraggedShape.Move(to: newPosition);
        }
    }
}

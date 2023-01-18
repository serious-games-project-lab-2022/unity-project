using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameShapeController : MonoBehaviour
{
    private List<MinigameShape> shapes;
    private MinigameShape currentlyDraggedShape = null;
    private Vector2 grabOffset = Vector2.zero;
    private Grid grid;

    private bool canMoveShapes;
    private Camera minigameCamera;

    public void SetShapes(List<MinigameShape> newShapes)
    {
        shapes = newShapes;
    }

    void Start()
    {
        grid = GetComponent<Grid>();
        minigameCamera = GameObject.FindGameObjectWithTag("MinigameCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (canMoveShapes)
        {
            if (Input.GetMouseButton(0) && currentlyDraggedShape == null)
            {
                Vector2 mousePosition = minigameCamera.ScreenToWorldPoint(Input.mousePosition);
                foreach (var shape in shapes)
                {
                    if (shape.hitbox.OverlapPoint(mousePosition))
                    {
                        currentlyDraggedShape = shape;
                        grabOffset = mousePosition - shape.body.position;
                        break;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (currentlyDraggedShape != null)
                {
                    grabOffset = Vector2.zero;
                    currentlyDraggedShape = null;
                }
            }
        }
    }

    void FixedUpdate()
    {
       if (canMoveShapes)
       {
           if (currentlyDraggedShape != null)
           {
                Vector2 mousePosition = minigameCamera.ScreenToWorldPoint(Input.mousePosition);
                var newPosition = Snapping.Snap(mousePosition - grabOffset, (Vector2) grid.cellSize);
                currentlyDraggedShape.Move(to: newPosition);
           }
       }
    }
    
    public void SetCanMoveShapes(bool CanMove)
    {
        canMoveShapes = CanMove;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameShapeController : MonoBehaviour
{
    private MinigameShape[] shapes;
    private MinigameShape currentlyDraggedShape = null;
    private Vector2 grabOffset = Vector2.zero;
    private Grid grid;

    private bool canMoveShapes;

    void Start()
    {
        shapes = FindObjectsOfType<MinigameShape>();
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        if (canMoveShapes)
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
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var newPosition = Snapping.Snap(mousePosition - grabOffset, (Vector2) grid.cellSize);
                currentlyDraggedShape.Move(to: newPosition);
           }
       }
    }
    
    public void SetCanMoveShapes(bool CanMove)
    {
        canMoveShapes = CanMove;
    }
    
    // Computes the relative positions of shapes to each other. Picks the first shape and saves a list of vectors from the first shape to all other shapes
    void computeRelativePostions()
    {
        if (shapes.Length!=0)
        {
            Vector2 relativePositionOfPair;
            foreach (var shape in shapes)
            {
                //relativePositionOfPair = var.TBC
            }
        }
    }
}

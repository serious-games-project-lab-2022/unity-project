using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameShapeController : MonoBehaviour
{
    private MinigameShape[] shapes;
    private MinigameShape currentlyDraggedShape = null;

    void Start()
    {
        shapes = FindObjectsOfType<MinigameShape>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (var shape in shapes)
            {
                if (shape.hitbox.OverlapPoint(mousePosition))
                {
                    if (currentlyDraggedShape == null)
                    {
                        currentlyDraggedShape = shape;
                        currentlyDraggedShape.Grab(at: mousePosition);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentlyDraggedShape.Release();
            currentlyDraggedShape = null;
        }
    }

    void FixedUpdate()
    {
        if (currentlyDraggedShape != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentlyDraggedShape.Move(to: mousePosition);
        }
    }
}

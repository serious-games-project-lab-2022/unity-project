using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinigameShape : MonoBehaviour
{
    [HideInInspector] public Collider2D hitbox;
    public Rigidbody2D body;
    private Tilemap tilemap;
    List<Vector3Int> usedCellCoordinates;

    public void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<TilemapCollider2D>();
        tilemap = GetComponent<Tilemap>();
        usedCellCoordinates = GetAllUsedCellCoordinates();
    }

    public void Move(Vector2 to)
    {
        var newPosition = to;
        if (newPosition == body.position) return;
        if 
        (
            DoesCollideWithOtherShapeWhenMovingTo(newPosition)
            || LeavesAllowedAreaWhenMovingTo(newPosition)
        ) return;

        body.MovePosition(newPosition);
    }

    public bool DoesCollideWithOtherShapeWhenMovingTo(Vector2 newPosition)
    {
        var relativeMovement = (Vector3) (newPosition - body.position);
        foreach (var cellPosition in usedCellCoordinates)
        {
            var worldPosition = tilemap.CellToWorld(cellPosition) + tilemap.cellSize * 0.5f;
            var rayStart = worldPosition + relativeMovement;
            var rayEnd = worldPosition + relativeMovement + relativeMovement.normalized * 0.1f;
            var hits = Physics2D.RaycastAll(rayStart, rayEnd, distance: relativeMovement.magnitude * 0.1f);
            foreach (var hit in hits)
            {
                if (hit.collider == null) continue;
                if (hit.collider == this.hitbox) continue;
                if (hit.collider.gameObject.tag != "MinigameShape") continue;

                return true;
            }
        }
        return false;
    }

    private bool LeavesAllowedAreaWhenMovingTo(Vector2 newPosition)
    {
        var relativeMovement = (Vector3) (newPosition - body.position);
        foreach (var cellPosition in usedCellCoordinates)
        {
            var worldPosition = tilemap.CellToWorld(cellPosition) + tilemap.cellSize * 0.5f;
            var rayStart = worldPosition + relativeMovement;
            var rayEnd = worldPosition + relativeMovement + relativeMovement.normalized * 0.1f;
            var hits = Physics2D.RaycastAll(rayStart, rayEnd, distance: relativeMovement.magnitude * 0.1f);
            var cellInAllowedArea = false;
            foreach (var hit in hits)
            {
                if (hit.collider == null) continue;
                if (hit.collider.gameObject.tag == "ShapeMinigameArea")
                {
                    cellInAllowedArea = true;
                }
            }
            if (!cellInAllowedArea)
            {
                return true;
            }
        }
        return false;
    }

    private List<Vector3Int> GetAllUsedCellCoordinates()
    {
        var allTiles = new List<Vector3Int>();
        foreach (var cellPosition in tilemap.cellBounds.allPositionsWithin)
        {   
            if (!tilemap.HasTile(cellPosition)) continue;

            allTiles.Add(cellPosition);
        }
        return allTiles;
    }

    private void DecreaseOpacity()
    {
        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0.5f);
    }

    private void ResetOpacity()
    {
        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 1f);
    }
}

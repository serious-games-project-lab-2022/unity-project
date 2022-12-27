using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShapeMinigame : Minigame
{
    public List<MinigameShape> shapePrefabs;

    void GenerateConfiguration()
    {
        Random.seed = 1234;

        // Shuffle prefabs
        var shuffledShapePrefabs = shapePrefabs.OrderBy( x => Random.value ).ToList( );
        var centerShape = shuffledShapePrefabs[0];
        var absolutePositions = new List<Vector3>() { centerShape.transform.position };

        var combinedShape = new GameObject();
        combinedShape.AddComponent<Tilemap>();
        combinedShape.AddComponent<TilemapCollider2D>();
        Merge(centerShape.GetComponent<Tilemap>(), into: combinedShape.GetComponent<Tilemap>());

        foreach (var currentShape in shuffledShapePrefabs.Skip(0))
        {
            currentShape.GetComponent<Tilemap>().CompressBounds();

            // Pick random side
            var randomSideIndex = Random.Range(0, 2);

            var combinedTilemap = combinedShape.GetComponent<Tilemap>();
            var currentShapeTilemap = combinedShape.GetComponent<Tilemap>();

            // Pick random coordinate perpendicular to side normal
            var perpendicularSideIndex = randomSideIndex == 0 ? 1 : 0;
            var currentShapeBounds = currentShapeTilemap.cellBounds;
            var combinedShapeBounds = combinedTilemap.cellBounds;
            
            var currentShapeExtend =
                currentShapeBounds.max[perpendicularSideIndex] - currentShapeBounds.min[perpendicularSideIndex];
            
            var minimalOverlap = 1;
            var lowestPossiblePerpendicularCoordinate =
                combinedShapeBounds.min[perpendicularSideIndex] - currentShapeExtend + minimalOverlap;
            var highestPossiblePerpendicularCoordinate =
                combinedShapeBounds.max[perpendicularSideIndex] - minimalOverlap;
            
            var randomPerpendicularCoordinate = Random.Range(
                lowestPossiblePerpendicularCoordinate,
                highestPossiblePerpendicularCoordinate
            );

            // Find closest coordinate parallel to side normal

            // Save coordinate

            // Add shape to configuration
        }
        // Compute relative positions between all 
    }

    void Merge(Tilemap sourceTilemap, Tilemap into)
    {
        var targetTilemap = into;

        foreach (var cellPosition in sourceTilemap.cellBounds.allPositionsWithin)
        {
            var currentTile = sourceTilemap.GetTile(cellPosition);
            if (currentTile == null)
            {
                continue;
            }

            targetTilemap.SetTile(cellPosition, currentTile);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}

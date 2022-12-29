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
        var grid = Instantiate(new GameObject());
        grid.AddComponent<Grid>();

        // Shuffle prefabs
        var shuffledShapePrefabs = shapePrefabs.OrderBy( x => Random.value ).ToList();
        var centerShape = shuffledShapePrefabs[0];
        var absolutePositions = new List<Vector3>() { centerShape.transform.position };

        var combinedShape = Instantiate(centerShape, parent: grid.transform);
        Instantiate(centerShape, grid.transform);

        foreach (var currentShape in shuffledShapePrefabs.Skip(3).Take(1))
        {
            var currentShapeClone = Instantiate(currentShape, parent: grid.transform);
            var currentShapeCollider = currentShapeClone.GetComponent<TilemapCollider2D>();
            var combinedShapeCollider = combinedShape.GetComponent<TilemapCollider2D>();
            var overlapping = true;
            var newPosition = currentShape.transform.position;
            while (overlapping)
            {
                var collisionInformation = Physics2D.Distance(
                    colliderA: currentShapeCollider,
                    colliderB: combinedShapeCollider
                );
                overlapping = collisionInformation.isOverlapped;
                Debug.Log(currentShape);
                Debug.Log(overlapping);
                if (!overlapping)
                {
                    break;
                }
                var positionWithoutOverlap = collisionInformation.normal * collisionInformation.distance;
                newPosition = new Vector3(
                    Mathf.Sign(positionWithoutOverlap.x) * Mathf.Ceil(Mathf.Abs(positionWithoutOverlap.x)),
                    Mathf.Sign(positionWithoutOverlap.y) * Mathf.Ceil(Mathf.Abs(positionWithoutOverlap.y)),
                    0
                );
                // newPosition = positionWithoutOverlap;
                Debug.Log(newPosition);
                Debug.DrawLine(collisionInformation.pointA, collisionInformation.pointB, Color.red, 60);
                // currentShapeClone.gameObject.SetActive(false);
                currentShapeClone.transform.position = newPosition;
                // currentShapeClone.gameObject.SetActive(true);
                break;
            }
            combinedShape.gameObject.SetActive(false);
            Merge(currentShapeClone.GetComponent<Tilemap>(), into: combinedShape.GetComponent<Tilemap>(), offset: new Vector2Int((int) newPosition.x, (int) newPosition.y));
            combinedShape.gameObject.SetActive(true);

            // Destroy(currentShapeClone.gameObject);
            // currentShape.GetComponent<Tilemap>().CompressBounds();

            // // Pick random side
            // var randomSideIndex = Random.Range(0, 2);

            // var combinedShapeTilemap = combinedShape.GetComponent<Tilemap>();
            // var currentShapeTilemap = combinedShape.GetComponent<Tilemap>();

            // // Pick random coordinate perpendicular to side normal
            // var perpendicularSideIndex = randomSideIndex == 0 ? 1 : 0;
            // var currentShapeBounds = currentShapeTilemap.cellBounds;
            // var combinedShapeBounds = combinedShapeTilemap.cellBounds;
            
            // var currentShapeExtend =
            //     currentShapeBounds.max[perpendicularSideIndex] - currentShapeBounds.min[perpendicularSideIndex];
            
            // var minimalOverlap = 1;
            // var lowestPossiblePerpendicularCoordinate = combinedShapeBounds.min[perpendicularSideIndex] + minimalOverlap;
            // var highestPossiblePerpendicularCoordinate =
            //     combinedShapeBounds.max[perpendicularSideIndex] + currentShapeExtend - minimalOverlap;
            
            // var randomPerpendicularCoordinate = Random.Range(
            //     lowestPossiblePerpendicularCoordinate,
            //     highestPossiblePerpendicularCoordinate
            // );
            // var displacement = new Vector3Int();
            // displacement[perpendicularSideIndex] = randomPerpendicularCoordinate;

            // // Find closest coordinate parallel to side normal
            // var minimalDistance = Mathf.Infinity;
            // foreach (var currentShapeCellPosition in currentShapeTilemap.cellBounds.allPositionsWithin)
            // {
            //     var currentShapeTile = currentShapeTilemap.GetTile(currentShapeCellPosition);
            //     if (currentShapeTile == null) continue;
            //     var currentShapeDisplacedCellPosition = currentShapeCellPosition + displacement;

            //     foreach (var combinedShapeCellPosition in combinedShapeTilemap.cellBounds.allPositionsWithin)
            //     {
            //         var currentCombinedShapeTile = combinedShapeTilemap.GetTile(combinedShapeCellPosition);
            //         if (currentCombinedShapeTile == null) continue;
            //         var cellsAreOnSameHeight = currentShapeDisplacedCellPosition[perpendicularSideIndex]
            //             == combinedShapeCellPosition[perpendicularSideIndex];
            //         if (cellsAreOnSameHeight)
            //         {
            //             var horizontalOrVerticalTileDistance = currentShapeDisplacedCellPosition[randomSideIndex]
            //                 - combinedShapeCellPosition[randomSideIndex];
            //             if (horizontalOrVerticalTileDistance < minimalDistance)
            //             {
            //                 minimalDistance = horizontalOrVerticalTileDistance;
            //             }
            //         }
            //     }
            // }
            // Debug.Log(minimalDistance);

            // Save coordinate

            // Add shape to configuration
        }
        // Destroy(combinedShape.gameObject);
        // Compute relative positions between all 
    }

    void Merge(Tilemap sourceTilemap, Tilemap into, Vector2Int offset)
    {
        var targetTilemap = into;

        foreach (var cellPosition in sourceTilemap.cellBounds.allPositionsWithin)
        {
            var currentTile = sourceTilemap.GetTile(cellPosition);
            var currentTargetTile = targetTilemap.GetTile(cellPosition + (Vector3Int) offset);
            if (currentTile == null || currentTargetTile != null)
            {
                continue;
            }

            targetTilemap.SetTile(cellPosition + (Vector3Int) offset, currentTile);
        }
    }

    protected override void Start()
    {
        base.Start();
        GenerateConfiguration();
    }

    protected override void Update()
    {
        base.Update();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainBuilder : MonoBehaviour
{
    public Tile wallSquare;

    public Tilemap overworldTilemap;

    public void DrawTilemap(Terrain compressedTerrain)
    {
        // This array has a 0 when there should be a wall and other integers that indicate empty space and the numbers says in what iteration that empty space was generated
        int[,] mapArray = new int[compressedTerrain.mapHeight, compressedTerrain.mapWidth];
        foreach (Vector3Int position in compressedTerrain.emptySpaceList)
        {
            mapArray[position.x, position.y] = position.z;
        }

        Vector3Int drawingPosition = new Vector3Int();
        for (int k = 0; k < mapArray.GetLength(0); k++)
        {
            for (int m = 0; m < mapArray.GetLength(1); m++)
            {
                if (mapArray[k, m] == 0)
                {
                    drawingPosition.x = k;
                    drawingPosition.y = m;
                    overworldTilemap.SetTile(drawingPosition, wallSquare);
                }
            }
        }
    }

    public void ClearTilemap()
    {
        overworldTilemap.ClearAllTiles();
    }
}

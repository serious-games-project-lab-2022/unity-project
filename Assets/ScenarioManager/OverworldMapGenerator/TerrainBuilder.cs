using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainBuilder : MonoBehaviour
{
    public Tile wallSquare;

    public Tilemap overworldTilemap;
 
    //TODO How can we make sure the Terrain Object is already given to this class before calling this call?
    public void DrawTilemap(Terrain compressedTerrain)    
    {
        // This array has a 0 when there should be a wall and other integers that indicate empty space and the numbers says in what iteration that empty space was generated
        int[,] mapArray = new int [compressedTerrain.MapHeight, compressedTerrain.MapWidth];
        foreach(Vector3Int position in compressedTerrain.EmptySpaceList)
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
}

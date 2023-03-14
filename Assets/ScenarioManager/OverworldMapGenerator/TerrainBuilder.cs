using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainBuilder : MonoBehaviour
{
    public Tile wallSquare;

    
    public Tile wallLeftSquare;
    public Tile wallRightSquare;
    public Tile wallUpSquare;
    public Tile wallDownSquare;
    public Tile wallDUpLeftSquare;
    public Tile wallDUpRightSquare;
    public Tile wallDDownLeftSquare;
    public Tile wallDDownRightSquare;
    public Tile wallEUpLeftSquare;
    public Tile wallEUpRightSquare;
    public Tile wallEDownLeftSquare;
    public Tile wallEDownRightSquare;

    [SerializeField] private Transform worldOrigin;

    [SerializeField] private SpaceCoin spaceCoinPrefab;



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
                drawingPosition.x = k;
                drawingPosition.y = m;
                if (mapArray[k, m] == 0)
                {
                   
                    overworldTilemap.SetTile(drawingPosition, wallSquare);
                    continue;
                }
                if (mapArray[k, m] == 2)
                {
                   
                    //spawn coin with a chance
                    if (worldOrigin != null)
                    {
                        int j = Random.Range(0, 30);
                        if (j<1)
                        {
                            var currentCoin = Instantiate(
                                spaceCoinPrefab,
                                parent: worldOrigin.transform
                            );
                            currentCoin.transform.localPosition = drawingPosition;
                            continue;
                        }
                    }
                }
                if (mapArray[k, m] == 5)
                {
                    if (mapArray[k+1, m] == 0)
                    {
                        if (mapArray[k, m+1] == 0)
                        {
                            overworldTilemap.SetTile(drawingPosition, wallEDownLeftSquare);
                            continue;
                        }else
                        {
                            if (mapArray[k, m-1] != 0)
                            {
                                overworldTilemap.SetTile(drawingPosition, wallLeftSquare);
                                continue;
                            }
                        }
                    }
                    if (mapArray[k, m+1] == 0)
                    {
                        if (mapArray[k-1, m] == 0)
                        {
                            overworldTilemap.SetTile(drawingPosition, wallEDownRightSquare);
                            continue;
                        }else
                        {
                            overworldTilemap.SetTile(drawingPosition, wallDownSquare);
                            continue;
                        }
                    }
                    if (mapArray[k-1, m] == 0)
                    {
                        if (mapArray[k, m-1] == 0)
                        {
                            overworldTilemap.SetTile(drawingPosition, wallEUpRightSquare);
                            continue;
                        }else
                        {
                            overworldTilemap.SetTile(drawingPosition, wallRightSquare);
                            continue;
                        }
                    }
                    if (mapArray[k, m-1] == 0)
                    {
                        if (mapArray[k+1, m] == 0)
                        {
                            overworldTilemap.SetTile(drawingPosition, wallEUpLeftSquare);
                            continue;
                        }else
                        {
                            overworldTilemap.SetTile(drawingPosition, wallUpSquare);
                            continue;
                        }
                    }
                    if (mapArray[k+1, m+1] == 0)
                    {
                        overworldTilemap.SetTile(drawingPosition, wallDDownLeftSquare);    
                        continue;                    
                    }
                    if (mapArray[k-1, m+1] == 0)
                    {
                        overworldTilemap.SetTile(drawingPosition, wallDDownRightSquare);     
                        continue;                   
                    }
                    if (mapArray[k-1, m-1] == 0)
                    {
                        overworldTilemap.SetTile(drawingPosition, wallDUpRightSquare); 
                        continue;                       
                    }
                    if (mapArray[k+1, m-1] == 0)
                    {
                        overworldTilemap.SetTile(drawingPosition, wallDUpLeftSquare); 
                        continue;                       
                    }




                   
                }
            }
        }
    }

    public void ClearTilemap()
    {
        overworldTilemap.ClearAllTiles();
    }
}

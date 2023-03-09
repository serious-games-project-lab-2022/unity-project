using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainBuilder : MonoBehaviour
{
    public Tile redSquare;

    public Tilemap overworldTilemap;

    public Terrain compressedTerrainList;

    // This array has a 0 when there should be a wall and other integers that indicate empty space and in what iteration that empty space was generated
    private int[,] mapArray = new int [200,200];
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // expandCompressionToFullArray takes the information sent via the network and expands it to a full array with the original information
    // This way
    void ExpandCompressionToFullArray()
    {
        foreach(Vector3Int pos in compressedTerrainList.EmptySpaceList)
        {
            mapArray[pos.x,pos.y]=pos.z;
        }
    }
    void DrawTilemap()    
    {
    Vector3Int drawingPosition = new Vector3Int();
        for (int k = 0; k < mapArray.GetLength(0); k++)
        {
            for (int m = 0; m < mapArray.GetLength(1); m++)
            {
                if(mapArray[k,m]==0)
                {
                    drawingPosition.x = k;
                    drawingPosition.y = m;
                    overworldTilemap.SetTile(drawingPosition,redSquare);
                }
            }
        }
        
    }
}

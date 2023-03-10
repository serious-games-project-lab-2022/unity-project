using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain
{
     private List<Vector3Int> emptySpaceList =  new List<Vector3Int>();

     private List<Vector3Int> checkpointList =  new List<Vector3Int>();

     private int mapHeight;
     private int mapWidth;

     public Terrain(List<Vector3Int> emptySpaceList,List<Vector3Int> checkpointList, int mapHeight,int mapWidth)
     {
        this.emptySpaceList=emptySpaceList;
        this.checkpointList=checkpointList;
        this.MapHeight=mapHeight;
        this.MapWidth=mapWidth;
     }

    public List<Vector3Int> EmptySpaceList { get => emptySpaceList; set => emptySpaceList = value; }
    public List<Vector3Int> CheckpointList { get => checkpointList; set => checkpointList = value; }
    public int MapHeight { get => mapHeight; set => mapHeight = value; }
    public int MapWidth { get => mapWidth; set => mapWidth = value; }
}

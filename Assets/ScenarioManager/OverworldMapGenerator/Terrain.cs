using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
     private List<Vector3Int> emptySpaceList =  new List<Vector3Int>();

     private List<Vector3Int> checkpointList =  new List<Vector3Int>();

     public Terrain(List<Vector3Int> emptySpaceList,List<Vector3Int> checkpointList)
     {
        this.EmptySpaceList=emptySpaceList;
        this.CheckpointList=checkpointList;
     }

    public List<Vector3Int> EmptySpaceList { get => emptySpaceList; set => emptySpaceList = value; }
    public List<Vector3Int> CheckpointList { get => checkpointList; set => checkpointList = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

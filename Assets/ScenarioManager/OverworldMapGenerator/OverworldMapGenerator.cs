using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMapGenerator : MonoBehaviour
{
    private static int mapWidth = 20;
    private static int mapHeight = 20;
    private int[,] mapArray = new int [mapHeight,mapWidth];

    private List<Vector3Int> checkpointList =  new List<Vector3Int>();

    private List<(Vector3Int,Vector3Int)> edgesBetweenCheckpoints = new List<(Vector3Int,Vector3Int)>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // GenerateOverworldMap is called to create a list of checkpoints and a list of tileslocations that are the overworldmap
    void GenerateOverworldMap(int numberOfCheckpoints)
    {
        //Change this parameter to spawn checkpoints closer to the edges of the map or further in
        int outerWallThickness = 3;
        checkpointList.Clear();
        edgesBetweenCheckpoints.Clear();
        
        for(int i = 0; i < numberOfCheckpoints; i++){
           checkpointList.Add(new Vector3Int(Random.Range(outerWallThickness+1,mapWidth-outerWallThickness),Random.Range(Random.Range(outerWallThickness+1,mapHeight-outerWallThickness),0))); 
        }
        //TODO remove debug.log
         Debug.Log(checkpointList);

         /*


         List<Vector3> nearestCheckpointList =  new List<Vector3>();
         nearestCheckpointList.Clear();

        
        //Failed Venture: just computing the closest neighbor doesn't give you a nice graph. at some point the edges has to be between further away points
        
        foreach (Vector3Int checkpoint in checkpointList)
        {
            nearestCheckpointList.Add(new Vector3(0.0f,0.0f,Mathf.Sqrt(Math.Pow(mapWidth)+Math.Pow(mapHeight))));
            foreach (Vector3Int checkpointneighbor in checkpointList)
            {
                
                if(checkpoint==checkpointneighbor)
                {
                    continue;
                }else
                {
                    float distanceToPoint = Vector3.Distance(checkpoint,checkpointneighbor);
                    if(distanceToPoint<nearestCheckpointList[^1].z)
                    {
                        nearestCheckpointList[^1]= new Vector3(checkpointneighbor.x,checkpointneighbor.y,distanceToPoint);


                    }
                }
            }

        */
        float[,] distances = new float[checkpointList.Count,checkpointList.Count];
        // Iterate through list of checkpoints
        for (int i = 0; i < checkpointList.Count; i++)
        {
            Vector3Int checkpoint1 = checkpointList[i];

            // Iterate through remaining checkpoints
            for (int j = i + 1; j < checkpointList.Count; j++)
            {
                Vector3Int checkpoint2 = checkpointList[j];
                
                // Calculate distance
                float distance = Vector3.Distance(checkpoint1,checkpoint2);
                
                // Save distance in 2D array
                distances[i, j] = distance;
                distances[j, i] = distance;
            }
        }

        // This HashSet contains all checkpoints that are connected via the edges

        HashSet<int> checkpointsAdded = new HashSet<int>();
        
        //Find minimal distance and add minimal distance edge to the list. The number of edges is at least one less than the number of checkpoints
        
        //Find first edge
        float minDistance = float.MaxValue;
        int point1 = 0;
        int point2 = 0;

        for (int i = 0; i < distances.GetLength(0); i++)
        {
            for (int j = 0; j < distances.GetLength(1); j++)
            {
                if (distances[i, j] < minDistance)
                    minDistance = distances[i, j];
                    point1 = i;
                    point2 = j;
            }
        }
        // Adds first edge to the edge list

        edgesBetweenCheckpoints.Add((checkpointList[point1],checkpointList[point2]));

        // Removes the edges from further considerations

        distances[point2,point1]=float.MaxValue;
        distances[point1,point2]=float.MaxValue;

        // Adds the 2 points to the connected grid list, which keeps track of what checkpoints are connected

        checkpointsAdded.Add(point1);
        checkpointsAdded.Add(point2);



        //Find and add all other edges
    
        for (int k = 0; k < checkpointList.Count-2; k++)
        {
            minDistance = float.MaxValue;
            point1 = 0;
            point2 = 0;
            
            // Find minimal distance of edges that connected to the already interconnected checkpoints

            foreach (int i in checkpointsAdded)
            {
                for (int j = 0; j < distances.GetLength(1); j++)
                {
                    if (distances[i, j] < minDistance)
                        minDistance = distances[i, j];
                        point1 = i;
                        point2 = j;
                }
            }


            //

            for (int i = 0; i < distances.GetLength(0); i++)
            {
                for (int j = 0; j < distances.GetLength(1); j++)
                {
                    if (distances[i, j] < minDistance)
                        minDistance = distances[i, j];
                        point1 = i;
                        point2 = j;
                }
            }
            


            // add the edge to the list
            //TODO Make sure this part works!

            edgesBetweenCheckpoints.Add((checkpointList[point1],checkpointList[point2]));

            // set distance to infinite to edges(distances) that can't be considered any further
            
            if(!checkpointsAdded.Contains(point1))
            {            
                foreach (int i in checkpointsAdded)
                {
                    distances[i,point1]=float.MaxValue;
                    distances[point1,1]=float.MaxValue;
                }
            }
            if(!checkpointsAdded.Contains(point2))
            {            
                foreach (int i in checkpointsAdded)
                {
                    distances[i,point2]=float.MaxValue;
                    distances[point2,1]=float.MaxValue;
                }
            }
                      
            // add new point to the checkpoint graph of connected checkpoints

            checkpointsAdded.Add(point1);
            checkpointsAdded.Add(point2);



        }

        //Lists of Checkpoints and Edges are complete
        //TODO Make circle around checkpoints and make connecting path for edges (compute  x and y distance and draw a path where it is longer)



    }
}

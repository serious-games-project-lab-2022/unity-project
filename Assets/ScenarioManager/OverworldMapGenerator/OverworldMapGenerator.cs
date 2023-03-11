using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverworldMapGenerator
{
    private static int mapWidth = 200;
    private static int mapHeight = 200;

    // Change this parameter to spawn checkpoints closer to the edges of the map or further in. Minimal number should be 2.
    private static int outerWallThickness = 7;

    // This array has a 0 when there should be a wall and other integers that indicate empty space and in what iteration that empty space was generated
    private int[,] mapArray = new int[mapHeight, mapWidth];

    // A compressed list with vectors that point to empty space
    private List<Vector3Int> emptySpaceList = new List<Vector3Int>();

    // A list of possible positions that a checkpoint or the spaceship could spawn at
    private List<Vector3Int> checkpointList = new List<Vector3Int>();

    private List<(Vector3Int, Vector3Int)> edgesBetweenCheckpoints = new List<(Vector3Int, Vector3Int)>();

    // GenerateCheckpointsAndEdges is called to create a list of checkpoints and a list of edges between those checkpoints
    void GenerateCheckpointsAndEdges(int numberOfCheckpoints)
    {
        //TODO remove debug.log
        Debug.Log("Limits");
        Debug.Log(outerWallThickness + 1);
        Debug.Log(mapWidth - outerWallThickness);
        Debug.Log(mapHeight - outerWallThickness);

        checkpointList.Clear();
        edgesBetweenCheckpoints.Clear();

        for (int i = 0; i < numberOfCheckpoints; i++)
        {
            checkpointList.Add(
                new Vector3Int(
                    Random.Range(outerWallThickness + 1, mapWidth - outerWallThickness),
                    Random.Range(outerWallThickness + 1, mapHeight - outerWallThickness),
                    0
                )
            );
        }
        //remove duplicate checkpoints
        checkpointList = new List<Vector3Int>(new HashSet<Vector3Int>(checkpointList));

        //TODO remove debug.log
        Debug.Log("Checkpoints:");
        foreach (Vector3Int v in checkpointList)
        {
            Debug.Log(v);
        }

        float[,] distances = new float[checkpointList.Count, checkpointList.Count];
        // Iterate through list of checkpoints
        for (int i = 0; i < checkpointList.Count; i++)
        {
            Vector3Int checkpoint1 = checkpointList[i];

            // Iterate through remaining checkpoints
            for (int j = i + 1; j < checkpointList.Count; j++)
            {
                Vector3Int checkpoint2 = checkpointList[j];

                // Calculate distance
                float distance = Vector3.Distance(checkpoint1, checkpoint2);

                // Save distance in 2D array
                distances[i, j] = distance;
                distances[j, i] = distance;
            }
        }

        // Set diagonal of distance to max value (Can't have a edge between a point and itself)
        for (int i = 0; i < checkpointList.Count; i++)
        {
            distances[i, i] = float.MaxValue;
        }

        //TODO remove debug.log
        /* Debug.Log("Distance Table:");
          foreach (float f in distances)
          {
             Debug.Log(f);
          }

         */


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
                {
                    minDistance = distances[i, j];
                    point1 = i;
                    point2 = j;
                }
            }
        }
        // Adds first edge to the edge list
        Debug.Log("Distance of first Edge:" + minDistance);
        Debug.Log("point1:" + point1);
        Debug.Log("point2:" + point2);

        edgesBetweenCheckpoints.Add((checkpointList[point1], checkpointList[point2]));

        // Removes the edges from further considerations

        distances[point2, point1] = float.MaxValue;
        distances[point1, point2] = float.MaxValue;

        // Adds the 2 points to the connected grid list, which keeps track of what checkpoints are connected

        checkpointsAdded.Add(point1);
        checkpointsAdded.Add(point2);

        //Find and add all other edges

        for (int k = 0; k < checkpointList.Count - 2; k++)
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
                    {
                        minDistance = distances[i, j];
                        point1 = i;
                        point2 = j;
                    }
                }
            }

            // add the edge to the list
            //TODO Make sure this part works!

            edgesBetweenCheckpoints.Add((checkpointList[point1], checkpointList[point2]));

            // set distance to infinite to edges(distances) that can't be considered any further

            if (!checkpointsAdded.Contains(point1))
            {
                foreach (int i in checkpointsAdded)
                {
                    distances[i, point1] = float.MaxValue;
                    distances[point1, i] = float.MaxValue;
                }
            }
            if (!checkpointsAdded.Contains(point2))
            {
                foreach (int i in checkpointsAdded)
                {
                    distances[i, point2] = float.MaxValue;
                    distances[point2, i] = float.MaxValue;
                }
            }

            // add new point to the checkpoint graph of connected checkpoints

            checkpointsAdded.Add(point1);
            checkpointsAdded.Add(point2);
        }

        //TODO remove debug.log

        Debug.Log("---End of Algo:---");
        Debug.Log("Distance Table:");
        foreach (float f in distances)
        {
            Debug.Log(f);
        }
        Debug.Log("Checkpoints added:");
        foreach (int i in checkpointsAdded)
        {
            Debug.Log(i);
        }
        Debug.Log("Edges:");
        foreach ((Vector3Int, Vector3Int) e in edgesBetweenCheckpoints)
        {
            Debug.Log(e);
        }
    }

    //GenerateOverworldMap is called to create a list of tileslocations that are the overworldmap from the list of checkpoint and edges
    void GenerateArrayMap()
    {
        //Lists of Checkpoints and Edges are complete
        //TODO Make circle around checkpoints and make connecting path for edges (compute  x and y distance and draw a path where it is longer)

        foreach (Vector3Int c in checkpointList)
        {
            //Mark checkpoint
            mapArray[c.x, c.y] = 1;
            //Mark surrounding tiles
            mapArray[c.x - 1, c.y - 1] = 1;
            mapArray[c.x - 1, c.y] = 1;
            mapArray[c.x - 1, c.y + 1] = 1;
            mapArray[c.x, c.y - 1] = 1;
            mapArray[c.x, c.y + 1] = 1;
            mapArray[c.x + 1, c.y - 1] = 1;
            mapArray[c.x + 1, c.y] = 1;
            mapArray[c.x + 1, c.y + 1] = 1;
        }

        //Mark edges between checkpoints
        foreach ((Vector3Int, Vector3Int) e in edgesBetweenCheckpoints)
        {
            Vector3Int currentDrillLocation = e.Item1;
            Vector3Int targetDrillLocation = e.Item2;
            int xDistance = currentDrillLocation.x - targetDrillLocation.x;
            int yDistance = currentDrillLocation.y - targetDrillLocation.y;

            //drill the path alongside a edge
            while (xDistance != 0 || yDistance != 0)
            {
                mapArray[targetDrillLocation.x + xDistance, targetDrillLocation.y + yDistance] = 1;

                if (Mathf.Abs(xDistance) > Mathf.Abs(yDistance))
                {
                    xDistance -= (int)Mathf.Sign(xDistance);
                }
                else
                {
                    yDistance -= (int)Mathf.Sign(yDistance);
                }
            }
        }

        //TODO Remove Debug Log
        Debug.Log(edgesBetweenCheckpoints[0].Item1);
        Debug.Log(edgesBetweenCheckpoints[0].Item2);

        //TODO Remove Debug Log
        Debug.Log(mapArray[0, 0]);
        Debug.Log(mapArray[1, 1]);
        Debug.Log(mapArray[2, 2]);
        Debug.Log(mapArray[3, 3]);
        Debug.Log(mapArray[4, 4]);
        Debug.Log(mapArray[5, 5]);
        Debug.Log(mapArray[6, 6]);
        Debug.Log(mapArray[7, 7]);
        Debug.Log(mapArray[8, 8]);
        Debug.Log(mapArray[9, 9]);
    }

    //widenPaths makes the playable area larger by moving the walls back. It takes the an integer, which will be looked for in the mapArray and only those "walls" will be widened.
    //The wideningInteger should be 1 in the first call of this method, since the generateArrayMap method places a 1 at the location of checkpoints and paths between checkpoints.
    //The outerWall of the Map won't be narrowed down by more than 1 regardless of the iteration.
    //This only removes walls (zeros) from the array. It does not overwrite existing paths.
    void WidenPaths(int wideningInteger)
    {
        for (int k = 1; k < mapArray.GetLength(0) - 1; k++)
        {
            for (int m = 1; m < mapArray.GetLength(1) - 1; m++)
            {
                if (mapArray[k, m] == wideningInteger)
                {
                    //Logic to widen walls here
                    if (mapArray[k - 1, m - 1] == 0)
                    {
                        mapArray[k - 1, m - 1] = wideningInteger + 1;
                    }
                    if (mapArray[k - 1, m] == 0)
                    {
                        mapArray[k - 1, m] = wideningInteger + 1;
                    }
                    if (mapArray[k - 1, m + 1] == 0)
                    {
                        mapArray[k - 1, m + 1] = wideningInteger + 1;
                    }
                    if (mapArray[k, m - 1] == 0)
                    {
                        mapArray[k, m - 1] = wideningInteger + 1;
                    }
                    if (mapArray[k, m + 1] == 0)
                    {
                        mapArray[k, m + 1] = wideningInteger + 1;
                    }
                    if (mapArray[k + 1, m - 1] == 0)
                    {
                        mapArray[k + 1, m - 1] = wideningInteger + 1;
                    }
                    if (mapArray[k + 1, m] == 0)
                    {
                        mapArray[k + 1, m] = wideningInteger + 1;
                    }
                    if (mapArray[k + 1, m + 1] == 0)
                    {
                        mapArray[k + 1, m + 1] = wideningInteger + 1;
                    }
                }
            }
        }
    }

    void ComputeCompressedList()
    {
        Vector3Int drawingPosition = new Vector3Int();
        for (int k = 0; k < mapArray.GetLength(0); k++)
        {
            for (int m = 0; m < mapArray.GetLength(1); m++)
            {
                if (mapArray[k, m] != 0)
                {
                    drawingPosition.x = k;
                    drawingPosition.y = m;
                    //abusing the z koordinate for additional information
                    emptySpaceList.Add(new Vector3Int(k, m, mapArray[k, m]));
                    //overworldTilemap.SetTile(drawingPosition,redSquare);
                }
            }
        }
        // for (int index = 0; index < checkpointList.Count; index++)
        // {
        //     checkpointList[index] = checkpointList[index] - new Vector3Int(mapWidth, mapHeight, 0);
        // }
    }

    // GenerateTerrain() generates a instance of the Terrain class
    public Terrain GenerateTerrain()
    {
        this.GenerateCheckpointsAndEdges(50);
        // Empty the area around the initial space ship spawn position
        mapArray[checkpointList[0].x, checkpointList[0].y] = 42;
        this.WidenPaths(42);
        this.WidenPaths(43);
        this.WidenPaths(44);
        this.WidenPaths(45);
        this.WidenPaths(46);
        this.WidenPaths(47);
        this.WidenPaths(48);

        this.GenerateArrayMap();
        this.WidenPaths(1);
        this.WidenPaths(2);
        this.WidenPaths(3);
        this.ComputeCompressedList();
        return new Terrain(emptySpaceList, checkpointList, mapHeight, mapWidth);
    }
}

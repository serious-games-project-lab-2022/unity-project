using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCheckpointSpawnPositions : MonoBehaviour
{
    public GameObject myPrefab;
    public GameObject spaceship;
    //public GameObject OverworldGoal;
    Transform spaceshipPos;
    Transform myRandom;
    int CheckpointsNumber = 2;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {   
        this.SpawnCheckpoint();
        //spawn new checkpoint once last one was reached
        //MinigoalCheckpoint.OnCheckpointReached += SpawnCheckpoint;
        //MinigoalCheckpoint.OnMinigoalCollidedWithTerrain += SpawnCheckpoint;
    }

    public void SpawnCheckpoint()
    {
           
        if(count <= CheckpointsNumber)
        {
            spaceshipPos = spaceship.transform;
            Debug.Log(spaceshipPos.position);
            //spaceship as center of the radius 6, spwan one checkpoint randomly
            Vector2 randomPos = new Vector2(2 + count+spaceshipPos.position.x, 2 + count + spaceshipPos.position.y);
            Debug.Log(randomPos);
            GameObject.Instantiate(myPrefab, randomPos, Quaternion.identity);
            count++;

            var sharedGameState = GameManager.Singleton.sharedGameState;
       
            if (sharedGameState != null)
            {
                sharedGameState.checkpointPosition.Value = randomPos;
                Debug.Log("randomPos"+randomPos);
                 Debug.Log("sharedGameState"+sharedGameState.checkpointPosition.Value);
            }
        } //else
        //SpawnOverworldGoal();
            
        
    }
    /*
    public void SpawnOverworldGoal()
    {
        Debug.Log("SpawnOverworldGoal");
        GameObject.Instantiate(OverworldGoal,new Vector3(-31.1f,126.9f,0),Quaternion.identity);
    }
    */
}
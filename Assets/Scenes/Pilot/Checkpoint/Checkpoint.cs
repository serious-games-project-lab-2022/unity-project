using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //public delegate void CheckpointReached();
    //public event CheckpointReached OnCheckpointReached;

    void Start()
    {
        var sharedGameState = GameManager.Singleton.sharedGameState;
       
        if (sharedGameState != null)
        {
            sharedGameState.checkpointPosition.Value = new Vector2(transform.localPosition.x, transform.localPosition.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        var minigameHandler = GameObject.FindObjectOfType<MinigameHandler>();
        print(minigameHandler == null);

        if(other.tag == "OverworldSpaceship"){
            minigameHandler.SpawnMinigame();
            
            Destroy(gameObject);
        }
    }
}
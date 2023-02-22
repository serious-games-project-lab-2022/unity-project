using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //public delegate void CheckpointReached();
    //public event CheckpointReached OnCheckpointReached;

    void Start()
    {
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
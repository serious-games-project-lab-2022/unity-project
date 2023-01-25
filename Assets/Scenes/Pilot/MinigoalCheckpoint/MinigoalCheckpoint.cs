using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigoalCheckpoint : MonoBehaviour
{
    public delegate void CheckpointReached();
    public static event CheckpointReached OnCheckpointReached;
    private SharedGameState sharedGameState;

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.tag == "OverworldSpaceship"){
            OnCheckpointReached();
            Destroy(gameObject);
        }
    }
}

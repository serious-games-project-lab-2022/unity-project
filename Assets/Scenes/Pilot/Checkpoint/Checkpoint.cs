using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : OverworldCollectible
{
    public delegate void CheckpointReached();
    public event CheckpointReached OnCheckpointReached;

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
        if(other.tag == "OverworldSpaceship"){
            OnCheckpointReached();
            HideForInstructor();
            Destroy(gameObject);
        }
    }

    private void HideForInstructor()
    {
        var sharedGameState = GameManager.Singleton.sharedGameState;
        sharedGameState.checkpointPosition.Value = new Vector2(1_000_000, 1_000_000);
    }
}
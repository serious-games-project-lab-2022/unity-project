using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldGoal : OverworldCollectible
{
    public delegate void CollidedWithSpaceship();
    public event CollidedWithSpaceship OnCollidedWithSpaceship;

    void Start()
    {
        var sharedGameState = GameManager.Singleton.sharedGameState;
       
        if (sharedGameState != null)
        {
            sharedGameState.overworldGoalPosition.Value = new Vector2(transform.localPosition.x, transform.localPosition.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "OverworldSpaceship"){
            OnCollidedWithSpaceship();
            Destroy(gameObject);
        }
    }
}

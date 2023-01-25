using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldGoal : MonoBehaviour
{
    public delegate void CollidedWithSpaceship();
    public static event CollidedWithSpaceship OnCollidedWithSpaceship;

    private SharedGameState sharedGameState;

    void Start()
    {
        sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
       
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

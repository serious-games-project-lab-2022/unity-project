using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    private SharedGameState sharedGameState;

    void Start()
    {
        sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
    }

    void Update()
    {
        if (sharedGameState != null)
        {
            sharedGameState.spaceshipPosition.Value = new Vector2(transform.position.x, transform.position.y);
        }
    }
}

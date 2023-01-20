using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    public delegate void CollidedWithTerrain();
    public static event CollidedWithTerrain OnCollidedWithTerrain = delegate {};


    void Update()
    {
        var sharedGameState = GameManager.Singleton.sharedGameState;
        if (sharedGameState != null)
        {
            sharedGameState.spaceshipPosition.Value = new Vector2(transform.localPosition.x, transform.localPosition.y);
            sharedGameState.spaceshipRotation.Value = transform.eulerAngles.z;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "OverworldTerrain")
        {
            OnCollidedWithTerrain();
        }
    }
}

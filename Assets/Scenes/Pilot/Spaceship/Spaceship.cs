using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    //Space Ship Effects
    private CameraShaker shaker;
    public AudioSource collisionSound;
  


    private SharedGameState sharedGameState;
    
    public delegate void CollidedWithTerrain();
    public static event CollidedWithTerrain OnCollidedWithTerrain = delegate {};

    void Start()
    {
        sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
        shaker = GameObject.Find("FollowSpaceShipCam").GetComponent<CameraShaker>();
    }

    void Update()
    {
        
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
            collisionSound.Play();
            OnCollidedWithTerrain();
            shaker.shakePilot();
           
        }
    }

    
}

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
    public AudioSource spaceShipMotorSound1;
    public AudioSource spaceShipMotorSound2;

    private bool playMotor1 = true;
    //


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
        if(playMotor1)
        {
            if(!spaceShipMotorSound2.isPlaying)
            {
                print("motor 1");
                if(!spaceShipMotorSound1.isPlaying)
                {
                    spaceShipMotorSound1.Play();
                }
                
                playMotor1 = false;
            }
           
        }
        else
        {
            if(!spaceShipMotorSound1.isPlaying)
            {
                print("motor 2");
                spaceShipMotorSound2.Play();
                playMotor1 = true;
            }
            
        }
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

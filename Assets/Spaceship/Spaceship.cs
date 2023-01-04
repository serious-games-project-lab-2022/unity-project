using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    private SharedGameState sharedGameState;
    private float rotation = 0;

    void Start()
    {
        sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
    }

    void Update()
    {
        if(transform.eulerAngles.z>180)
        {
           
           rotation = transform.eulerAngles.z - 360f;

        }
        else
        {
            
            rotation = transform.eulerAngles.z;
        }
        

       
        if (sharedGameState != null)
        {
            sharedGameState.spaceshipPosition.Value = new Vector3(transform.position.x, transform.position.y,rotation);
          
        }
    }
}

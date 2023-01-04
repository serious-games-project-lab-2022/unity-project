using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class FollowSpaceShip : MonoBehaviour
{
    private SharedGameState sharedGameState;
    private float rotation = 0;

    void Start()
    {
        SharedGameState.OnInstructorReceivedGameState += () => {
            sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
        };
       
    }


    // Update is called once per frame
    [System.Obsolete]
    void FixedUpdate()
    {
        
        if (sharedGameState != null)
        {
            transform.position = new Vector3(sharedGameState.spaceshipPosition.Value.x, sharedGameState.spaceshipPosition.Value.y, 0);

            if(rotation != sharedGameState.spaceshipPosition.Value.z)
            {
                //character.freezeRotation = false;
                rotation = sharedGameState.spaceshipPosition.Value.z;
                //character.rotation = rotation;
                //transform.Rotate(new Vector3(0, 0, rotation) * Time.fixedDeltaTime * 300f);

                //transform.rotation.z = rotation;
                transform.eulerAngles = new Vector3(0, 0, rotation);
            }

            Debug.Log("rotation of instructor " + rotation + " , rotation of pilot " + sharedGameState.spaceshipPosition.Value.z);
           
            
        }
         
      
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowSpaceShip : MonoBehaviour
{
    private SharedGameState sharedGameState;
    private Rigidbody2D character;

    void Start()
    {
        SharedGameState.OnInstructorReceivedGameState += () => {
            sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
        };

        character = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(sharedGameState.spaceshipPosition.Value.x, sharedGameState.spaceshipPosition.Value.y);
        transform.rotation = new Quaternion(0, 0, sharedGameState.spaceshipPosition.Value.z, 0);
    }


    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(sharedGameState != null)
        {
            transform.position = new Vector2(sharedGameState.spaceshipPosition.Value.x, sharedGameState.spaceshipPosition.Value.y);
            //transform(new Vector2(sharedGameState.spaceshipPosition.Value.x, sharedGameState.spaceshipPosition.Value.y) * Time.fixedDeltaTime * 300f);
            //transform.rotation.SetEulerRotation(new Vector3(0, 0, sharedGameState.spaceshipPosition.Value.z)); 
            transform.rotation = new Quaternion(0, 0, sharedGameState.spaceshipPosition.Value.z, 0);
            /*if (transform.rotation.z != sharedGameState.spaceshipPosition.Value.z)
            {
                character.MoveRotation(character.rotation - sharedGameState.spaceshipPosition.Value.z * Time.fixedDeltaTime * 300f);
            }*/
        }
        
        
       
      
    }
}

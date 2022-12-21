using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldGoal : MonoBehaviour
{
    private bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "OverworldSpaceship"){
            var characterController = other.GetComponent<CharacterController>();
            characterController.achievedGoal(active);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldGoal : MonoBehaviour
{
    public delegate void CollidedWithSpaceship();
    public static event CollidedWithSpaceship OnCollidedWithSpaceship;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "OverworldSpaceship"){
            OnCollidedWithSpaceship();
        }
    }
}

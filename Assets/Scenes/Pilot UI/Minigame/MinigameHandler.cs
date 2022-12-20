using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameHandler : MonoBehaviour
{
    [SerializeField ]bool turnOn;
    
    void Update()
    {
        
        if((int)Time.realtimeSinceStartup == 20)
        {
            tornOnTheCamera();
        }
    }


    void tornOnTheCamera()
    {
        Camera c = GetComponent<Camera>();
        c.enabled = true;
    }
}

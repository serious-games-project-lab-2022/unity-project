using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour

{

    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angle += Time.fixedDeltaTime;
        transform.Rotate(0,0,angle*0.015f); 
    }
}

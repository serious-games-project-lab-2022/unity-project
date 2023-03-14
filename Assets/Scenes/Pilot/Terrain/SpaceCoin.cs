using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCoin : MonoBehaviour
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
        transform.Rotate(0,angle*0.05f,0); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.tag == "OverworldSpaceship"){
            GameObject.FindObjectOfType<PilotManager>().score += 1f;
            Destroy(gameObject);
        }
    }
}

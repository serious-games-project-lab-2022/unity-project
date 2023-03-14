using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
    
    public GameObject objectToFollow;
    
    public float speed = 2.0f;
    Vector2 offset;

    void Start()
    {
        offset = this.transform.position - objectToFollow.transform.position;
    }
    
    void Update () {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        // position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
        // position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);
        position.y = offset.y + objectToFollow.transform.position.y;
        position.x = offset.x + objectToFollow.transform.position.x;

        this.transform.position = position;



    }
}
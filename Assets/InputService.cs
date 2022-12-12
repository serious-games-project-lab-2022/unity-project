using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : MonoBehaviour
{
    public float turnDirection = 0f;

    void Update()
    {
        turnDirection = Input.GetAxis("Horizontal");
    }
}

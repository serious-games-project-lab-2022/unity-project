using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService : MonoBehaviour
{
    [HideInInspector]
    public float turnDirection = 0f;

    [SerializeField]
    private SteeringWheel steeringWheel;

    void Update()
    {
        if (steeringWheel != null)
        {
            turnDirection = steeringWheel.steeringInput;
        } else {
            turnDirection = Input.GetAxis("Horizontal");
        }
    }
}

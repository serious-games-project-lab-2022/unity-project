using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class MiniatureSpaceship : MonoBehaviour
{
    private SharedGameState sharedGameState;

    void Start()
    {
        SharedGameState.OnInstructorReceivedGameState += () => {
            sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
        };
    }

    void FixedUpdate()
    {
        if (sharedGameState == null)
        {
            return;
        }
        transform.localPosition = sharedGameState.spaceshipPosition.Value / 16;
        transform.eulerAngles = new Vector3(0, 0, sharedGameState.spaceshipRotation.Value);
    }
}

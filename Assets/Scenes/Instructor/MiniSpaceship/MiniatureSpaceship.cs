using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MiniatureSpaceship : MonoBehaviour
{
    private void Start() 
    {
        transform.localPosition = new Vector3(1000, 1000, 0);
    }

    void FixedUpdate()
    {
        var sharedGameState = GameManager.Singleton.sharedGameState;
        if (sharedGameState == null)
        {
            return;
        }
        transform.localPosition = sharedGameState.spaceshipPosition.Value / 8f;
        transform.eulerAngles = new Vector3(0, 0, sharedGameState.spaceshipRotation.Value);
    }
}

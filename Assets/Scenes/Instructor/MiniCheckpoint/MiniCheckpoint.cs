using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCheckpoint : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Singleton.sharedGameState != null)
        {
            transform.localPosition = (
                GameManager.Singleton.sharedGameState.checkpointPosition.Value / 16f
            );
        }
        var instructorManager = GameObject.FindObjectOfType<InstructorManager>();
        instructorManager.OnInstructorReceivedGameState += () => {
            GameManager.Singleton.sharedGameState.checkpointPosition.OnValueChanged +=
                (Vector2 preValue, Vector2 newValue) => {
                    transform.localPosition = newValue / 16f;
                };
        };
    }
}

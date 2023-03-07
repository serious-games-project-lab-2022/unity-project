using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCheckpoint : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Singleton.sharedGameState != null)
        {
            SubscribeToPositionChange();
        }
        else
        {
            var instructorManager = GameObject.FindObjectOfType<InstructorManager>();
            instructorManager.OnInstructorReceivedGameState += SubscribeToPositionChange;
        }
    }

    void SubscribeToPositionChange()
    {
        transform.localPosition = (
            GameManager.Singleton.sharedGameState.checkpointPosition.Value / 16f
        );
        GameManager.Singleton.sharedGameState.checkpointPosition.OnValueChanged +=
            (Vector2 preValue, Vector2 newValue) => {
                transform.localPosition = newValue / 16f;
            };
    }
}

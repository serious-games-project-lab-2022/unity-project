using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class MiniOverworldGoal : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Singleton.sharedGameState != null)
        {
            transform.localPosition = (
<<<<<<< Updated upstream
                GameManager.Singleton.sharedGameState.overworldGoalPosition.Value / 8f
=======
                GameManager.Singleton.sharedGameState.overworldGoalPosition.Value*1000 / 16f
>>>>>>> Stashed changes
            );
        }
        var instructorManager = GameObject.FindObjectOfType<InstructorManager>();
        instructorManager.OnInstructorReceivedGameState += () => {
            GameManager.Singleton.sharedGameState.overworldGoalPosition.OnValueChanged += SubscribeToOverWorldGoalPosition;
        };
    }

    void SubscribeToOverWorldGoalPosition(Vector2 preValue, Vector2 newValue)
    {
        transform.localPosition = newValue / 8f;
    }

    private void OnDestroy()
    {
        GameManager.Singleton.sharedGameState.overworldGoalPosition.OnValueChanged -= SubscribeToOverWorldGoalPosition;
    }
}

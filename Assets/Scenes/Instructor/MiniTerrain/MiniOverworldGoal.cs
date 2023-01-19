using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniOverworldGoal : MonoBehaviour
{
    private SharedGameState sharedGameState;

    void Start()
    {
        SharedGameState.OnInstructorReceivedGameState += () => {
            sharedGameState = GameObject.FindObjectOfType<SharedGameState>();

            sharedGameState.overworldGoalPosition.OnValueChanged += (Vector2 preValue, Vector2 newValue) => {
                transform.localPosition = newValue / 16f;
            };
        };
    }


    private void OnDestroy()
    {
        SharedGameState.OnInstructorReceivedGameState -= () => {
            sharedGameState = GameObject.FindObjectOfType<SharedGameState>();

            sharedGameState.overworldGoalPosition.OnValueChanged -= (Vector2 preValue, Vector2 newValue) => {
                transform.localPosition = newValue / 16f;
            };
        };
    }
}

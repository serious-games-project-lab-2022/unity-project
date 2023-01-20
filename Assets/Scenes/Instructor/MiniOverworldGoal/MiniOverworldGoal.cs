using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniOverworldGoal : MonoBehaviour
{
    void Start()
    {
        SharedGameState.OnInstructorReceivedGameState += () => {
            GameManager.Singleton.sharedGameState.overworldGoalPosition.OnValueChanged +=
                (Vector2 preValue, Vector2 newValue) => {
                    transform.localPosition = newValue / 16f;
                };
        };
    }
}

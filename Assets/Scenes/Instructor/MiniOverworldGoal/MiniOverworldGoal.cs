using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniOverworldGoal : MonoBehaviour
{
    void Start()
    {
        var instructorManager = GameObject.FindObjectOfType<InstructorManager>();
        instructorManager.OnInstructorReceivedGameState += () => {
            GameManager.Singleton.sharedGameState.overworldGoalPosition.OnValueChanged +=
                (Vector2 preValue, Vector2 newValue) => {
                    transform.localPosition = newValue / 16f;
                };
        };
    }
}

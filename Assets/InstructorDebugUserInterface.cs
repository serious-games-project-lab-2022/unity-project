using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructorDebugUserInterface : MonoBehaviour
{
    private SharedGameState sharedGameState;
    [SerializeField] private TextMeshProUGUI spaceshipPositionText;

    void Start()
    {
        // sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
    }

    void Update()
    {
        // spaceshipPositionText.text = $"{sharedGameState.spaceshipPosition.Value.x}, {sharedGameState.spaceshipPosition.Value.x}";
    }
}

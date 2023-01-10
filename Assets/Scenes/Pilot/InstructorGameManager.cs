using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructorGameManager : MonoBehaviour
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
        if (sharedGameState.gameOverSceneTransition.Value)
        {
            SceneManager.LoadScene("GameOver");
        }

        if(sharedGameState.gameWonSceneTransition.Value)
        {
            SceneManager.LoadScene("GameWon");
        }
    }
}

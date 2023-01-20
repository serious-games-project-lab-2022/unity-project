using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructorManager : MonoBehaviour
{
    void Start()
    {
        SharedGameState.OnInstructorReceivedGameState += () => {
            GameManager.Singleton.sharedGameState.OnInstructorReceivedGameEndedRpc += (bool gameEndedSuccessfully) => {
                EndGame(gameEndedSuccessfully);
            };
        };
    }

    void EndGame(bool gameEndedSuccessfully)
    {
        if (gameEndedSuccessfully)
        {
            SceneManager.LoadScene("GameWon");
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}

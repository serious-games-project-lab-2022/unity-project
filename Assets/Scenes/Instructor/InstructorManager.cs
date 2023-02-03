using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructorManager : MonoBehaviour
{
    public delegate void InstructorReceivedGameState();
    public event InstructorReceivedGameState OnInstructorReceivedGameState = delegate {};

    public void OnReceivedGameState()
    {
        GameManager.Singleton.sharedGameState.OnInstructorReceivedGameEndedRpc += (bool gameEndedSuccessfully) => {
            EndGame(gameEndedSuccessfully);
        };
        OnInstructorReceivedGameState();
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

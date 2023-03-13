using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] GameObject gameWonText, gameLostText;
    public static bool GameWon;
    public TextMeshProUGUI scoreText;
    [SerializeField] GameObject ready;
    private void Start()
    {
        gameWonText.SetActive(false);
        gameLostText.SetActive(false);
        
        if(GameWon)
        {
            gameWonText.SetActive(true);
        }
        else
        {
            gameLostText.SetActive(true);
        }
    }
    public void ReturnToMainMenu()
    {
        GameManager.Singleton.DestroyAllPermanentObjects();
    }

    public void InviteToRetry()
    {
        ready.SetActive(true);
        GameManager.Singleton.sharedGameState.InviteToRetry();
    }

    public void Exit()
    {
        Application.Quit();
    }

    /*private void showTheScore()
    {
        GameManager.Singleton.sharedGameState.score.OnValueChanged += (float preValue, float newValue) =>
        {
            scoreText.SetText("Score:{0}", Mathf.RoundToInt(newValue * 10));
        };
    }*/
    private void FixedUpdate()
    {
        var score = GameManager.Singleton.sharedGameState.score.Value;
        scoreText.SetText("Score:{0}", Mathf.RoundToInt(score * 10));

    }
}

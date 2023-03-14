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
    public TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject ready;
    private void Start()
    {
        gameWonText.SetActive(false);
        gameLostText.SetActive(false);
        ready.SetActive(false);

        var score = GameManager.Singleton.sharedGameState.score.Value;
        scoreText.SetText("Score:{0}", Mathf.RoundToInt(score));
        
        if(GameWon)
        {
            gameWonText.SetActive(true);
            if (score > PlayerPrefs.GetInt("HighScore"))
            {
             PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(GameManager.Singleton.sharedGameState.score.Value));
            }

        }

        else
        {
            gameLostText.SetActive(true);
        }
        
        highScoreText.SetText("High Score: {0}", PlayerPrefs.GetInt("HighScore"));
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
}

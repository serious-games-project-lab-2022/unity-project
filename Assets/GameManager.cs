using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private SharedGameState sharedGameState;
    public int maxHealth = 1;
    [HideInInspector]
    public int currentHealthAmount;
    
    public delegate void HealthChanged(int newHealthValue);
    public event HealthChanged OnHealthChanged = delegate {};

    
    private void Start()
    {
        currentHealthAmount = maxHealth;
        sharedGameState = GameObject.FindObjectOfType<SharedGameState>();

        MinigameHandler.OnPlayerLostMinigame += (int damageAmount) =>
        {
            DepleteHealth(by: damageAmount);
        };
        
        OverworldGoal.OnCollidedWithSpaceship += () =>
        {
            WonGame();
        };
    }

  

    private void DepleteHealth(int by = 1)
    {
        var damageAmount = by;
        currentHealthAmount -= damageAmount;
        OnHealthChanged(newHealthValue: currentHealthAmount);

        if (currentHealthAmount <= 0)
        {
            
            EndGame();
        }
    }

    private void EndGame()
    {
        sharedGameState.gameOverSceneTransition.Value = true;
        SceneManager.LoadScene("GameOver");
    }

    private void WonGame()
    {
        sharedGameState.gameWonSceneTransition.Value = true;
        SceneManager.LoadScene("GameWon");
    }
}

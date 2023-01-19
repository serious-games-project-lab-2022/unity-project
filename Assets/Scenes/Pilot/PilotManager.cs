using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PilotManager : MonoBehaviour
{
    private SharedGameState sharedGameState;
    public int maxHealth = 3;
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
            EndGame(gameEndedSuccessfully: true);
        };

        Spaceship.OnCollidedWithTerrain += () => {
            DepleteHealth(by: 1);
        };
    }

    private void DepleteHealth(int by = 1)
    {
        var damageAmount = by;
        currentHealthAmount -= damageAmount;
        OnHealthChanged(newHealthValue: currentHealthAmount);

        if (currentHealthAmount <= 0)
        {
            EndGame(gameEndedSuccessfully: false);
        }
    }

    private void EndGame(bool gameEndedSuccessfully)
    {
        sharedGameState.GameEndedClientRpc(gameEndedSuccessfully);
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

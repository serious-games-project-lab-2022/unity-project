using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PilotManager : MonoBehaviour
{
    private SharedGameState sharedGameState;
    public float maxFuel = 3.0f;
    private float fuelLoss;
    [HideInInspector]
    public float currentFuelAmount;
    
    public delegate void FuelChanged(float newFuelValue);
    public event FuelChanged OnFuelChanged = delegate {};


    private void Start()
    {
        currentFuelAmount = maxFuel;
        sharedGameState = GameObject.FindObjectOfType<SharedGameState>();

        MinigameHandler.OnPlayerLostMinigame += (float damageAmount) =>
        {
            DepleteFuel(by: damageAmount);
        };
        
        OverworldGoal.OnCollidedWithSpaceship += () =>
        {
            EndGame(gameEndedSuccessfully: true);
        };

        Spaceship.OnCollidedWithTerrain += () => {
            DepleteFuel(by: 1.0f);
        };
    }

    private void DepleteFuel(float by = 1.0f)
    {
        var damageAmount = by;
        currentFuelAmount -= damageAmount;
        OnFuelChanged(newFuelValue: currentFuelAmount);

        if (currentFuelAmount <= 0.0f)
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

    private void FixedUpdate()
    {
        fuelLoss = 0.00007f * Time.fixedDeltaTime;
        DepleteFuel(fuelLoss);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PilotManager : MonoBehaviour
{
    public float maxFuel = 3.0f;
    private float fuelLoss;
    [HideInInspector]
    public float currentFuelAmount;
    [SerializeField]
    private MinigameHandler minigameHandler;
    [SerializeField]
    private OverworldGoal overworldGoal;
    [SerializeField]
    private Spaceship spaceship;
    public delegate void FuelChanged(float newFuelValue);
    public event FuelChanged OnFuelChanged = delegate {};


    private void Start()
    {
        currentFuelAmount = maxFuel;

        var terrain = GameManager.Singleton.scenarioManager.terrain;
        var terrainBuilder = GetComponent<TerrainBuilder>();
        terrainBuilder.DrawTilemap(terrain);
        var firstCheckpoint = terrain.CheckpointList[0];
      
        spaceship.transform.localPosition = new Vector3(
            firstCheckpoint.x,
            firstCheckpoint.y,
            0
        );

        minigameHandler.OnPlayerLostMinigame += (float damageAmount) =>
        {
            DepleteFuel(by: damageAmount);
        };
        
        //overworldGoal.OnCollidedWithSpaceship += () =>
        //{
        //    EndGame(gameEndedSuccessfully: true);
        //};

        spaceship.OnCollidedWithTerrain += () => {
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

    public void EndGame(bool gameEndedSuccessfully)
    {
        GameManager.Singleton.sharedGameState.GameEndedClientRpc(gameEndedSuccessfully);
        SceneManager.LoadScene("EndScreen");
    }

    private void FixedUpdate()
    {
        fuelLoss = 0.007f * Time.fixedDeltaTime;
        DepleteFuel(fuelLoss);
        //0.00007f
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

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
    public TextMeshProUGUI scoreText;
    public float score = 0;

    [SerializeField] private GameObject startWindow;
    [SerializeField] private GameObject instructorCheckMark;
    [SerializeField] private GameObject pilotCheckMark;

    private void Awake()
    {
        stopTheGame();
        startWindow.SetActive(true);
     
    }
    private void Start()
    {
        currentFuelAmount = maxFuel;

        var terrain = GameManager.Singleton.scenarioManager.terrain;
        var terrainBuilder = GetComponent<TerrainBuilder>();
        terrainBuilder.DrawTilemap(terrain);
        var firstCheckpoint = terrain.checkpointList[0];
      
        spaceship.transform.localPosition = new Vector3(
            firstCheckpoint.x,
            firstCheckpoint.y,
            0
        );

        minigameHandler.OnPlayerLostMinigame += (float damageAmount) =>
        {
            DepleteFuel(by: damageAmount);
        };
        
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
        if (gameEndedSuccessfully) 
        {
            score += currentFuelAmount*100;
        }

        var sharedGameState = GameManager.Singleton.sharedGameState;
        sharedGameState.GameEndedClientRpc(gameEndedSuccessfully);

        // EndgameScene Infos
        EndSceneManager.GameWon = gameEndedSuccessfully;
        sharedGameState.score.Value = score;
        // Load EndGame
        SceneManager.LoadScene("EndScreen");
    }

    private void FixedUpdate()
    {
        scoreText.SetText("Score:{0}", Mathf.RoundToInt(score*10));
        fuelLoss = 0.007f * Time.fixedDeltaTime;
        DepleteFuel(fuelLoss);
    }

    public void readyButton()
    {
        if (GameManager.Singleton.sharedGameState != null)
        {
            pilotCheckMark.gameObject.SetActive(true);
            GameManager.Singleton.sharedGameState.InviteToStart(true);
        }
    }

    public void stopTheGame()
    {
        Time.timeScale = 0;
    }

    public void resumeTheGame()
    {
        Time.timeScale = 1;
        GameManager.Singleton.sharedGameState.InviteToStart(false);
    }
}

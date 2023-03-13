using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructorManager : MonoBehaviour
{
    public delegate void InstructorReceivedGameState();
    public event InstructorReceivedGameState OnInstructorReceivedGameState = delegate {};

    private TerrainBuilder terrainBuilder;
    [SerializeField] private Transform radar;
    [SerializeField] private GameObject startWindow;
    
    [SerializeField] private GameObject instructorCheckMark;
    [SerializeField] private GameObject pilotCheckMark;
    private void Awake()
    {
        stopTheGame();
        startWindow.SetActive(true);
        
    }
    void Start()
    {
        terrainBuilder = GetComponent<TerrainBuilder>();
        if (GameManager.Singleton.sharedGameState != null)
        {
            BuildTerrainAndSubscribeToChange();
        }
        OnInstructorReceivedGameState += () => {
            BuildTerrainAndSubscribeToChange();
        };
    }

    public void OnReceivedGameState()
    {
        GameManager.Singleton.sharedGameState.OnInstructorReceivedGameEndedRpc += EndGame;
        OnInstructorReceivedGameState();
    }

    void BuildTerrainAndSubscribeToChange()
    {
        var terrain = GameManager.Singleton.sharedGameState.terrain;
        BuildTerrain(terrain.Value);
        terrain.OnValueChanged += (Terrain _, Terrain newTerrain) => {
            BuildTerrain(newTerrain);
        };
    }

    void BuildTerrain(Terrain terrain)
    {
        terrainBuilder.ClearTilemap();
        terrainBuilder.DrawTilemap(terrain);
        print("Hello");
        print(terrain.mapWidth);
        print(terrain.mapHeight);
        radar.localPosition = new Vector3(-terrain.mapWidth/2, -terrain.mapHeight/2, -1);
        print(radar.localPosition);
    }

    void EndGame(bool gameEndedSuccessfully)
    {
        EndSceneManager.GameWon = gameEndedSuccessfully;
        SceneManager.LoadScene("EndScreen");
    }

    public void readyButton()
    {
        if(GameManager.Singleton.sharedGameState != null)
        {
            instructorCheckMark.gameObject.SetActive(true);
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

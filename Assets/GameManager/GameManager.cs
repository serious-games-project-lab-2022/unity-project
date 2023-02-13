using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class GameManager : NetworkBehaviour
{
    public static GameManager Singleton { get; private set; }
    [HideInInspector]
    public ScenarioManager scenarioManager;
    [HideInInspector]
    public SharedGameState sharedGameState;
    [SerializeField] private SharedGameState sharedGameStatePrefab;
    [SerializeField] private ScenarioManager scenarioManagerPrefab;

    private void Awake()
    {
        var singletonAlreadyExists = Singleton != null && Singleton != this;
        if (singletonAlreadyExists)
        {
            Destroy(this.gameObject);
            return;
        }
        Singleton = this;
        DontDestroyOnLoad(this);

        scenarioManager = Instantiate(scenarioManagerPrefab);
        DontDestroyOnLoad(scenarioManager);
    }

    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (ulong clientIdentifier) =>
        {
            var hostConnectingToOwnServer = clientIdentifier == 0;
            if (hostConnectingToOwnServer)
            {
                return;
            }

            if (IsServer)
            {
                InitiateSharedGameState();
            }

            TransitionToGameScene();
        };
        NetworkManager.Singleton.OnClientDisconnectCallback += (ulong clientIdentifier) => {
            SceneManager.LoadScene("Menu");
        };
    }

    public void TransitionToGameScene()
    {
        var sceneName = IsHost ? "Scenes/Pilot/PilotGame" : "Scenes/Instructor/InstructorGame";
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private void InitiateSharedGameState()
    {

        sharedGameState = Instantiate(sharedGameStatePrefab);
        GenerateScenario();
        DontDestroyOnLoad(sharedGameState);

        sharedGameState.GetComponent<NetworkObject>().Spawn();
    }

    public void GenerateScenario()
    {
        scenarioManager.generateScenario();
        sharedGameState.minigameSolutions.Value = scenarioManager.minigameSolutions;
    }

    public void GoBackToMainMenu()
    {
        Destroy(NetworkManager.Singleton.gameObject);
    }

    public void InitHost()
    {
        NetworkManager.Singleton.StartHost(); 
    }

    public void InitClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void BreackHost()
    {
        NetworkManager.Singleton.Shutdown();
    }

}

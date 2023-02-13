using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private NetworkObject sharedGameStatePrefab;
    [SerializeField] private ScenarioManager scenarioManagerPrefab;

    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (ulong clientIdentifier) => {
            // Don't do anything if the host connects to it's own server
            if (clientIdentifier == 0)
            {
                return;
            }

            if (IsServer)
            {
                var scenarioManager = Instantiate(scenarioManagerPrefab);
                DontDestroyOnLoad(scenarioManager);
                scenarioManager.generateScenario();

                var sharedGameState = Instantiate(sharedGameStatePrefab);
                sharedGameState.GetComponent<SharedGameState>().minigameSolutions.Value = scenarioManager.minigameSolutions;
                DontDestroyOnLoad(sharedGameState);
                sharedGameState.Spawn();
            }

            var sceneName = IsHost ? "Scenes/Pilot/PilotGame" : "Scenes/Instructor/InstructorGame";
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        };
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

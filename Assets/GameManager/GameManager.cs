using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager Singleton { get; private set; }
    [SerializeField] private NetworkObject sharedGameStatePrefab;
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
    }

    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (ulong clientIdentifier) => {
            var hostConnectingToOwnServer = clientIdentifier == 0;
            if (hostConnectingToOwnServer)
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

}

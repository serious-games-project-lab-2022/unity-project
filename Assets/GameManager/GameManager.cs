using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

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
        NetworkManager.Singleton.OnClientConnectedCallback += (ulong clientIdentifier) => {
            var hostConnectingToOwnServer = clientIdentifier == 0;
            if (hostConnectingToOwnServer)
            {
                return;
            }

            if (IsServer)
            {
                scenarioManager.generateScenario();

                sharedGameState = Instantiate(sharedGameStatePrefab);
                sharedGameState.minigameSolutions.Value = scenarioManager.minigameSolutions;
                DontDestroyOnLoad(sharedGameState);

                sharedGameState.GetComponent<NetworkObject>().Spawn();
            }

            var sceneName = IsHost ? "Scenes/Pilot/PilotGame" : "Scenes/Instructor/InstructorGame";
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        };
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

}

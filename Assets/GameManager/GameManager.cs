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
        NetworkManager.Singleton.OnClientDisconnectCallback += (ulong clientIdentifier) =>
        {
            if (IsServer)
            {
                // in case the game has been closed / stopped by the player
                Destroy(GameManager.Singleton.sharedGameState.gameObject);
                Destroy(GameManager.Singleton.scenarioManager.gameObject);
                Destroy(GameManager.Singleton.gameObject);
                Destroy(NetworkManager.Singleton.gameObject);
            }
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
        sharedGameState.terrain.Value = scenarioManager.terrain;
        sharedGameState.overworldGoalPosition.Value = new Vector2(1000, 1000);
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

    public void BreakHost()
    {
        NetworkManager.Singleton.Shutdown();
    }

    public void DestroyAllPermanentObjects()
    {
        if(IsServer)
        {
            Destroy(GameManager.Singleton.sharedGameState.gameObject);
            Destroy(GameManager.Singleton.scenarioManager.gameObject);
            Destroy(GameManager.Singleton.gameObject);
            Destroy(NetworkManager.Singleton.gameObject);
        }
        SceneManager.LoadScene("Menu");
    }
}

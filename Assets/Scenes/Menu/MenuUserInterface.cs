using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode.Transports.UTP;

public class MenuUserInterface : NetworkBehaviour
{
    [SerializeField] private Button pilotButton;
    [SerializeField] private Button instructorButton;
    [SerializeField] private NetworkObject sharedGameStatePrefab;

    //private UnityTransport transport;
    public string ipAddress = "127.0.0.1";
    UnityTransport transport;
   
    void Start()
    {

        NetworkManager.Singleton.OnClientConnectedCallback += (ulong clientIdentifier) => {
            // Don't do anything if the host connects to it's own server
            if (clientIdentifier == 0) {
                return;
            }

            if (IsServer) {
                var sharedGameState = Instantiate(sharedGameStatePrefab);
                DontDestroyOnLoad(sharedGameState);
                sharedGameState.Spawn();
            }

            var sceneName = IsHost ? "Scenes/PilotGame" : "Scenes/InstructorGame";
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        };
        pilotButton.onClick.AddListener(() => {
            transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetConnectionData(ipAddress,777);
            NetworkManager.Singleton.StartHost();
            
        });
        instructorButton.onClick.AddListener(() => {
            transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetConnectionData(ipAddress, 777);
            NetworkManager.Singleton.StartClient();
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUserInterface : NetworkBehaviour
{
    [SerializeField] private Button pilotButton;
    [SerializeField] private Button instructorButton;
    [SerializeField] private NetworkObject sharedGameStatePrefab;

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
            NetworkManager.Singleton.StartHost();
        });
        instructorButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
        });
    }
}

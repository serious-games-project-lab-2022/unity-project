using System.Collections;
using System.Collections.Generic;

using Unity.Netcode;

using UnityEngine;
using UnityEngine.UI;

public class NetworkUserInterface : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;

    void Awake()
    {
        hostButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
        joinButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
        });
    }
}

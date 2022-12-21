using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode.Transports.UTP;
using TMPro;
using UnityEngine.Events;

public class MenuUserInterface : NetworkBehaviour
{
    [SerializeField] private Button pilotButton;
    [SerializeField] private Button instructorButton;
    [SerializeField] private NetworkObject sharedGameStatePrefab;

    /// <summary>
    /// The followings are the object that should be turned off/on. E.g turn off the Pilot and Instruktor Button once the user clicks on Instructor/Pilot Button 
    /// </summary>
    [SerializeField] private GameObject ipAddressInput;
    [SerializeField] private TextMeshProUGUI spawnTheIpAdress;
    [SerializeField] private GameObject confiramtionButton;


    
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
            string ipAddress = getIpAddress();
            print(ipAddress);
            transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetConnectionData(ipAddress, 7778);
            spawnTheIpAdress.text = "Your IP Adress is " + ipAddress + "\n waiting for the client...";
            instructorButton.gameObject.SetActive(false);
            pilotButton.gameObject.SetActive(false);

            spawnTheIpAdress.gameObject.SetActive(true);


            NetworkManager.Singleton.StartHost();
        });

        instructorButton.onClick.AddListener(() => {
            instructorButton.gameObject.SetActive(false);
            pilotButton.gameObject.SetActive(false);
            ipAddressInput.gameObject.SetActive(true);
            confiramtionButton.gameObject.SetActive(true);
        });
    }


    string getIpAddress()
    {
        return IPManager.GetIP(ADDRESSFAM.IPv4);
    }

   
}

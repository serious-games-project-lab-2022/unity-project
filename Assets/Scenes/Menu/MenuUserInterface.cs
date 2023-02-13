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
    [SerializeField] private Button confirmationButton;
    [SerializeField] private Button goBackPilotButton;
    [SerializeField] private Button goBackInstructorButton;
    [SerializeField] private TextMeshProUGUI connectionMessage;

    [SerializeField] private TextMeshProUGUI IpAddressInfoLabel;
    [SerializeField] private TMP_InputField ipAddressInput;
    [SerializeField] private GameManager gameManagerPrefab;

    void Start()
    {
        var gameManager = Instantiate(gameManagerPrefab);
        DontDestroyOnLoad(gameManager);

        pilotButton.onClick.AddListener(() => {
            string ipAddress = IpAddressManager.GetIpAddress(IpAddressVersion.v4);
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetConnectionData(ipAddress, 7778);
            IpAddressInfoLabel.text = $"Your IP address is {ipAddress}\n waiting for the client...";

            instructorButton.gameObject.SetActive(false);
            pilotButton.gameObject.SetActive(false);
            IpAddressInfoLabel.gameObject.SetActive(true);
            goBackPilotButton.gameObject.SetActive(true);
            gameManager.InitHost();
        });

        instructorButton.onClick.AddListener(() => {
            instructorButton.gameObject.SetActive(false);
            pilotButton.gameObject.SetActive(false);
            ipAddressInput.gameObject.SetActive(true);
            confirmationButton.gameObject.SetActive(true);
            goBackInstructorButton.gameObject.SetActive(true);
        });

        confirmationButton.onClick.AddListener(() => {
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetConnectionData(ipAddressInput.text, 7778);
            transport.MaxConnectAttempts = 1;
            gameManager.InitClient();


            // the code below will only run if the connection failed
            if (connectionMessage.gameObject.activeSelf)
            {
                connectionMessage.gameObject.SetActive(false);
                Invoke("SetActiveWithDelay", 0.5f);
            }
            
            else
            {
                connectionMessage.gameObject.SetActive(true);
            }
            

        });

        goBackPilotButton.onClick.AddListener(() =>
        {
            gameManager.BreackHost();
            print("host was broken");
            instructorButton.gameObject.SetActive(true);
            pilotButton.gameObject.SetActive(true);
            IpAddressInfoLabel.gameObject.SetActive(false);
            goBackPilotButton.gameObject.SetActive(false);
        });

        goBackInstructorButton.onClick.AddListener(() =>
        {
            instructorButton.gameObject.SetActive(true);
            pilotButton.gameObject.SetActive(true);
            ipAddressInput.gameObject.SetActive(false);
            confirmationButton.gameObject.SetActive(false);
            goBackInstructorButton.gameObject.SetActive(false);
            connectionMessage.gameObject.SetActive(false);
        });


    }

    private void SetActiveWithDelay()
    {
        connectionMessage.gameObject.SetActive(true);
    }
}

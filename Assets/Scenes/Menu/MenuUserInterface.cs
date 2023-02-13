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

    [SerializeField] private TextMeshProUGUI IpAddressInfoLabel;
    [SerializeField] private TMP_InputField ipAddressInput;

    void Start()
    {
        pilotButton.onClick.AddListener(() => {
            string ipAddress = IpAddressManager.GetIpAddress(IpAddressVersion.v4);
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetConnectionData(ipAddress, 7778);
            IpAddressInfoLabel.text = $"Your IP address is {ipAddress}\n waiting for the client...";

            instructorButton.gameObject.SetActive(false);
            pilotButton.gameObject.SetActive(false);
            IpAddressInfoLabel.gameObject.SetActive(true);

            GameManager.Singleton.InitHost();
        });

        instructorButton.onClick.AddListener(() => {
            instructorButton.gameObject.SetActive(false);
            pilotButton.gameObject.SetActive(false);
            ipAddressInput.gameObject.SetActive(true);
            confirmationButton.gameObject.SetActive(true);
        });

        confirmationButton.onClick.AddListener(() => {
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetConnectionData(ipAddressInput.text, 7778);
            transport.MaxConnectAttempts = 2;
            //print(transport.MaxConnectAttempts);
            GameManager.Singleton.InitClient();
        });
    }
}

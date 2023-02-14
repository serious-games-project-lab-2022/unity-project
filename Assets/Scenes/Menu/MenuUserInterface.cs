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
    [SerializeField] private TextMeshProUGUI gameTitle;
    [SerializeField] private TextMeshProUGUI gameDescription;
    [SerializeField] private TextMeshProUGUI connectionMessage;
    [SerializeField] private TextMeshProUGUI ipAddressInfoLabel;
    [SerializeField] private TMP_InputField ipAddressInput;

    void Start()
    {
        pilotButton.onClick.AddListener(() => {
            string ipAddress = IpAddressManager.GetIpAddress(IpAddressVersion.v4);
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetConnectionData(ipAddress, 7778);
            ipAddressInfoLabel.text = $"Your IP address is {ipAddress}\n waiting for the client...";

            instructorButton.gameObject.SetActive(false);
            pilotButton.gameObject.SetActive(false);
            gameTitle.gameObject.SetActive(false);
            gameDescription.gameObject.SetActive(false);
            ipAddressInfoLabel.gameObject.SetActive(true);
            goBackPilotButton.gameObject.SetActive(true);
            GameManager.Singleton.InitHost();
        });

        instructorButton.onClick.AddListener(() => {
            instructorButton.gameObject.SetActive(false);
            pilotButton.gameObject.SetActive(false);
            gameTitle.gameObject.SetActive(false);
            gameDescription.gameObject.SetActive(false);
            ipAddressInput.gameObject.SetActive(true);
            confirmationButton.gameObject.SetActive(true);
            goBackInstructorButton.gameObject.SetActive(true);
        });

        confirmationButton.onClick.AddListener(() => {
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetConnectionData(ipAddressInput.text, 7778);
            transport.MaxConnectAttempts = 1;
            GameManager.Singleton.InitClient();

            StartCoroutine(ShowErroMessage());
        });

        goBackPilotButton.onClick.AddListener(() =>
        {
            GameManager.Singleton.BreakHost();
            print("host was broken");
            instructorButton.gameObject.SetActive(true);
            pilotButton.gameObject.SetActive(true);
            gameTitle.gameObject.SetActive(true);
            gameDescription.gameObject.SetActive(true);
            ipAddressInfoLabel.gameObject.SetActive(false);
            goBackPilotButton.gameObject.SetActive(false);
        });

        goBackInstructorButton.onClick.AddListener(() =>
        {
            instructorButton.gameObject.SetActive(true);
            pilotButton.gameObject.SetActive(true);
            gameTitle.gameObject.SetActive(true);
            gameDescription.gameObject.SetActive(true);
            ipAddressInput.gameObject.SetActive(false);
            confirmationButton.gameObject.SetActive(false);
            goBackInstructorButton.gameObject.SetActive(false);
            connectionMessage.gameObject.SetActive(false);
        });


    }

    private IEnumerator ShowErroMessage()
    {
        yield return new WaitForSeconds(0.4f);
        connectionMessage.gameObject.SetActive(true);
    }
}

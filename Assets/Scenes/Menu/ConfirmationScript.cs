using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class ConfirmationScript : MonoBehaviour
{
    UnityTransport transport;
   [SerializeField] private TMP_InputField inputField;
   public void OnClick()
    {
        print(inputField.text);
        transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetConnectionData(inputField.text, 7778);
        NetworkManager.Singleton.StartClient();
    }
}

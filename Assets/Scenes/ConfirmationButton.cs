using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmationButton : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private NetworkManager networkManager;
   // [SerializeField] private List<GameObject> deactivate;
    public void OnClick()
    {
        
        networkManager.GetComponent<UnityTransport>().ConnectionData.Address = textMeshProUGUI.text;
        SceneManager.LoadScene(1);
    }
}


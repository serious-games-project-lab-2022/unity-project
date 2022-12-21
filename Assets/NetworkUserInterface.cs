using System.Collections;
using System.Collections.Generic;

using TMPro;

using Unity.Netcode;

using UnityEngine;
using UnityEngine.UI;

public class NetworkUserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hashText;
    [SerializeField] private SharedGameState sharedGameState;

    void Awake()
    {
       // hashText.text = sharedGameState.GetComponent<NetworkObject>().GetHashCode().ToString();
    }
}

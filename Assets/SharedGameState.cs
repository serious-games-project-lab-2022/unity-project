using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SharedGameState : NetworkBehaviour
{
    public NetworkVariable<Vector3> spaceshipPosition = new NetworkVariable<Vector3>(new Vector3(0, 0, 0), NetworkVariableReadPermission.Everyone);
    private NetworkObject networkObject;
    [SerializeField] private GameObject instructorShip; 
    private bool IsPilot {
        get { return IsHost; }
    }
    private bool IsInstructor {
        get { return !IsHost; }
    }
    public delegate void InstructorReceivedGameState();
    public static event InstructorReceivedGameState OnInstructorReceivedGameState = delegate { };

    public override void OnNetworkSpawn()
    {
        if (IsInstructor) {
            DontDestroyOnLoad(this);
            OnInstructorReceivedGameState();
            InstructorReadyServerRpc();
           
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void InstructorReadyServerRpc()
    {
    }
}

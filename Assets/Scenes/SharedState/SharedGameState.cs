using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SharedGameState : NetworkBehaviour
{
    public NetworkVariable<Vector2> spaceshipPosition = new NetworkVariable<Vector2>(new Vector2(0, 0));
    private NetworkObject networkObject;

    void Start()
    {
        
    }

    void OnNetworkSpawn()
    {
        if (!IsServer) {

        }
    }
}

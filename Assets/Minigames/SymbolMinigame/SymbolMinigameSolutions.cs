using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SymbolMinigameSolutions : INetworkSerializable
{
    public SymbolMinigameSolution solution;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        solution.NetworkSerialize(serializer);
    }
}

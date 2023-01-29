using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FrequenzMinigameSolution : INetworkSerializable
{
    public float amplitude;
    public float frequence;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            amplitude = 0.0f;
            frequence = 0.0f;
        }

        serializer.SerializeValue(ref amplitude);
        serializer.SerializeValue(ref frequence);
    }
}

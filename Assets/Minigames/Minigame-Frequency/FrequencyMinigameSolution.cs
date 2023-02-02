using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FrequencyMinigameSolution : INetworkSerializable
{
    public float amplitude;
    public float frequency;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            amplitude = 0.0f;
            frequency = 0.0f;
        }

        serializer.SerializeValue(ref amplitude);
        serializer.SerializeValue(ref frequency);
    }
}

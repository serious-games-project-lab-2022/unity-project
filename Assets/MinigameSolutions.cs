using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public struct MinigameSolutions: INetworkSerializable
{
    public Vector2[] shapeMinigameSolution;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        int length = 0;
        if (!serializer.IsReader)
        {
            length = shapeMinigameSolution.Length;
        }
        serializer.SerializeValue(ref length);

        if (serializer.IsReader)
        {
            shapeMinigameSolution = new Vector2[length];
        }

        for (int index = 0; index < length; index++)
        {
            serializer.SerializeValue(ref shapeMinigameSolution[index]);
        }
    }
}

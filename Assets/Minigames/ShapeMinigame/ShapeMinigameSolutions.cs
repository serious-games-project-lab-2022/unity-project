using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShapeMinigameSolutions : INetworkSerializable
{
    public ShapeMinigameSolution solutions;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        // int length = 0;
        // if (!serializer.IsReader)
        // {
        //     length = solutions.Length;
        // }
        // serializer.SerializeValue(ref length);

        // if (serializer.IsReader)
        // {
        //     solutions = new ShapeMinigameSolution[length];
        // }

        // for (int index = 0; index < length; index++)
        // {
            solutions.NetworkSerialize(serializer);
        // }
    }
}

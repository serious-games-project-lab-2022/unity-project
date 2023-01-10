using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShapeMinigameSolution: INetworkSerializable
{
    public int[] shapeIndices;
    public Vector2[] relativePositions;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        int length = 0;
        if (!serializer.IsReader)
        {
            length = relativePositions.Length;
        }
        serializer.SerializeValue(ref length);

        if (serializer.IsReader)
        {
            shapeIndices = new int[length];
            relativePositions = new Vector2[length];
        }

        for (int index = 0; index < length; index++)
        {
            serializer.SerializeValue(ref shapeIndices[index]);
            serializer.SerializeValue(ref relativePositions[index]);
        }
    }
}

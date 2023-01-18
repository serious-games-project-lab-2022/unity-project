using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public struct MinigameSolutions: INetworkSerializable
{
    public ShapeMinigameSolutions shapeMinigameSolutions;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            shapeMinigameSolutions = new ShapeMinigameSolutions {
                solutions = new ShapeMinigameSolution {
                    relativePositions = new Vector2[] {},
                    shapeIndices = new int[] {},
                }
            };
        }
        shapeMinigameSolutions.NetworkSerialize(serializer);
    }
}

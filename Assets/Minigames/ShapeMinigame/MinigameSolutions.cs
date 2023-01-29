using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public struct MinigameSolutions: INetworkSerializable
{
    public ShapeMinigameSolutions shapeMinigameSolutions;
    public FrequenzMinigameSolutions frequenzMinigameSolutions;
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

            frequenzMinigameSolutions = new FrequenzMinigameSolutions
            {
                solution = new FrequenzMinigameSolution
                {
                    amplitude = 0.0f,
                    frequence = 0.0f,
                }
            };
        }
        shapeMinigameSolutions.NetworkSerialize(serializer);
        frequenzMinigameSolutions.NetworkSerialize(serializer);
    }
}

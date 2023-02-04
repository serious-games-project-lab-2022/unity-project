using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public struct MinigameSolutions: INetworkSerializable
{
    public ShapeMinigameSolutions shapeMinigameSolutions;
    public FrequencyMinigameSolutions frequencyMinigameSolutions;
    public SymbolMinigameSolutions symbolMinigameSolutions;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            shapeMinigameSolutions = new ShapeMinigameSolutions {
                solutions = new ShapeMinigameSolution
                {
                    relativePositions = new Vector2[] {},
                    shapeIndices = new int[] {},
                }
            };
        
            frequencyMinigameSolutions = new FrequencyMinigameSolutions {
                solution = new FrequencyMinigameSolution
                {
                    amplitude = 0.0f,
                    frequency = 0.0f,
                }
            };
            symbolMinigameSolutions = new SymbolMinigameSolutions
            {
                solution = new SymbolMinigameSolution
                {
                    sameSymbolsIndices = new int[] {},
                    pilotSymbolIndices = new int[] {},
                    instructorSymbolIndices = new int [] {},
                }
            };
        }

        shapeMinigameSolutions.NetworkSerialize(serializer);
        frequencyMinigameSolutions.NetworkSerialize(serializer);
    }
}

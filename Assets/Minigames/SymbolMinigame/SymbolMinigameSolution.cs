using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SymbolMinigameSolution : INetworkSerializable
{
    public int[] sameSymbolsIndices;
    public int[] pilotSymbolIndices;
    public int[] instructorSymbolIndices;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            sameSymbolsIndices = new int[6];
            pilotSymbolIndices = new int[3];
            instructorSymbolIndices = new int[3];
        }

        for (int index = 0; index < sameSymbolsIndices.Length; index++)
        {
            serializer.SerializeValue(ref sameSymbolsIndices[index]);
        }

        for (int index = 0; index < pilotSymbolIndices.Length; index++)
        {
            serializer.SerializeValue(ref pilotSymbolIndices[index]);
            serializer.SerializeValue(ref instructorSymbolIndices[index]);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public struct Terrain: INetworkSerializable
{
   public List<Vector3Int> emptySpaceList;

   public List<Vector3Int> checkpointList;

   public int mapHeight;
   public int mapWidth;

   public Terrain(
      List<Vector3Int> emptySpaceList,
      List<Vector3Int> checkpointList,
      int mapHeight,
      int mapWidth
   )
   {
      this.emptySpaceList = emptySpaceList;
      this.checkpointList = checkpointList;
      this.mapHeight = mapHeight;
      this.mapWidth = mapWidth;
   }

   public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
   {
      int emptySpaceListLength = 0;
      Vector3Int[] emptySpaceArray = new Vector3Int[emptySpaceListLength];
      if (serializer.IsWriter)
      {
         emptySpaceListLength = emptySpaceList.Count;
         emptySpaceArray = emptySpaceList.ToArray();
      }
      serializer.SerializeValue(ref emptySpaceListLength);

      if (serializer.IsReader)
      {
         emptySpaceArray = new Vector3Int[emptySpaceListLength];
      }

      for (int index = 0; index < emptySpaceListLength; index++)
      {
         serializer.SerializeValue(ref emptySpaceArray[index]);
      }

      if (serializer.IsReader)
      {
         emptySpaceList = new List<Vector3Int>(emptySpaceArray);
      }

      serializer.SerializeValue(ref mapHeight);
      serializer.SerializeValue(ref mapWidth);
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public struct PlayerInfo : INetworkSerializable, IEquatable<PlayerInfo>, IDisposable
{
    public ulong clientId;
    public FixedString128Bytes Name;
    public bool isPlayerReady;

    //Serializable requirement
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref clientId);
        serializer.SerializeValue(ref Name);
        serializer.SerializeValue(ref isPlayerReady);
    }

    //required for IEquatable
    public bool Equals(PlayerInfo other)
    {
        return clientId == other.clientId;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

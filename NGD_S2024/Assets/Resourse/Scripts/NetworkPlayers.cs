using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayers : NetworkBehaviour
{
    public NetworkList<PlayerInfo> allNetPlayers;
    public int playerCount;
    
    //have to ini in awake, could cause memory leaks
    public void Awake()
    {
        allNetPlayers = new NetworkList<PlayerInfo>();
    }

    void start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (IsServer)
        {
            SeverStart();
        }

        
    }

    private void SeverStart()
    {
        PlayerInfo info = new PlayerInfo(NetworkManager.LocalClientId);
        info._isPlayerReady = true;
        allNetPlayers.Add(info);
        
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Netcode;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class KnightsSpwaner : NetworkBehaviour
{
    
    
    private NetworkVariable<int> spawnIndex = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public Transform[] mySpawns;

  

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // allows use to do something when a client conects
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
           // NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
            spawnIndex.Value = 0;
     
      //  spawnIndex.OnValueChanged += SpawnChanged;
        }
    }

    private void OnClientConnectedCallback(ulong id)
    {
        SetPlayerServerRpc(id);
       
    }


    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerServerRpc(ulong id)
    {
        Transform tempT = mySpawns[spawnIndex.Value];
        //get the client GO and set it.
        NetworkManager.Singleton.ConnectedClients[id].PlayerObject.gameObject.transform.position = tempT.position; 
      
        spawnIndex.Value++;
    }
}



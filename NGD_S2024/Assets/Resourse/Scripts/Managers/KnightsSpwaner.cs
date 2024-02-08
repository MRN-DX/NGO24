using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class KnightsSpwaner : NetworkBehaviour
{

    public GameObject playerPrefab;
 
    public Button startGameBttn;
  
   
    private int spawnIndex = 0;
    private int spawnedChar = 0;

    public Transform[] mySpawns;

    private void Start()
    {
        
        
        if (IsHost)
        {
            startGameBttn.onClick.AddListener(SpawnPlayers);
            
        }
        else
        {
            startGameBttn.gameObject.SetActive(false);
        }
        
       
    }

    private void SpawnPlayers()
    {

        foreach (ulong clientID in NetworkManager.ConnectedClientsIds)
        {

            Vector3 myPOS = mySpawns[spawnIndex].TransformPoint(0f,0f,0f);
            //spawn a player
            GameObject playerSpawn = Instantiate(playerPrefab, myPOS, quaternion.identity);
            
            
            playerSpawn.GetComponent<NetworkObject>().SpawnWithOwnership(clientID);
            //playerSpawn.GetComponent<SkinChanger>().setCharClientRpc(spawnedChar);
            //playerSpawn.GetComponent<SkinChanger>().randomMatClientRpc();
              
            if (spawnIndex > mySpawns.Length - 1)
              {

                  spawnIndex++;
              }
              else
              {
                  spawnIndex = 0;
              }

            spawnedChar++;
        }
        
    }

    
        
    

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // allows use to do something when a client conects
           // NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
           // NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
            spawnIndex = 0;
     
      //  spawnIndex.OnValueChanged += SpawnChanged;
        }
    }


    public void StartGame()
    {
        
    }

    private void OnClientConnectedCallback(ulong id)
    {
        SetPlayerServerRpc(id);
       
    }


    [ServerRpc(RequireOwnership = false)]
    public void SetPlayerServerRpc(ulong id)
    {
        Transform tempT = mySpawns[spawnIndex];
        //get the client GO and set it.
        NetworkManager.Singleton.ConnectedClients[id].PlayerObject.gameObject.transform.position = tempT.position; 
      
        spawnIndex++;
    }
}



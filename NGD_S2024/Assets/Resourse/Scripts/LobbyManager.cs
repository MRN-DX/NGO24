using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour
{
    //our UI buttons in the lobby UI
    
    
    [SerializeField] Button startBttn, leaveBttn, readyBttn;
  
    [SerializeField] private GameObject PanelPrefab;
    [SerializeField] private GameObject ContentGO;
    
    
    public NetworkList<PlayerInfo> allNetPlayers = new NetworkList<PlayerInfo>();
    private List<GameObject> playerPanels = new List<GameObject>();

    private ulong myLocalClientID;
//Colors for users
    private Color[] playerColors = new Color[]
    {
        Color.blue,
        Color.magenta,
        Color.cyan,
        Color.yellow
    };


    private void Start()
    {
      
        if (IsHost)
        {
            foreach (NetworkClient nc in NetworkManager.ConnectedClientsList)
            {
               
                AddPlayerToList(nc.ClientId);
            }
            
            RefreshPlayerPanels();
        }
        
        myLocalClientID = NetworkManager.LocalClientId;
    }


    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HostOnClientConnected;
        }
        
        // must be after host connects to signals
        base.OnNetworkSpawn();


        if (IsClient)
        {
            //networklist
            allNetPlayers.OnListChanged += ClientOnAllPlayersChanged;
          //  NetworkManager.Singleton.OnClientDisconnectCallback += ClientDissconnected;
           
        }
    }

   


    private void ClientOnAllPlayersChanged(NetworkListEvent<PlayerInfo> changeEvent)
    {
       RefreshPlayerPanels();
    }

    private void HostOnClientConnected(ulong clientID)
    {
           AddPlayerToList(clientID);
           RefreshPlayerPanels();
    }


   

    //  private NetworkList<PlayerInfo> allPlayers = new NetworkList<PlayerInfo>();
    // Start is called before the first frame update
    
    // Add Players panel phystically triger each prefab for 

    private void AddPlayerToList(ulong clientID)
    {
        allNetPlayers.Add(new PlayerInfo(clientID));
    }
    
    private void AddPlayerPanel(PlayerInfo info)
    {
        GameObject newPanel = Instantiate(PanelPrefab, ContentGO.transform);
        LobbyPlayerLabel LPL = newPanel.GetComponent<LobbyPlayerLabel>();
        LPL.setPlayerName(info._clientId);
       
        if (IsServer)
        {
        LPL.onKickClicked += KikckUserBttn;
        }
        
        if (IsClient && !IsHost || info._clientId == myLocalClientID)
        {
            LPL.setKickActive(false);
        }
        playerPanels.Add(newPanel);
    }

    private void RefreshPlayerPanels()
    {
        foreach (GameObject panel in playerPanels)
        {
            Destroy(panel);
            
        }
        playerPanels.Clear();

        foreach (PlayerInfo pi in allNetPlayers)
        {
            AddPlayerPanel(pi);
        }
    }
    
    
    public void KikckUserBttn(ulong kickTarget)
    {
        if (!IsServer || !IsHost) return;
        foreach (PlayerInfo pi in allNetPlayers)
        {
            if (pi._clientId == kickTarget)
            {
                allNetPlayers.Remove(pi);
                
                // send RPC to target client to discconnect/scene
                DisconnectClient(kickTarget);
               
            }
            
        }
      
        RefreshPlayerPanels();
    }

    
    public void DisconnectClient(ulong kickTarget)
    {
        ClientRpcParams clientRpcParams = default;
        clientRpcParams.Send.TargetClientIds = new ulong[1] { kickTarget };
        DisconnectionClientRPC(clientRpcParams);
        NetworkManager.Singleton.DisconnectClient(kickTarget);
        
    }

    [ClientRpc]
    public void DisconnectionClientRPC(ClientRpcParams clientRpcParams)
    {
        SceneManager.LoadScene(0);
    }
 
    
    
}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour
{
    //our UI buttons in the lobby UI
    [SerializeField] Button startBttn, leaveBttn, readyBttn;

    [SerializeField] private GameObject PanelPrefab;

    private NetworkList<PlayerInfo> allPlayers = new NetworkList<PlayerInfo>();
    // Start is called before the first frame update
    
    // Add Players panel phystically triger each prefab for 

    private void AddPlayerPanel(PlayerInfo info)
    {
        
        
    }
    
}

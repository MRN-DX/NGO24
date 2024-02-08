using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_NetManager : NetworkBehaviour
{

    [SerializeField] private Button hostBttn, joinBttn, serverBttn, startBttn;

    public TMP_Text statusTxt;
    public GameObject startUI;
    private void Start()
    {
        //Listeners for buttns
        startBttn.onClick.AddListener(StartBttnClick);
        hostBttn.onClick.AddListener(HostClick);
        joinBttn.onClick.AddListener(JoinClick);
        serverBttn.onClick.AddListener(ServerClick);

        //subscribe to events
        NetworkManager.OnServerStarted += OnServerStarted;
        NetworkManager.OnClientStarted += OnClientStarted;
      
        
        startBttn.gameObject.SetActive(false);
    }

    
    
    private void OnServerStarted()
    {
        startBttn.gameObject.SetActive(true);
        statusTxt.text = "Press Start";

    }


    private void OnClientStarted()
    {
        if (!IsHost)
        {
            statusTxt.text = "Wait for game to start";
        }
        
    }


    private void StartBttnClick()
    {
        StartGame();
    }

    private void StartGame()
    {
        // tell the manager to load the scene for everyone 
        NetworkManager.SceneManager.LoadScene("DefenderTower", LoadSceneMode.Single);
    }
    
   
    private void HostClick()
    {
        NetworkManager.Singleton.StartHost();
    }
    
    private void ServerClick()
    {
        NetworkManager.Singleton.StartHost();
    }
    
    private void JoinClick()
    {
        NetworkManager.Singleton.StartClient();
    }
  
    

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.UI;

public class UI_NetManager : NetworkBehaviour
{

    [SerializeField]
    private Button hostBttn, joinBttn, arrowBttn, cannonBttn;

    private Transform spawnSpot;

    private void Start()
    {

        hostBttn.onClick.AddListener(HostClick);
        joinBttn.onClick.AddListener(JoinClick);
    }
    
    private void HostClick()
    {
        NetworkManager.Singleton.StartHost();
    }
    
    private void JoinClick()
    {
        NetworkManager.Singleton.StartClient();
    }
  
    

}

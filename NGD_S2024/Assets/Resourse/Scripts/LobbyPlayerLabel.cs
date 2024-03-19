using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LobbyPlayerLabel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected TMP_Text PlayerText;
    [SerializeField] protected Image readyImage, colorImage;
    [SerializeField] protected Button kickBttn;

    public event Action<ulong> onKickClicked;
    private ulong _clientID;

    private void OnEnable()
    {
        kickBttn.onClick.AddListener(BtnKick_Clicked);
    }

    public void setPlayerName(ulong playerName)
    {
        _clientID = playerName;
        PlayerText.text = "Player "+playerName.ToString();
    }
    
    private void BtnKick_Clicked()
    {
        onKickClicked?.Invoke(_clientID);
    }

    public void setKickActive(bool isOn)
    {
        kickBttn.gameObject.SetActive(isOn);
    }

    public void SetReady(bool ready)
    {
        if (ready)
        {
            readyImage.material.color = Color.green;
        }
        else
        {
            readyImage.material.color = Color.red;
        }
    }

    public void SetIconColor(Color color)
    {
        colorImage.material.color = color;
    }

    
    
}

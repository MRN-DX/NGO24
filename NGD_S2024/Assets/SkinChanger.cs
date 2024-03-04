using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkinChanger : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] CharacterGO;

    public NetworkVariable<int> currentCharId = new NetworkVariable<int>(0);
    public NetworkVariable<int> currentMatId = new NetworkVariable<int>(0);


    [SerializeField]
    private Material[] myMats;


    //Swap char
    [ClientRpc]
    public void setCharClientRpc(int charID)
    {
        foreach (GameObject cGO in CharacterGO)
        {
            cGO.SetActive(false);
        }

        currentCharId.Value = charID;
        CharacterGO[currentCharId.Value].SetActive(true);
    }
    
    [ClientRpc]
    public void setMatClientRpc(int matID)
    {
        // set materials
        currentMatId.Value = matID;
        CharacterGO[currentCharId.Value].GetComponent<SkinnedMeshRenderer>().material = myMats[matID];
    }
   
    [ClientRpc]
    public void randomCharAndMatClientRpc()
    {
        currentCharId.Value = Random.Range(0, CharacterGO.Length - 1);
        currentMatId.Value = Random.Range(0, myMats.Length - 1);
        setCharClientRpc(currentCharId.Value);
        setMatClientRpc(currentMatId.Value);


    }
   
    [ClientRpc]
    public void randomCharClientRpc()
    {
        currentCharId.Value = Random.Range(0, CharacterGO.Length - 1);
        setCharClientRpc(currentCharId.Value);
       
    }
   
    [ClientRpc]
    public void randomMatClientRpc()
    {
        currentMatId.Value = Random.Range(0, myMats.Length - 1);
        setMatClientRpc(currentMatId.Value);
       
    }
    
    
}

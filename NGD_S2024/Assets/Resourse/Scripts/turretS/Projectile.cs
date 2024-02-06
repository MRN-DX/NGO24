using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{

    [SerializeField] private float _speed = 20f;


    //set velocity on spawn
    public override void OnNetworkSpawn()
    {
        GetComponent<Rigidbody>().velocity = this.transform.forward * _speed;
    }
}

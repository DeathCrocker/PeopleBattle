
using System;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControll>().EarnCoin();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _rb;
    private Vector2 _vector2;
    private PhotonView _phView;

    private void Start()
    {
        _phView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (_phView.IsMine)
        {
            
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerControll>().TakeDamage(20);
        }
        PhotonNetwork.Destroy(gameObject);
    }
}

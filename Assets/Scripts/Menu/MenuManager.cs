using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField _inputName;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        _inputName.text = PlayerPrefs.GetString("name");
        PhotonNetwork.NickName = _inputName.text;
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8;
        PhotonNetwork.CreateRoom(null, roomOptions,null);
    }

    public void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameScene");
        
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("name", _inputName.text);
        PhotonNetwork.NickName = _inputName.text;
    }
    
}

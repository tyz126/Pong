using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyManager : Photon.PunBehaviour
{
    public TMP_InputField inputField;
    public GameObject panel;
    bool isCreateRoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateRoom()
    {
        isCreateRoom = true;
        PhotonNetwork.sendRate = 120;
        PhotonNetwork.ConnectUsingSettings("1.0.0");
    }

    public void JoinRoom()
    {
        isCreateRoom = false;
        PhotonNetwork.sendRate = 120;
        PhotonNetwork.ConnectUsingSettings("1.0.0");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (isCreateRoom)
        {
            PhotonNetwork.CreateRoom(inputField.text);
        }
        else
        {
            PhotonNetwork.JoinRoom(inputField.text);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }
}

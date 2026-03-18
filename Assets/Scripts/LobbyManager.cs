using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : Photon.PunBehaviour
{
    public TMP_InputField inputField;
    public GameObject panel;
    bool connectedToMaster;
    bool createRoom;

    public void CreateRoom()
    {
        createRoom = true;
        panel.SetActive(true);
        if (connectedToMaster)
        {
            PhotonNetwork.CreateRoom(inputField.text);
            return;
        }
        PhotonNetwork.ConnectUsingSettings("1.0.0");
    }

    public void JoinRoom()
    {
        createRoom = false;
        panel.SetActive(true);
        if (connectedToMaster)
        {
            PhotonNetwork.JoinRoom(inputField.text);
            return;
        }
        PhotonNetwork.ConnectUsingSettings("1.0.0");
    }

    public override void OnConnectedToMaster()
    {
        connectedToMaster = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (createRoom)
        {
            PhotonNetwork.CreateRoom(inputField.text);
        }
        else
        {
            PhotonNetwork.JoinRoom(inputField.text);
        }
    }
}

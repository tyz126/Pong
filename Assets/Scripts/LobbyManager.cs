using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyManager : Photon.PunBehaviour
{
    public TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(inputField.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(inputField.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }
}

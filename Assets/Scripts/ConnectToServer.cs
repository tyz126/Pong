using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToServer : Photon.PunBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.sendRate = 120;
        PhotonNetwork.ConnectUsingSettings("1.0.0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene(1);
    }
}

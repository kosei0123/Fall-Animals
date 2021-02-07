using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        Debug.Log("Master");
        //ロビーに入室する
        PhotonNetwork.JoinLobby();
    }

    //ロビーに入った際の処理
    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby");
        //RoomOptionsの用意
        RoomOptions roomOptions = new RoomOptions();
        //PlayerのMac人数を指定
        roomOptions.MaxPlayers = 2;

        //ルームの作成・入室
        PhotonNetwork.JoinOrCreateRoom("Game Room", roomOptions, TypedLobby.Default);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

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
        Debug.Log("Join");

        //ルームの作成・入室
        PhotonNetwork.JoinRandomRoom();
    }

    //部屋への参加に失敗した際は、作成する
    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        //RoomOptionsの用意
        RoomOptions roomOptions = new RoomOptions();
        //PlayerのMac人数を指定
        roomOptions.MaxPlayers = 4;
        //ルームの作成
        PhotonNetwork.CreateRoom("Room" + Random.Range(1,100),roomOptions);
    }
}

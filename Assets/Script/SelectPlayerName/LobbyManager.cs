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
        //入室の可否
        roomOptions.IsOpen = true;
        //PlayerのMac人数を指定
        roomOptions.MaxPlayers = 2;

        //カスタムプロパティの設定
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            //待機所内の人数カウント
            {"WaitingRoomPlayerCount", 0}
        };
        //ロビーにカスタムプロパティの情報を表示させる
        roomOptions.CustomRoomPropertiesForLobby = new string[]
        {
            "WaitingRoomPlayerCount",
        };

        //ルームの作成
        PhotonNetwork.CreateRoom("Room" + Random.Range(1,100),roomOptions);
    }

    //ルームオプションを更新する
    public static void UpdateRoomOptions(bool newIsOpen)
    {
        //入室の可否変更
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = newIsOpen;
        }
    }
}

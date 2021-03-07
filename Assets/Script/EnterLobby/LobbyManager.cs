using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    //Room入室時にtrueを返す
    public bool joinedRoomFlag;

    //一定時間操作がなかった時に接続を切る用
    private float disconnectTime;

    // Start is called before the first frame update
    void Start()
    {
        //Room入室時にtrueを返す
        joinedRoomFlag = false;

        //時間の設定(20秒)
        disconnectTime = 20;
    }

    // Update is called once per frame
    void Update()
    {
        //一定時間操作がなかった時に退出
        if (disconnectTime > 0)
        {
            disconnectTime -= Time.deltaTime;
        }
        else
        {
            //画面遷移
            SceneManager.LoadScene("Menu");

            //Photonに接続を解除する
            if (PhotonNetwork.IsConnected == true)
            {
                PhotonNetwork.Disconnect();
            }
        }
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
        //PlayerのMax人数を指定
        roomOptions.MaxPlayers = 4;
        //去っていくプレイヤーが生成したオブジェクトが破壊されないようにする
        //roomOptions.CleanupCacheOnLeave = false;

        //カスタムプロパティの設定
        roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
        {
            //待機所内の人数カウント
            {"WaitingRoomPlayerCount", 0},
            //残り人数カウント
            {"RemainingPlayerCount", 0},
            //マスタークライアントの切断検出用
            {"NoMasterCliant", false},
            //ステージの確定
            {"DefinedStage", 1}
        };
        //ロビーにカスタムプロパティの情報を表示させる
        roomOptions.CustomRoomPropertiesForLobby = new string[]
        {
            "WaitingRoomPlayerCount",
            "RemainingPlayerCount",
            "NoMasterCliant",
            "DefinedStage",
        };

        //ルームの作成
        PhotonNetwork.CreateRoom("Room" + Random.Range(1,100),roomOptions);
    }

    //部屋に入室した時
    public override void OnJoinedRoom()
    {
        joinedRoomFlag = true;
        //ニックネームの登録
        PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("NickName");
        //人数により部屋をクローズする
        UpdateRoomOptions(true);
    }

    //ルームオプションを更新する
    public static void UpdateRoomOptions(bool newIsOpen)
    {
        //強制的にルームをクローズ
        if (newIsOpen == false)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        //人数によりルームをクローズ
        else if(newIsOpen == true)
        {
            //入室の可否変更
            if (PhotonNetwork.CurrentRoom.PlayerCount >= 4)
            {
                if (PhotonNetwork.InRoom)
                {
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                }
            }
            else
            {
                if (PhotonNetwork.InRoom)
                {
                    PhotonNetwork.CurrentRoom.IsOpen = true;
                }
            }
        }
    }

    //キックされた時用
    public override void OnLeftRoom()
    {
        LobbyManager_PhotonOff();
    }

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            LobbyManager_PhotonOff();
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
        LobbyManager_PhotonOff();
    }

    //Photon接続解除や画面の遷移
    private void LobbyManager_PhotonOff()
    {
        //画面遷移
        SceneManager.LoadScene("Menu");

        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }
    }
}

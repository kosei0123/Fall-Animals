using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class WaitingPlayerCount : MonoBehaviourPunCallbacks
{
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;

    //待機人数の表示
    [SerializeField]
    private Text WaitingPlayerCountText;

    //プレイヤー番号
    public static int playerCreatedNumber;

    //スタート時間を設定する
    [SerializeField]
    private Text StartTimeText;
    private float waitingBattleStartTime;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //ルーム内のクライアントがMasterClientと同じシーンをロードするように設定
        PhotonNetwork.AutomaticallySyncScene = true;

        //同じルーム内のWaitingRoomにいるプレイヤーの数を数える
        var n = PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] is int value ? value : 0;
        PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] = n + 1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
        //プレイヤー番号の決定
        playerCreatedNumber = (int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"];

        //プレイヤーが入っていた時にバトルスタート時間を設定する
        waitingBattleStartTime = 5.0f;

    }

    // Update is called once per frame
    void Update()
    {
        //待機人数の表示
        WaitingPlayerCountText.text = "ルーム内待機中プレイヤー : " + PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"]  + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        //バトルスタート時間を表示する
        StartTimeText.text = ((int)waitingBattleStartTime).ToString("D2");

        //バトルスタート時間を減らしていく
        if (PhotonNetwork.IsMasterClient)
        {
            waitingBattleStartTime -= Time.deltaTime;

            //バトル時間を同期する
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("StartTimeValue", RpcTarget.All, waitingBattleStartTime);
        }

        

        //MaxPlayerに達した段階で画面遷移、または時間が0になったとき
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] == PhotonNetwork.CurrentRoom.MaxPlayers || waitingBattleStartTime <= 0)
        {
            //入室できないようにする
            LobbyManager.UpdateRoomOptions(false);

            //イベントを受け取らないように設定
            PhotonNetwork.IsMessageQueueRunning = false;

            //画面遷移
            if (PhotonNetwork.IsMasterClient)
            {
                SceneManager.LoadScene("BattleScene");
            }
            
        }
    }

    //メニューボタンを押下した際の挙動
    public void OnClick_MenuButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移等
        WaitingPlayerCount_PhotonOff();
    }

    [PunRPC]
    //スタート時間を共有する
    private void StartTimeValue(float value)
    {
        //スタート時間を設定する
        waitingBattleStartTime = value;
    }

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            WaitingPlayerCount_PhotonOff();
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
        WaitingPlayerCount_PhotonOff();
    }

    //Photon接続解除や画面の遷移
    private void WaitingPlayerCount_PhotonOff()
    {
        //同じルーム内のWaitingRoomにいるプレイヤーの数を減らす
        var n = PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] is int value ? value : 0;
        PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] = n - 1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

        //画面遷移
        SceneManager.LoadScene("Menu");

        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }
    }
}

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

    //毎Updateによる取得人数
    private int updateWaitingPlayerCount = 0;

    //ランダム値の取得(ステージ)
    private int randomStage;

    //ニックネームを所持する
    private string WaitingPlayerNickName = "0";
    private string WaitingPlayer2NickName = "0";
    private string WaitingPlayer3NickName = "0";
    private string WaitingPlayer4NickName = "0";

    //スタート時間を設定する
    [SerializeField]
    private Text StartTimeText;
    private float waitingBattleStartTime;
    private float waitingBattleStartStackTime;

    //人数がMaxになったかの確認
    private bool WaitingRoomMaxPlayerFlag = false;

    //メッセージの送信に使用される
    new PhotonView photonView;
    PhotonView startTimePhotonView;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //メッセージの送信に使用される
        photonView = PhotonView.Get(this);
        startTimePhotonView = PhotonView.Get(this);

        //同じルーム内のWaitingRoomにいるプレイヤーの数を数える
        var n = PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] is int value ? value : 0;
        PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] = n + 1;
        //ステージを確定する
        if (PhotonNetwork.IsMasterClient)
        {
            randomStage = Random.Range(1, 4);
            PhotonNetwork.CurrentRoom.CustomProperties["DefinedStage"] = 1;
        }
        //反映
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["DefinedStage"]);

        //プレイヤー番号の決定
        PhotonNetwork.LocalPlayer.CustomProperties["playerCreatedNumber"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"];
        PhotonNetwork.LocalPlayer.SetCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);

        //キックされないように設定する
        //var prps = PhotonNetwork.LocalPlayer.CustomProperties;
        //prps["NoKick"] = "true";
        //PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

        //プレイヤーが入っていた時にバトルスタート時間を設定する
        waitingBattleStartTime = 12.0f;
        waitingBattleStartStackTime = 12.0f;

        //ルーム内のクライアントがMasterClientと同じシーンをロードするように設定
        PhotonNetwork.AutomaticallySyncScene = true;


    }

    // Update is called once per frame
    void Update()
    {
        //待機人数の表示
        WaitingPlayerCountText.text = "待機プレイヤー : " + PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"]  + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        //バトルスタート時間を表示する
        StartTimeText.text = ((int)waitingBattleStartTime).ToString("D2");

        //バトルスタート時間を減らしていく
        if (PhotonNetwork.IsMasterClient && waitingBattleStartStackTime > 0)
        {
            waitingBattleStartStackTime -= Time.deltaTime;

            //バトル時間を同期する
            startTimePhotonView.RPC("StartTimeValue", RpcTarget.AllViaServer, waitingBattleStartStackTime);
        }

        
        //部屋内の人数と毎Updateで取得している人数で差異があれば呼び出す
        if (PhotonNetwork.IsMasterClient)
        {
            //人数を毎Updateで取得しておく
            updateWaitingPlayerCount = 0;
            foreach (var p in PhotonNetwork.PlayerList)
            {
                //if ((string)p.CustomProperties["NoKick"] == "true")
                //{
                //}

                //人数を取得
                updateWaitingPlayerCount++;

                //ニックネームを取得
                if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] == PhotonNetwork.CurrentRoom.MaxPlayers || ((waitingBattleStartTime <= 2 || waitingBattleStartTime % 5.0f <= 1.0f) && waitingBattleStartTime > 0))
                {
                    if (updateWaitingPlayerCount == 1)
                    {
                        ////ニックネーム消去を共有する
                        //PhotonView photonView = PhotonView.Get(this);
                        //photonView.RPC("WaitingPlayerDeleteNickNameValue", RpcTarget.All);
                        p.CustomProperties["playerCreatedNumber"] = updateWaitingPlayerCount;
                        p.SetCustomProperties(p.CustomProperties);
                        //ニックネームを取得
                        //photonView.RPC("WaitingPlayerNickNameValue", RpcTarget.All, p.NickName);
                    }
                    else if (updateWaitingPlayerCount == 2)
                    {
                        p.CustomProperties["playerCreatedNumber"] = updateWaitingPlayerCount;
                        p.SetCustomProperties(p.CustomProperties);
                        //photonView.RPC("WaitingPlayer2NickNameValue", RpcTarget.All, p.NickName);
                    }
                    else if (updateWaitingPlayerCount == 3)
                    {
                        p.CustomProperties["playerCreatedNumber"] = updateWaitingPlayerCount;
                        p.SetCustomProperties(p.CustomProperties);
                        //photonView.RPC("WaitingPlayer3NickNameValue", RpcTarget.All, p.NickName);
                    }
                    else if (updateWaitingPlayerCount == 4)
                    {
                        p.CustomProperties["playerCreatedNumber"] = updateWaitingPlayerCount;
                        p.SetCustomProperties(p.CustomProperties);
                        //photonView.RPC("WaitingPlayer4NickNameValue", RpcTarget.All, p.NickName);

                    }
                }
            }

            //部屋内の人数表示変更
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] != updateWaitingPlayerCount)
            {
                PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] = updateWaitingPlayerCount;
                PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
            }
            
            ////プレイヤー番号の再取得
            //foreach (var p in PhotonNetwork.PlayerList)
            //{
            //    //if ((string)p.CustomProperties["NoKick"] == "true")
            //    //{
            //        //それぞれに番号をいれる
            //        if (p.NickName == WaitingPlayerNickName)
            //        {
            //            p.CustomProperties["playerCreatedNumber"] = updateWaitingPlayerCount;
            //            p.SetCustomProperties(p.CustomProperties);
            //            //次のプレイヤーはプレイヤー番号を1つ減らす
            //            updateWaitingPlayerCount--;
            //        }
            //        else if (p.NickName == WaitingPlayer2NickName)
            //        {
            //            p.CustomProperties["playerCreatedNumber"] = updateWaitingPlayerCount;
            //            p.SetCustomProperties(p.CustomProperties);
            //            //次のプレイヤーはプレイヤー番号を1つ減らす
            //            updateWaitingPlayerCount--;
            //        }
            //        else if (p.NickName == WaitingPlayer3NickName)
            //        {
            //            p.CustomProperties["playerCreatedNumber"] = updateWaitingPlayerCount;
            //            p.SetCustomProperties(p.CustomProperties);
            //            //次のプレイヤーはプレイヤー番号を1つ減らす
            //            updateWaitingPlayerCount--;
            //        }
            //        else if (p.NickName == WaitingPlayer4NickName)
            //        {
            //            p.CustomProperties["playerCreatedNumber"] = updateWaitingPlayerCount;
            //            p.SetCustomProperties(p.CustomProperties);
            //            //次のプレイヤーはプレイヤー番号を1つ減らす
            //            updateWaitingPlayerCount--;
            //        }

            //    //}
            //}
        }

        //人数により部屋をクローズする
        LobbyManager.UpdateRoomOptions(true);

        //時間が2以下になったとき
        if (waitingBattleStartTime <= 2.0f)
        {
            //入室できないようにする
            LobbyManager.UpdateRoomOptions(false);

            //if (PhotonNetwork.IsMasterClient)
            //{
            //    //このシーンにいないプレイヤーをキックする
            //    foreach (var p in PhotonNetwork.PlayerList)
            //    {
            //        if ((string)p.CustomProperties["NoKick"] != "true")
            //        {
            //            PhotonNetwork.CloseConnection(p);
            //        }
            //    }
            //}


        }

        //時間が0になったとき
        if (waitingBattleStartTime <= 0)
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
            else
            {
                SceneManager.LoadScene("BattleScene");
            }
        }

        //MaxPlayerに達した時
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            //残り3秒にする
            if (PhotonNetwork.IsMasterClient && waitingBattleStartStackTime > 3.0f && WaitingRoomMaxPlayerFlag == false)
            {
                waitingBattleStartStackTime = 3.0f;

                //バトル時間を同期する
                startTimePhotonView.RPC("StartTimeValue", RpcTarget.AllViaServer, waitingBattleStartStackTime);

                //1度のみ実行するようにする
                photonView.RPC("WaitingRoomMaxPlayerFlagValue", RpcTarget.All, true);
            }
        }
    }

    [PunRPC]
    //スタート時間を共有する
    private void StartTimeValue(float value)
    {
        //スタート時間を設定する
        waitingBattleStartTime = value;
    }

    [PunRPC]
    //ニックネームを共有する
    private void WaitingPlayerNickNameValue(string value)
    {
        //ニックネームを設定する
        WaitingPlayerNickName = value;
    }
    [PunRPC]
    //ニックネームを共有する
    private void WaitingPlayer2NickNameValue(string value)
    {
        //ニックネームを設定する
        WaitingPlayer2NickName = value;
    }
    [PunRPC]
    //ニックネームを共有する
    private void WaitingPlayer3NickNameValue(string value)
    {
        //ニックネームを設定する
        WaitingPlayer3NickName = value;
    }
    [PunRPC]
    //ニックネームを共有する
    private void WaitingPlayer4NickNameValue(string value)
    {
        //ニックネームを設定する
        WaitingPlayer4NickName = value;
    }

    [PunRPC]
    //ニックネーム消去を共有する
    private void WaitingPlayerDeleteNickNameValue()
    {
        //ニックネーム設定を消去する
        WaitingPlayerNickName = "0";
        WaitingPlayer2NickName = "0";
        WaitingPlayer3NickName = "0";
        WaitingPlayer4NickName = "0";
    }

    [PunRPC]
    //人数がMaxになったかを確認する
    private void WaitingRoomMaxPlayerFlagValue(bool value)
    {
        //生成されたプレイヤーの数を1度のみ取得する
        WaitingRoomMaxPlayerFlag = value;
    }

    //メニューボタンを押下した際の挙動
    public void OnClick_MenuButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //次回もキック確認されるように設定する
        var prps = PhotonNetwork.LocalPlayer.CustomProperties;
        //prps["NoKick"] = "false";
        prps["playerCreatedNumber"] = null;
        PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

        //画面遷移等(0.5秒後)
        Invoke("WaitingPlayerCount_PhotonOff", 0.5f);
    }

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //次回もキック確認されるように設定する
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            //prps["NoKick"] = "false";
            prps["playerCreatedNumber"] = null;
            PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

            //Photonに接続を解除する
            if (PhotonNetwork.IsConnected == true)
            {
                PhotonNetwork.Disconnect();
            }

            //画面遷移等(0.5秒後)
            Invoke("WaitingPlayerCount_PhotonOff", 0.5f);
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
        //次回もキック確認されるように設定する
        var prps = PhotonNetwork.LocalPlayer.CustomProperties;
        //prps["NoKick"] = "false";
        prps["playerCreatedNumber"] = null;
        PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

        //画面遷移等(0.5秒後)
        Invoke("WaitingPlayerCount_PhotonOff", 0.5f);
    }

    //Photon接続解除や画面の遷移
    private void WaitingPlayerCount_PhotonOff()
    {
        //画面遷移
        SceneManager.LoadScene("Menu");

    }

    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(150, 30, 150, 70), "番号1 : " + WaitingPlayerNickName);
        //GUI.TextField(new Rect(350, 30, 150, 70), "番号2 : " + WaitingPlayer2NickName);
        //GUI.TextField(new Rect(550, 30, 150, 70), "番号3 : " + WaitingPlayer3NickName);
        //GUI.TextField(new Rect(750, 30, 150, 70), "番号4 : " + WaitingPlayer4NickName);

        //GUI.TextField(new Rect(150, 150, 150, 70), "番号1 : " + PhotonNetwork.LocalPlayer.CustomProperties["playerCreatedNumber"]);
    }
}

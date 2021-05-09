using System;
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
#if UNITY_IOS
    //AdMobWaitingRoomAdvertisingのpublic定数を取得
    AdMobWaitingRoomAdvertising adMobWaitingRoomAdvertising;
#endif

    //待機人数の表示
    [SerializeField]
    private Text WaitingPlayerCountText;
    //メニューボタンのゲームオブジェクト
    [SerializeField]
    private GameObject MenuButton;
    //オンライン待機ボタンのゲームオブジェクトとボタン設定
    [SerializeField]
    private GameObject MenuWaitingOnlineButtonGameObject;
    [SerializeField]
    private Button MenuWaitingOnlineButton;
    
    //ルームマスター退出時のフラグ
    public static bool RoomMasterLeftFlag = false;

    //毎Updateによる取得人数
    private int updateWaitingPlayerCount = 0;

    //ランダム値の取得(ステージ、背景)
    private int randomStage;
    private int randomBackground;
    //ステージ、背景リストの初期化
    private List<int> stageList = new List<int>();
    private List<int> backgroundList = new List<int>();

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
#if UNITY_IOS
        //AdMobWaitingRoomAdvertisingのpublic定数を取得
        adMobWaitingRoomAdvertising = GameObject.Find("WaitingRoomAdvertising").GetComponent<AdMobWaitingRoomAdvertising>();
#endif

        //FPSを60に設定
        Application.targetFrameRate = 60;

        //メッセージの送信に使用される
        photonView = PhotonView.Get(this);
        startTimePhotonView = PhotonView.Get(this);

        //プレイヤーが入っていた時にバトルスタート時間を設定する
        waitingBattleStartTime = 10.0f;
        waitingBattleStartStackTime = 10.0f;

        //オンライン待機中だった場合はここで抜ける
        if (MenuWaitingOnline.menuWaitingOnlineFlag == true) return;


        //同じルーム内のWaitingRoomにいるプレイヤーの数を数える
        var n = PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] is int value ? value : 0;
        PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] = n + 1;
        //ステージ、背景を確定する
        StageList();
        BackgroundList();
        if (PhotonNetwork.IsMasterClient)
        {
            //ステージ
            randomStage = UnityEngine.Random.Range(0, stageList.Count);
            PhotonNetwork.CurrentRoom.CustomProperties["DefinedStage"] = stageList[randomStage];
            //背景
            randomBackground = UnityEngine.Random.Range(0, backgroundList.Count);
            PhotonNetwork.CurrentRoom.CustomProperties["DefinedBackground"] = backgroundList[randomBackground];
        }
        //反映
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

        //プレイヤー番号の決定
        PhotonNetwork.LocalPlayer.CustomProperties["playerCreatedNumber"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"];
        PhotonNetwork.LocalPlayer.SetCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);

        //キックされないように設定する
        //var prps = PhotonNetwork.LocalPlayer.CustomProperties;
        //prps["NoKick"] = "true";
        //PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

        //ルーム内のクライアントがMasterClientと同じシーンをロードするように設定
        //PhotonNetwork.AutomaticallySyncScene = true;


    }

    // Update is called once per frame
    void Update()
    {
        //待機人数の表示
        WaitingPlayerCountText.text = "待機プレイヤー : " + PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"]  + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

        //オンライン待機ボタンの押下設定
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] >= 2)
        {
            MenuWaitingOnlineButton.interactable = false;
            MenuWaitingOnline.menuWaitingOnlineFlag = false;
        }
        else { MenuWaitingOnlineButton.interactable = true; }

        //バトルスタート時間を減らしていく
        if (PhotonNetwork.IsMasterClient && waitingBattleStartStackTime > 0 && (int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] >= 2)
        {
            waitingBattleStartStackTime -= Time.deltaTime;

            //バトル時間を同期する
            startTimePhotonView.RPC("StartTimeValue", RpcTarget.AllViaServer, waitingBattleStartStackTime);
        }
        else if (PhotonNetwork.IsMasterClient && waitingBattleStartStackTime > 0 && (int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] < 2)
        {
            waitingBattleStartStackTime = 10.0f;

            //バトル時間を同期する
            startTimePhotonView.RPC("StartTimeValue", RpcTarget.AllViaServer, waitingBattleStartStackTime);
        }

        //バトルスタート時間の表示
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] >= 2)
        {
            //バトルスタート時間を表示する
            StartTimeText.text = ((int)waitingBattleStartTime).ToString("D2");
        }
        else
        {
            //バトルスタート時間を表示しない
            StartTimeText.text = "";
        }

        //部屋内の人数と毎Updateで取得している人数で差異があれば呼び出す
        if (PhotonNetwork.IsMasterClient)
        {
            //人数を毎Updateで取得しておく
            updateWaitingPlayerCount = 0;
            foreach (var p in PhotonNetwork.PlayerList)
            {
                //人数を取得
                updateWaitingPlayerCount++;

                //ニックネームを取得
                if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] == PhotonNetwork.CurrentRoom.MaxPlayers || ((waitingBattleStartTime <= 2 || waitingBattleStartTime % 5.0f <= 1.0f) && waitingBattleStartTime > 0))
                {
                    for (int i = 1; i < 5; i++)
                    {
                        if (updateWaitingPlayerCount == i)
                        {
                            ////ニックネーム消去を共有する
                            //PhotonView photonView = PhotonView.Get(this);
                            //photonView.RPC("WaitingPlayerDeleteNickNameValue", RpcTarget.All);
                            p.CustomProperties["playerCreatedNumber"] = updateWaitingPlayerCount;
                            p.SetCustomProperties(p.CustomProperties);
                            //ニックネームを取得
                            //photonView.RPC("WaitingPlayerNickNameValue", RpcTarget.All, p.NickName);
                        }
                    }
                }
            }

            //部屋内の人数表示変更
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] != updateWaitingPlayerCount)
            {
                PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] = updateWaitingPlayerCount;
                PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
            }
        }

        //時間が2秒より大きいとき
        if (waitingBattleStartTime > 2.0f)
        {
            //人数により部屋をクローズする
            LobbyManager.UpdateRoomOptions(true);
            //メニューボタンを表示する
            MenuButton.SetActive(true);
            //オンライン待機ボタンを表示する
            MenuWaitingOnlineButtonGameObject.SetActive(true);
        }
        

        //時間が2以下になったとき
        if (waitingBattleStartTime <= 2.0f)
        {
#if UNITY_IOS
            //広告解除していない場合
            if (PlayerPrefs.GetInt("Unlock_WaitingRoomAdvertising") == 0 && adMobWaitingRoomAdvertising.bannerView != null)
            {
                adMobWaitingRoomAdvertising.bannerView.Hide();
                adMobWaitingRoomAdvertising.bannerView.Destroy();
            }
#endif

            //入室できないようにする
            LobbyManager.UpdateRoomOptions(false);
            //メニューボタンを非表示にする
            MenuButton.SetActive(false);
            //オンライン待機ボタンを非表示にする
            MenuWaitingOnlineButtonGameObject.SetActive(false);

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

        //ルームマスターが退出した時
        if (RoomMasterLeftFlag == true)
        {
#if UNITY_IOS
            //広告解除していない場合
            if (PlayerPrefs.GetInt("Unlock_WaitingRoomAdvertising") == 0 && adMobWaitingRoomAdvertising.bannerView != null)
            {
                adMobWaitingRoomAdvertising.bannerView.Hide();
                adMobWaitingRoomAdvertising.bannerView.Destroy();
            }
#endif
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

    [PunRPC]
    //ルームマスターが退出したかを確認する
    private void RoomMasterLeftFlagValue(bool value)
    {
        //ルームマスターが退出したかを確認する
        RoomMasterLeftFlag = value;
    }

    //ステージリスト
    private void StageList()
    {
        //ステージ1~3
        stageList.Add(1);
        stageList.Add(2);
        stageList.Add(3);
        //追加ステージ
        //if (PlayerPrefs.GetInt("Unlock_Stage4") == 1) stageList.Add(4);
        if (PlayerPrefs.GetInt("Unlock_Stage5") == 1) stageList.Add(5);
        if (PlayerPrefs.GetInt("Unlock_Stage6") == 1) stageList.Add(6);

    }

    //背景リスト
    private void BackgroundList()
    {
        DateTime now = DateTime.Now;

        backgroundList.Add(1);
        backgroundList.Add(2);
        if ((now.Hour >= 0 && now.Hour <= 6) || (now.Hour >= 18 && now.Hour <= 24)) backgroundList.Add(3);
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

#if UNITY_IOS
        //広告解除していない場合
        if (PlayerPrefs.GetInt("Unlock_WaitingRoomAdvertising") == 0 && adMobWaitingRoomAdvertising.bannerView != null)
        {
            adMobWaitingRoomAdvertising.bannerView.Hide();
            adMobWaitingRoomAdvertising.bannerView.Destroy();
        }
#endif

        //ルームマスターが退出したことを確認する
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RoomMasterLeftFlagValue", RpcTarget.All, true);
        }

        //画面遷移等(0.5秒後)
        Invoke("WaitingPlayerCount_PhotonOff", 0.5f);
    }

    //オンライン待機ボタンを押した時
    public void OnClick_MenuWaitingOnlineButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
#if UNITY_IOS
        //広告解除していない場合
        if (PlayerPrefs.GetInt("Unlock_WaitingRoomAdvertising") == 0 && adMobWaitingRoomAdvertising.bannerView != null)
        {
            adMobWaitingRoomAdvertising.bannerView.Hide();
            adMobWaitingRoomAdvertising.bannerView.Destroy();
        }
#endif
        //オンライン待機フラグを立てる
        MenuWaitingOnline.menuWaitingOnlineFlag = true;
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

#if UNITY_IOS
            //広告解除していない場合
            if (PlayerPrefs.GetInt("Unlock_WaitingRoomAdvertising") == 0)
            {
                adMobWaitingRoomAdvertising.bannerView.Hide();
                adMobWaitingRoomAdvertising.bannerView.Destroy();
            }
#endif

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

#if UNITY_IOS
        //広告解除していない場合
        if (PlayerPrefs.GetInt("Unlock_WaitingRoomAdvertising") == 0)
        {
            adMobWaitingRoomAdvertising.bannerView.Hide();
            adMobWaitingRoomAdvertising.bannerView.Destroy();
        }
#endif

        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }

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

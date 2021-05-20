using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class MenuUI : MonoBehaviour
{
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;
    //UserAuthのスクリプトの関数使用
    UserAuth userAuth;
    //CheckTodayスクリプトの関数使用
    CheckTody checkTody;

    //動物の表示用
    private GameObject menuAnimal;

    //ランキングデータ
    private RankingData rankingData = new RankingData();

    //プレイ数のテキスト表示
    [SerializeField]
    private Text PlayCountText;

    //オンラインのボタン設定
    [SerializeField]
    private Button OnlineButton;

    //WinCountRankingPanelの表示
    [SerializeField]
    private GameObject WinCountRankingPanel;
    //OfflineRankingChildPanelの表示
    [SerializeField]
    private GameObject OfflineRankingChildPanel;
    //WinCountRankingChildPanelの表示
    [SerializeField]
    private GameObject WinCountRankingChildPanel;

    //Win数のランキングテキスト表示
    [SerializeField]
    public Text WinCountRankingNameText;
    //アニマル名の表示
    [SerializeField]
    private Text OfflineRankingAnimalNameText;
    //オフラインタイムランキングユーザー名表示
    [SerializeField]
    public Text OfflineRankingUsernameText;
    //オフラインランキングハイスコア表示
    [SerializeField]
    public Text OfflineRankingHighScoreText;
    //オンラインタイムランキングユーザー名表示
    [SerializeField]
    public Text OnlineRankingUsernameText;
    //オンラインランキング勝利数表示
    [SerializeField]
    public Text OnlineRankingWinCountText;

    //コインの枚数を表示
    [SerializeField]
    private Text MyCoinText;

    //ロビーマネジャーのゲームオブジェクト
    [SerializeField]
    private GameObject LobbyManager;

    //タイムモード用ボタン
    [SerializeField]
    private Button OfflineTimeButton;
    //テッペンモード用ボタン
    [SerializeField]
    private Button OfflineTeppenButton;
    //モード初期値
    private static string offlineMode = "Time";

    //ダイアログ表示
    //ネットワークパネルを表示する
    [SerializeField]
    private GameObject NotNetworkPanel;
    //MaxPlayerPanelの表示
    [SerializeField]
    private GameObject MaxPlayerPanel;
    //RoomMasterLeftPanelの表示
    [SerializeField]
    private GameObject RoomMasterLeftPanel;
    //謝罪等用メッセージパネルの表紙
    [SerializeField]
    private GameObject MessagePanel;
    [SerializeField]
    private GameObject Message2Panel;

    //ドキュメントの表示
    //オフラインモードの説明
    [SerializeField]
    private GameObject OfflineExplainPanel;
    [SerializeField]
    private Text OfflineExplainText;
    [SerializeField]
    private TextAsset OfflineExplainTextAsset;

    //全動物のタイム合計
    private int bestTime_Total;



    // Start is called before the first frame update
    void Start()
    {

        //回転可能にする
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;

        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //UserAuthのスクリプトの関数使用
        userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();
        //CheckTodayスクリプトの関数使用
        checkTody = this.gameObject.GetComponent<CheckTody>();

        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true && MenuWaitingOnline.menuWaitingOnlineFlag == false)
        {
            PhotonNetwork.Disconnect();
        }

        //ルーム内のクライアントがMasterClientと同じシーンをロードしないように設定
        PhotonNetwork.AutomaticallySyncScene = false;

        //テッペンの階数をデバイスに保存
        if (!PlayerPrefs.HasKey("TeppenFloor")) PlayerPrefs.SetInt("TeppenFloor", 1);
        if (!PlayerPrefs.HasKey("BestTeppenFloor")) PlayerPrefs.SetInt("BestTeppenFloor", 1);

        //ベストタイムスコアを取得
        SelectCharacterBestTimeGet();

        //オフラインで記録できなかったベストタイムをオンライン時にいれる
        SelectCharacterBestTimeSet();

        //オフラインで記録できなかったベストテッペンフロアをいれる
        BestTeppenFloorSet();

        //モード初期値
        if (offlineMode == "Time") OfflineTimeButton.interactable = false;
        else if(offlineMode == "Teppen") OfflineTeppenButton.interactable = false;

       

        //アニマルの初期設定を入れる
        if (SelectCharacterUI.animalName == null)
        {
            SelectCharacterUI.animalName = "Dog";
            SelectCharacterUI.animalName_Color = "Dog(N)";
        }

        //選択された動物の表示
        if (SelectCharacterUI.animalName == "Dog")
        {
            menuAnimal = (GameObject)Instantiate(Resources.Load("Menu/Dog"), new Vector3(1.0f, 0, -7.0f), Quaternion.Euler(0.0f, 180.0f, 0.0f));
        }
        else if (SelectCharacterUI.animalName == "Giraffe")
        {
            menuAnimal = (GameObject)Instantiate(Resources.Load("Menu/Giraffe"), new Vector3(2.0f, -1.0f, -5.0f), Quaternion.Euler(0.0f, 180.0f, 0.0f));
        }
        else if (SelectCharacterUI.animalName == "Elephant")
        {
            menuAnimal = (GameObject)Instantiate(Resources.Load("Menu/Elephant"), new Vector3(1.5f, -0.1f, -6.0f), Quaternion.Euler(0.0f, 180.0f, 0.0f));
        }
        else if (SelectCharacterUI.animalName == "Tiger")
        {
            menuAnimal = (GameObject)Instantiate(Resources.Load("Menu/Tiger"), new Vector3(1.1f, 0, -7.0f), Quaternion.Euler(0.0f, 180.0f, 0.0f));
        }
        else if (SelectCharacterUI.animalName == "Cat")
        {
            menuAnimal = (GameObject)Instantiate(Resources.Load("Menu/Cat"), new Vector3(2.0f, -1.0f, -4.0f), Quaternion.Euler(0.0f, 180.0f, 0.0f));
        }
        else if (SelectCharacterUI.animalName == "Rabbit")
        {
            menuAnimal = (GameObject)Instantiate(Resources.Load("Menu/Rabbit"), new Vector3(2.0f, -1.0f, -5.5f), Quaternion.Euler(0.0f, 180.0f, 0.0f));
        }

        //ルームマスターが退出し追い出されたことをダイアログで表示
        if (WaitingPlayerCount.RoomMasterLeftFlag == true)
        {
            RoomMasterLeftPanel.SetActive(true);
            //再度falseに戻しておく
            WaitingPlayerCount.RoomMasterLeftFlag = false;
        }

        //メッセージの表示と日付の確認
        if (!PlayerPrefs.HasKey("MessageVersion")) PlayerPrefs.SetInt("MessageVersion", 0);
        if (PlayerPrefs.GetInt("MessageVersion") <= 0)
        {
            if (PlayerPrefs.GetString("NickName") == "SINVINO@chimolove(6177)") MessagePanel.SetActive(true);
            else { Message2Panel.SetActive(true); }
        }
        checkTody.CheckToday();
    }

    // Update is called once per frame
    void Update()
    {

        //デバイスに保持されているコインの枚数を表示
        if (!PlayerPrefs.HasKey("myCoin")) PlayerPrefs.SetInt("myCoin", 0);
        else { MyCoinText.text = PlayerPrefs.GetInt("myCoin").ToString(""); }

        //デバイスに保持されているプレイ数の表示
        if (!PlayerPrefs.HasKey("PlayCount")) PlayerPrefs.SetInt("PlayCount", 0);
        else { PlayCountText.text = PlayerPrefs.GetInt("PlayCount").ToString(""); }

        //デバイスに保持されているWin数情報を取得
        if (!PlayerPrefs.HasKey("WinCount")) PlayerPrefs.SetInt("WinCount", 0);
        //デバイスに保持されているUnlockキャラクター情報を取得
        //0：アンロックされていない
        //1：アンロックされている
        //キリン
        if (!PlayerPrefs.HasKey("Unlock_Giraffe")) PlayerPrefs.SetInt("Unlock_Giraffe", 0);
        //象
        if (!PlayerPrefs.HasKey("Unlock_Elephant")) PlayerPrefs.SetInt("Unlock_Elephant", 0);
        //虎
        if (!PlayerPrefs.HasKey("Unlock_Tiger")) PlayerPrefs.SetInt("Unlock_Tiger", 0);
        //猫
        if (!PlayerPrefs.HasKey("Unlock_Cat")) PlayerPrefs.SetInt("Unlock_Cat", 0);
        //ウサギ
        if (!PlayerPrefs.HasKey("Unlock_Rabbit")) PlayerPrefs.SetInt("Unlock_Rabbit", 0);
        //スキン
        //キャンディ
        if (!PlayerPrefs.HasKey("Unlock_Candy")) PlayerPrefs.SetInt("Unlock_Candy", 0);
        //王冠
        if (!PlayerPrefs.HasKey("Unlock_Crown")) PlayerPrefs.SetInt("Unlock_Crown", 0);
        //雲
        if (!PlayerPrefs.HasKey("Unlock_Cloud")) PlayerPrefs.SetInt("Unlock_Cloud", 0);
        //マップピン
        if (!PlayerPrefs.HasKey("Unlock_Mappin")) PlayerPrefs.SetInt("Unlock_Mappin", 0);
        //クリスタル
        if (!PlayerPrefs.HasKey("Unlock_Crystal")) PlayerPrefs.SetInt("Unlock_Crystal", 0);
        //ステージ
        //ステージ4
        if (!PlayerPrefs.HasKey("Unlock_Stage4")) PlayerPrefs.SetInt("Unlock_Stage4", 0);
        if (!PlayerPrefs.HasKey("Unlock_Stage4_ON")) PlayerPrefs.SetString("Unlock_Stage4_ON","false");
        //ステージ5
        if (!PlayerPrefs.HasKey("Unlock_Stage5")) PlayerPrefs.SetInt("Unlock_Stage5", 0);
        if (!PlayerPrefs.HasKey("Unlock_Stage5_ON")) PlayerPrefs.SetString("Unlock_Stage5_ON", "false");
        //ステージ6
        if (!PlayerPrefs.HasKey("Unlock_Stage6")) PlayerPrefs.SetInt("Unlock_Stage6", 0);
        if (!PlayerPrefs.HasKey("Unlock_Stage6_ON")) PlayerPrefs.SetString("Unlock_Stage6_ON", "false");

        //オンライン待機中にボタン押せないようにする
        if (MenuWaitingOnline.menuWaitingOnlineFlag == true) OnlineButton.interactable = false;
        else { OnlineButton.interactable = true; }
    }

    

    //ベストタイムスコアを取得
    private void SelectCharacterBestTimeGet()
    {
        if (!PlayerPrefs.HasKey("BestTime_Giraffe") || !PlayerPrefs.HasKey("BestTime_Elephant") || !PlayerPrefs.HasKey("BestTime_Dog") ||
            !PlayerPrefs.HasKey("BestTime_Tiger") || !PlayerPrefs.HasKey("BestTime_Cat") || !PlayerPrefs.HasKey("BestTime_Rabbit") || !PlayerPrefs.HasKey("BestTime_Total"))
        {
            //mobile backendに接続しベストタイムを初期登録する
            userAuth.firstSetBestTime(false);
        }
        
    }

    //オフラインで記録できなかったベストタイムをオンライン時にいれる
    private void SelectCharacterBestTimeSet()
    {
        //ネットワーク接続確認
        //インターネット接続なし
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
        }
        //インターネット接続あり
        else
        {
            //キリン
            if (PlayerPrefs.GetInt("bestTimeRecode_Giraffe") == 1)
            {
                //サーバにオフラインハイスコアを保存
                userAuth.save_Offline("Giraffe");
                //ランキングに登録完了したら0にする
                PlayerPrefs.SetInt("bestTimeRecode_Giraffe", 0);
            }
            //ゾウ
            if (PlayerPrefs.GetInt("bestTimeRecode_Elephant") == 1)
            {
                //サーバにオフラインハイスコアを保存
                userAuth.save_Offline("Elephant");
                //ランキングに登録完了したらfalseにする
                PlayerPrefs.SetInt("bestTimeRecode_Elephant", 0);
            }
            //犬
            if (PlayerPrefs.GetInt("bestTimeRecode_Dog") == 1)
            {
                //サーバにオフラインハイスコアを保存
                userAuth.save_Offline("Dog");
                //ランキングに登録完了したらfalseにする
                PlayerPrefs.SetInt("bestTimeRecode_Dog", 0);
            }
            //虎
            if (PlayerPrefs.GetInt("bestTimeRecode_Tiger") == 1)
            {
                //サーバにオフラインハイスコアを保存
                userAuth.save_Offline("Tiger");
                //ランキングに登録完了したらfalseにする
                PlayerPrefs.SetInt("bestTimeRecode_Tiger", 0);
            }
            //猫
            if (PlayerPrefs.GetInt("bestTimeRecode_Cat") == 1)
            {
                //サーバにオフラインハイスコアを保存
                userAuth.save_Offline("Cat");
                //ランキングに登録完了したらfalseにする
                PlayerPrefs.SetInt("bestTimeRecode_Cat", 0);
            }
            //ウサギ
            if (PlayerPrefs.GetInt("bestTimeRecode_Rabbit") == 1)
            {
                //サーバにオフラインハイスコアを保存
                userAuth.save_Offline("Rabbit");
                //ランキングに登録完了したらfalseにする
                PlayerPrefs.SetInt("bestTimeRecode_Rabbit", 0);
            }

            //総合
            bestTime_Total = PlayerPrefs.GetInt("BestTime_Giraffe") + PlayerPrefs.GetInt("BestTime_Elephant") + PlayerPrefs.GetInt("BestTime_Dog") +
                PlayerPrefs.GetInt("BestTime_Tiger") + PlayerPrefs.GetInt("BestTime_Cat") + PlayerPrefs.GetInt("BestTime_Rabbit");
            if (PlayerPrefs.GetInt("BestTime_Total") < bestTime_Total)
            {
                //トータルの時間更新
                PlayerPrefs.SetInt("BestTime_Total", bestTime_Total);
                //サーバにオフラインハイスコアを保存
                userAuth.save_Offline("Total");
            }

            PlayerPrefs.Save();
        }
    }

    private void BestTeppenFloorSet()
    {
        //ネットワーク接続確認
        //インターネット接続なし
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
        }
        //インターネット接続あり
        else
        {
        }

    }

    //LoginBounusYesButtonボタンを押した時の挙動
    public void OnClick_LoginBounusYesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //50コイン獲得する
        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") + checkTody.getLoginBounusCoin);
        //ログインボーナスパネルを非表示にする
        checkTody.LoginBounusPanel.SetActive(false);

        //オンラインtop30
        userAuth.TopRankers();
        //オフラインtop15
        OfflineRankingAnimalNameText.text = "Total";
        userAuth.TopOfflineRankers("Total");
        userAuth.TopOfflineRankers("Giraffe");
        userAuth.TopOfflineRankers("Elephant");
        userAuth.TopOfflineRankers("Dog");
        userAuth.TopOfflineRankers("Tiger");
        userAuth.TopOfflineRankers("Cat");
        userAuth.TopOfflineRankers("Rabbit");

        //非同期処理呼び出し(ランキング情報表示準備)
        StartCoroutine(prepareRankingData());
    }

    //オフラインランキングのアニマル切り替えボタン(右)
    public void OnClick_OfflineRankingAnimalNameRightButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        string nextAnimalName = "";

        //総合→キリン→象→犬→虎→猫→ウサギ
        if (OfflineRankingAnimalNameText.text == "Total")
        {
            nextAnimalName = "Giraffe";
        }
        else if (OfflineRankingAnimalNameText.text == "Giraffe")
        {
            nextAnimalName = "Elephant";
        }
        else if (OfflineRankingAnimalNameText.text == "Elephant")
        {
            nextAnimalName = "Dog";
        }
        else if (OfflineRankingAnimalNameText.text == "Dog")
        {
            nextAnimalName = "Tiger";
        }
        else if (OfflineRankingAnimalNameText.text == "Tiger")
        {
            nextAnimalName = "Cat";
        }
        else if (OfflineRankingAnimalNameText.text == "Cat")
        {
            nextAnimalName = "Rabbit";
        }
        else if (OfflineRankingAnimalNameText.text == "Rabbit")
        {
        }

        //ランキングデータの表示
        displayOfflineRankingData(nextAnimalName);
    }

    //オフラインランキングのアニマル切り替えボタン(左)
    public void OnClick_OfflineRankingAnimalNameLeftButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        string nextAnimalName = "";

        //総合→キリン←象←犬←虎←猫←ウサギ
        if (OfflineRankingAnimalNameText.text == "Total")
        {
        }
        else if (OfflineRankingAnimalNameText.text == "Giraffe")
        {
            nextAnimalName = "Total";
        }
        else if (OfflineRankingAnimalNameText.text == "Elephant")
        {
            nextAnimalName = "Giraffe";
        }
        else if (OfflineRankingAnimalNameText.text == "Dog")
        {
            nextAnimalName = "Elephant";
        }
        else if (OfflineRankingAnimalNameText.text == "Tiger")
        {
            nextAnimalName = "Dog";
        }
        else if (OfflineRankingAnimalNameText.text == "Cat")
        {
            nextAnimalName = "Tiger";
        }
        else if (OfflineRankingAnimalNameText.text == "Rabbit")
        {
            nextAnimalName = "Cat";
        }

        //ランキングデータの表示
        displayOfflineRankingData(nextAnimalName);
    }

    //ランキングのRankingOfflineButton押下時
    public void OnClick_RankingOfflineButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //パネルの表示非表示
        OfflineRankingChildPanel.SetActive(true);
        WinCountRankingChildPanel.SetActive(false);
    }

    //ランキングのRankingOnlineButton押下時
    public void OnClick_RankingOnlineButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        displayOnlineRankingData();

        //パネルの表示非表示
        OfflineRankingChildPanel.SetActive(false);
        WinCountRankingChildPanel.SetActive(true);
    }


    //WinCountRankingYesButtonボタンを押した時の挙動
    public void OnClick_WinCountRankingYesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //ランキングパネルを非表示にする
        WinCountRankingPanel.SetActive(false);
    }

    //UnityIAPMoveボタンを押した時の挙動
    public void OnClick_UnityIAPMoveButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("UnityIAP");
    }

    //オフラインボタンを押した際の挙動
    public void OnClick_OfflineButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //画面回転の制御
        if(Screen.width > Screen.height) Screen.autorotateToPortrait = false;
        else
        {
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToLandscapeLeft = false;
        }

        //画面遷移
        if (offlineMode == "Time") SceneManager.LoadScene("WaitingRoom(offline)");
        else if(offlineMode == "Teppen") SceneManager.LoadScene("TeppenMenu");

    }

    //オフライン(タイム)ボタンを押した時
    public void OnClick_OfflineTimeButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //モード変更
        offlineMode = "Time";
        //色の変更
        OfflineTimeButton.interactable = false;
        OfflineTeppenButton.interactable = true;
    }

    //オフライン(テッペン)ボタンを押した時
    public void OnClick_OfflineTeppenButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //モード変更
        offlineMode = "Teppen";
        //色の変更
        OfflineTimeButton.interactable = true;
        OfflineTeppenButton.interactable = false;
    }

    //オフラインのモード説明ボタンを押した時
    public void OnClick_OfflineInformationButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //パネル表示
        OfflineExplainPanel.SetActive(true);
        //ドキュメント取得
        OfflineExplainText.text = OfflineExplainTextAsset.text;
    }

    //オフラインのモード説明ボタンをクローズした時
    public void OnClick_OfflineExplainCloseButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //パネル非表示
        OfflineExplainPanel.SetActive(false);
    }

    //オンラインボタンを押した際の挙動
    public void OnClick_OnlineButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //ネットワーク接続確認
        //インターネット接続なし
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            NotNetworkPanel.SetActive(true);
        }
        //インターネット接続あり
        else
        {
            //画面回転の制御
            if (Screen.width > Screen.height) Screen.autorotateToPortrait = false;
            else
            {
                Screen.autorotateToLandscapeRight = false;
                Screen.autorotateToLandscapeLeft = false;
            }

            //ロビーマネジャーのゲームオブジェクトをオンにする
            LobbyManager.SetActive(true);
        }
    }

    //アンロックボタンを押した際の挙動
    public void OnClick_UnlockButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("Unlock");
    }

    //アニマルボタンを押した際
    public void OnClick_SelectAnimalButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("SelectCharacter");
    }

    //ランキングボタンを押した際
    public void OnClick_RankingButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //ランキングデータの消去
        if (rankingData.offlineRankingData["Total"] != null) rankingData.offlineRankingData["Total"].Clear();
        if (rankingData.offlineRankingData["Giraffe"] != null) rankingData.offlineRankingData["Giraffe"].Clear();
        if (rankingData.offlineRankingData["Elephant"] != null) rankingData.offlineRankingData["Elephant"].Clear();
        if (rankingData.offlineRankingData["Dog"] != null) rankingData.offlineRankingData["Dog"].Clear();
        if (rankingData.offlineRankingData["Tiger"] != null) rankingData.offlineRankingData["Tiger"].Clear();
        if (rankingData.offlineRankingData["Cat"] != null) rankingData.offlineRankingData["Cat"].Clear();
        if (rankingData.offlineRankingData["Rabbit"] != null) rankingData.offlineRankingData["Rabbit"].Clear();
        if (rankingData.onlineRankingData != null) rankingData.onlineRankingData.Clear();

        //オンラインtop30
        userAuth.TopRankers();
        //オフラインtop15
        OfflineRankingAnimalNameText.text = "Total";
        userAuth.TopOfflineRankers("Total");
        userAuth.TopOfflineRankers("Giraffe");
        userAuth.TopOfflineRankers("Elephant");
        userAuth.TopOfflineRankers("Dog");
        userAuth.TopOfflineRankers("Tiger");
        userAuth.TopOfflineRankers("Cat");
        userAuth.TopOfflineRankers("Rabbit");

        //非同期処理呼び出し(ランキング情報表示準備)
        StartCoroutine(prepareRankingData());
    }

    //MessageYesButtonボタン押下時
    public void OnClick_MessageYesButton()
    {
        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") + 1000);
        PlayerPrefs.SetInt("BestTime_Dog", 0);
        PlayerPrefs.SetInt("MessageVersion", 1);
        MessagePanel.SetActive(false);
    }

    //Message2YesButtonボタン押下時
    public void OnClick_Message2YesButton()
    {
        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") + 500);
        PlayerPrefs.SetInt("MessageVersion", 1);
        Message2Panel.SetActive(false);
    }

    //なんらかのダイアログのYesButtonボタンを押した時の挙動
    public void OnClick_DialogYesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //パネルを非表示にする
        NotNetworkPanel.SetActive(false);
        MaxPlayerPanel.SetActive(false);
        RoomMasterLeftPanel.SetActive(false);
    }

    //タイトルボタンを押した際の挙動
    public void OnClick_TitleButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("Title");
    }

    //オフライン用ランキングデータに情報を突っ込む
    public void SetOfflineRankingInfo(string animal, string userName, string highScore)
    {
        OfflineRankingElement data = new OfflineRankingElement();

        data.UserName = userName;
        data.HighScore = highScore;

        rankingData.offlineRankingData[animal].Add(data);
    }

    //オンライン用ランキングデータに情報を突っ込む
    public void SetOnlineRankingInfo(string userName, string winCount)
    {
        OnlineRankingElement data = new OnlineRankingElement();

        data.UserName = userName;
        data.WinCount = winCount;

        rankingData.onlineRankingData.Add(data);
    }

    //オフライン用ランキングデータを表示する
    private void displayOfflineRankingData(string animal)
    {
        //入力値エラーでリターン
        if (animal == "")
        {
            return;
        }

        //ランキングデータ初期化
        OfflineRankingUsernameText.text = "";
        OfflineRankingHighScoreText.text = "";

        OfflineRankingAnimalNameText.text = animal;

        //データ更新
        foreach (OfflineRankingElement data in rankingData.offlineRankingData[animal])
        {
            OfflineRankingUsernameText.text += data.UserName + "\n";
            OfflineRankingHighScoreText.text += data.HighScore + "\n";
        }
    }

    //オンライン用ランキングデータを表示する
    private void displayOnlineRankingData()
    {
        //ランキングデータ初期化
        OnlineRankingUsernameText.text = "";
        OnlineRankingWinCountText.text = "";

        //データ更新
        foreach (OnlineRankingElement data in rankingData.onlineRankingData)
        {
            OnlineRankingUsernameText.text += data.UserName + "\n";
            OnlineRankingWinCountText.text += data.WinCount + "\n";
        }
    }

    //ランキングデータ
    public class RankingData
    {
        public Dictionary<string, List<OfflineRankingElement>> offlineRankingData
            = new Dictionary<string, List<OfflineRankingElement>>();
        public List<OnlineRankingElement> onlineRankingData
            = new List<OnlineRankingElement>();

        //ランキングデータのインスタンス化
        public RankingData()
        {
            List<OfflineRankingElement> totalRankingList = new List<OfflineRankingElement>();
            offlineRankingData.Add("Total", totalRankingList);

            List<OfflineRankingElement> giraffeRankingList = new List<OfflineRankingElement>();
            offlineRankingData.Add("Giraffe", giraffeRankingList);

            List<OfflineRankingElement> elephantRankingList = new List<OfflineRankingElement>();
            offlineRankingData.Add("Elephant", elephantRankingList);

            List<OfflineRankingElement> dogRankingList = new List<OfflineRankingElement>();
            offlineRankingData.Add("Dog", dogRankingList);

            List<OfflineRankingElement> tigerRankingList = new List<OfflineRankingElement>();
            offlineRankingData.Add("Tiger", tigerRankingList);

            List<OfflineRankingElement> catRankingList = new List<OfflineRankingElement>();
            offlineRankingData.Add("Cat", catRankingList);

            List<OfflineRankingElement> rabbitRankingList = new List<OfflineRankingElement>();
            offlineRankingData.Add("Rabbit", rabbitRankingList);
        }
    }

    //オフラインランキング要素
    public class OfflineRankingElement
    {
        public string UserName;
        public string HighScore;
    }

    //オンラインランキング要素
    public class OnlineRankingElement
    {
        public string UserName;
        public string WinCount;
    }

    //コルーチンで別スレッド化
    //ランキング情報取得のためにちょっとだけ時間を稼ぐ
    private IEnumerator prepareRankingData()
    {
        //1秒待つ
        yield return new WaitForSeconds(1.0f);

        //最初に表示されるデータ
        displayOfflineRankingData("Total");

        //ランキングパネルを表示する
        WinCountRankingPanel.SetActive(true);

        yield break;
    }
}

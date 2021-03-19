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

    //動物の表示用
    private GameObject menuAnimal;

    //自己Win数のテキスト表示
    [SerializeField]
    private Text WinCountText;

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
    //Giraffe
    //ベストタイムランキングテキスト表示
    [SerializeField]
    public GameObject OfflineRankingGiraffeNameTextGameObject;
    [SerializeField]
    public Text OfflineRankingGiraffeNameText;
    //Elephant
    //ベストタイムランキングテキスト表示
    [SerializeField]
    public GameObject OfflineRankingElephantNameTextGameObject;
    [SerializeField]
    public Text OfflineRankingElephantNameText;
    //Dog
    //ベストタイムランキングテキスト表示
    [SerializeField]
    public GameObject OfflineRankingDogNameTextGameObject;
    [SerializeField]
    public Text OfflineRankingDogNameText;
    //Tiger
    //ベストタイムランキングテキスト表示
    [SerializeField]
    public GameObject OfflineRankingTigerNameTextGameObject;
    [SerializeField]
    public Text OfflineRankingTigerNameText;

    //コインの枚数を表示
    [SerializeField]
    private Text MyCoinText;
    //ログインボーナスパネルの表示
    [SerializeField]
    private GameObject LoginBounusPanel;
    //ログインボーナステキストの表示
    [SerializeField]
    private Text LoginBounusText;
    //ログインボーナスコインテキストの表示
    [SerializeField]
    private Text LoginBounusCoinText;
    //取得ログインボーナスコイン
    private int getLoginBounusCoin = 50;

    //ロビーマネジャーのゲームオブジェクト
    [SerializeField]
    private GameObject LobbyManager;
    //MaxPlayerPanelの表示
    [SerializeField]
    private GameObject MaxPlayerPanel;


    // Start is called before the first frame update
    void Start()
    {

        //回転可能にする
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.orientation = ScreenOrientation.AutoRotation;

        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //UserAuthのスクリプトの関数使用
        userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();

        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }

        //ルーム内のクライアントがMasterClientと同じシーンをロードしないように設定
        PhotonNetwork.AutomaticallySyncScene = false;

        //ベストタイムスコアを取得
        SelectCharacterBestTimeGet();

        //アニマルの初期設定を入れる
        if (SelectCharacterUI.animalName == null)
        {
            SelectCharacterUI.animalName = "Dog";
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

    }

    // Update is called once per frame
    void Update()
    {


        //デバイスに保持されているコインの枚数を表示
        if (!PlayerPrefs.HasKey("myCoin"))
        {
            PlayerPrefs.SetInt("myCoin", 0);
        }
        else
        {
            MyCoinText.text = PlayerPrefs.GetInt("myCoin").ToString("");
        }

        //デバイスに保持されているWin数の表示
        if (!PlayerPrefs.HasKey("WinCount"))
        {
            PlayerPrefs.SetInt("WinCount", 0);
        }
        else
        {
            WinCountText.text = PlayerPrefs.GetInt("WinCount").ToString("");
        }

        //日付の確認
        CheckToday();

        //デバイスに保持されているUnlockキャラクター情報を取得
        //0：アンロックされていない
        //1：アンロックされている
        //キリン
        if (!PlayerPrefs.HasKey("Unlock_Giraffe"))
        {
            PlayerPrefs.SetInt("Unlock_Giraffe", 0);
        }
        //象
        if (!PlayerPrefs.HasKey("Unlock_Elephant"))
        {
            PlayerPrefs.SetInt("Unlock_Elephant", 0);
        }
        //虎
        if (!PlayerPrefs.HasKey("Unlock_Tiger"))
        {
            PlayerPrefs.SetInt("Unlock_Tiger", 0);
        }

        
    }

    //日付の確認
    private void CheckToday()
    {
        //本日の日付を取得
        DateTime now = DateTime.Now;
        //日付を連続数値で取得
        int todayInt = 0;
        todayInt = now.Year * 10000 + now.Month * 100 + now.Day;

        //デバイスに日付が保持されているか確認
        if (!PlayerPrefs.HasKey("Date"))
        {
            PlayerPrefs.SetInt("Date", 0);
        }
        else
        {
            //次の日か確認
            if(todayInt - PlayerPrefs.GetInt("Date") > 0)
            {
                //次の日であった場合の処理
                PlayerPrefs.SetInt("Date", todayInt);

                //ログインボーナステキストを表示する
                LoginBounusText.text = "ログインボーナスを獲得しました";
                //ログインボーナスコインテキストを表示する
                LoginBounusCoinText.text = getLoginBounusCoin.ToString("") + "コイン";
                //ログインボーナスパネルを表示する
                LoginBounusPanel.SetActive(true);
            }
            else
            {
            }
        }
    }

    //ベストタイムスコアを取得
    private void SelectCharacterBestTimeGet()
    {
        if (!PlayerPrefs.HasKey("BestTime_Giraffe") || !PlayerPrefs.HasKey("BestTime_Elephant") || !PlayerPrefs.HasKey("BestTime_Dog") || !PlayerPrefs.HasKey("BestTime_Tiger"))
        {
            //mobile backendに接続しベストタイムを初期登録する
            userAuth.firstSetBestTime();
        }
        
    }

    //LoginBounusYesButtonボタンを押した時の挙動
    public void OnClick_LoginBounusYesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //50コイン獲得する
        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") + getLoginBounusCoin);
        //ログインボーナスパネルを非表示にする
        LoginBounusPanel.SetActive(false);

        
        //オンラインtop30
        userAuth.TopRankers();
        //オフラインtop15
        OfflineRankingAnimalNameText.text = "Giraffe";
        userAuth.TopOfflineRankers("Giraffe");
        
        //ランキングパネルを表示する
        WinCountRankingPanel.SetActive(true);
    }

    //オフラインランキングのアニマル切り替えボタン(右)
    public void OnClick_OfflineRankingAnimalNameRightButton()
    {
        //キリン→象→犬→虎
        if (OfflineRankingAnimalNameText.text == "Giraffe")
        {
            //アニマル名を表示
            OfflineRankingAnimalNameText.text = "Elephant";

            //初回のみランキング取得
            if (OfflineRankingElephantNameText.text == "")
            {
                userAuth.TopOfflineRankers("Elephant");
            }
            //ランキングテキストを表示/非表示にする
            OfflineRankingElephantNameTextGameObject.SetActive(true);
            OfflineRankingGiraffeNameTextGameObject.SetActive(false);
        }
        else if (OfflineRankingAnimalNameText.text == "Elephant")
        {
            OfflineRankingAnimalNameText.text = "Dog";

            //初回のみランキング取得
            if (OfflineRankingDogNameText.text == "")
            {
                userAuth.TopOfflineRankers("Dog");
            }
            //ランキングテキストを表示/非表示にする
            OfflineRankingDogNameTextGameObject.SetActive(true);
            OfflineRankingElephantNameTextGameObject.SetActive(false);
        }
        else if (OfflineRankingAnimalNameText.text == "Dog")
        {
            OfflineRankingAnimalNameText.text = "Tiger";

            //初回のみランキング取得
            if (OfflineRankingTigerNameText.text == "")
            {
                userAuth.TopOfflineRankers("Tiger");
            }
            //ランキングテキストを表示/非表示にする
            OfflineRankingTigerNameTextGameObject.SetActive(true);
            OfflineRankingDogNameTextGameObject.SetActive(false);
        }
        else if (OfflineRankingAnimalNameText.text == "Tiger")
        {

        }
    }

    //オフラインランキングのアニマル切り替えボタン(左)
    public void OnClick_OfflineRankingAnimalNameLeftButton()
    {
        //キリン←象←犬←虎
        if (OfflineRankingAnimalNameText.text == "Giraffe")
        {
        }
        else if (OfflineRankingAnimalNameText.text == "Elephant")
        {
            OfflineRankingAnimalNameText.text = "Giraffe";

            //初回のみランキング取得
            if (OfflineRankingGiraffeNameText.text == "")
            {
                userAuth.TopOfflineRankers("Giraffe");
            }
            //ランキングテキストを表示/非表示にする
            OfflineRankingGiraffeNameTextGameObject.SetActive(true);
            OfflineRankingElephantNameTextGameObject.SetActive(false);
        }
        else if (OfflineRankingAnimalNameText.text == "Dog")
        {
            OfflineRankingAnimalNameText.text = "Elephant";
            //ランキングテキストを表示/非表示にする
            OfflineRankingElephantNameTextGameObject.SetActive(true);
            OfflineRankingDogNameTextGameObject.SetActive(false);
        }
        else if (OfflineRankingAnimalNameText.text == "Tiger")
        {
            OfflineRankingAnimalNameText.text = "Dog";
            //ランキングテキストを表示/非表示にする
            OfflineRankingDogNameTextGameObject.SetActive(true);
            OfflineRankingTigerNameTextGameObject.SetActive(false);
        }
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

        //初回のみランキング取得
        if (WinCountRankingNameText.text == "")
        {
            userAuth.TopRankers();
        }

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

        //回転しないようにする
        if (Screen.width > Screen.height)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

        //画面遷移
        SceneManager.LoadScene("WaitingRoom(offline)");
    }

    //オンラインボタンを押した際の挙動
    public void OnClick_OnlineButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //回転しないようにする
        if (Screen.width > Screen.height)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

        //ロビーマネジャーのゲームオブジェクトをオンにする
        LobbyManager.SetActive(true);
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

    //MaxPlayerYesButtonボタンを押した時の挙動
    public void OnClick_MaxPlayerYesButton()
    {
        MaxPlayerPanel.SetActive(false);
    }

    //タイトルボタンを押した際の挙動
    public void OnClick_TitleButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("Title");
    }
}

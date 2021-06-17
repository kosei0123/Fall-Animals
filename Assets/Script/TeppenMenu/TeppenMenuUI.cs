using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeppenMenuUI : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

    //挑戦ボタン押下時に出るパネル
    [SerializeField]
    private GameObject ChallengePanel;
    //リタイアボタン押下時に出るパネル
    [SerializeField]
    private GameObject RetirePanel;

    //マイコインテキスト
    [SerializeField]
    private Text MyCoinText;
    //獲得予定コインテキスト
    [SerializeField]
    private Text GetCoinText;
    //フロアテキスト
    [SerializeField]
    private Text FloorText;

    //挑戦ボタン
    [SerializeField]
    private Button ChallengeButton;
    //本日の挑戦回数
    [SerializeField]
    private Text DairyCountText;

    //最初に表示される画面
    //パネル
    [SerializeField]
    private GameObject StartFloorPanelGameObject;
    [SerializeField]
    private Image StartFloorPanel;
    //文字
    [SerializeField]
    private Text StartFloorText;
    //α値を操作する
    private Color StartFloorPanelAlphaColor;
    private Color StartFloorTextAlphaColor;

    //使用キャラクターのパネル
    //メイン
    [SerializeField]
    private GameObject DogPanel;
    [SerializeField]
    private GameObject GiraffePanel;
    [SerializeField]
    private GameObject ElephantPanel;
    [SerializeField]
    private GameObject TigerPanel;
    [SerializeField]
    private GameObject CatPanel;
    [SerializeField]
    private GameObject RabbitPanel;
    //サブ
    [SerializeField]
    private GameObject DogSubPanel;
    [SerializeField]
    private GameObject GiraffeSubPanel;
    [SerializeField]
    private GameObject ElephantSubPanel;
    [SerializeField]
    private GameObject TigerSubPanel;
    [SerializeField]
    private GameObject CatSubPanel;
    [SerializeField]
    private GameObject RabbitSubPanel;

    //テッペンメニューゲームオブジェクト
    [SerializeField]
    private GameObject TeppenMenuCanvas;
    //テッペンショップゲームオブジェクト
    [SerializeField]
    private GameObject TeppenShopCanvas;
    //テッペンレコードオブジェクト
    [SerializeField]
    private GameObject TeppenRecordCanvas;

    //テッペンバトルシーン非同期呼び出し用
    public AsyncOperation async_TeppenBattleScene;

    private void Awake()
    {
        //メモリ解放
        Resources.UnloadUnusedAssets();
    }

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //回転可能にする
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;

        //マイコインの表示
        MyCoinText.text = PlayerPrefs.GetInt("myCoin").ToString("");

        //デバイスに保持されている獲得予定コインの枚数を表示
        if (!PlayerPrefs.HasKey("getScheduledCoin")) PlayerPrefs.SetInt("getScheduledCoin", 0);
        GetCoinText.text = PlayerPrefs.GetInt("getScheduledCoin").ToString("");

        //デバイスにテッペンの状態を保存
        if (!PlayerPrefs.HasKey("TeppenStatus")) PlayerPrefs.SetString("TeppenStatus", "Play");

        //最初の画面のパネルを表示する
        StartFloorPanelGameObject.SetActive(true);
        StartFloorPanelAlphaColor = StartFloorPanel.color;
        StartFloorTextAlphaColor = StartFloorText.color;

        //フロアを表示する
        StartFloorText.text = "〜" + PlayerPrefs.GetInt("TeppenFloor").ToString("") + "階〜";
        FloorText.text = "〜" + PlayerPrefs.GetInt("TeppenFloor").ToString("") + "階〜";

        //テッペンの状態を確認
        switch (PlayerPrefs.GetString("TeppenStatus"))
        {
            case "Play":
                break;
            case "GameOver":
                //獲得予定コインを半分にする
                PlayerPrefs.SetInt("getScheduledCoin", PlayerPrefs.GetInt("getScheduledCoin") / 2);
                PlayerPrefs.Save();
                TeppenMenuCanvas.SetActive(false);
                TeppenRecordCanvas.SetActive(true);
                break;
            case "Retire":
                /*リタイアボタン押下のところに記述*/
                break;
            default:
                break;
        }

        //使用アニマルの名前と色の設定
        if(PlayerPrefs.GetInt("TeppenFloor") == 1)
        {
            PlayerPrefs.SetString("TeppenAnimalName", SelectCharacterUI.animalName);
            PlayerPrefs.SetString("TeppenAnimalColor", SelectCharacterUI.animalName_Color);
            PlayerPrefs.Save();
        }
        else
        {
            SelectCharacterUI.animalName = PlayerPrefs.GetString("TeppenAnimalName");
            SelectCharacterUI.animalName_Color = PlayerPrefs.GetString("TeppenAnimalColor");
        }

        //使用キャラクターの表示
        PlayCharacterDisplay();

        //挑戦回数の表示
        DairyCountText.text = "本日の挑戦回数：" + PlayerPrefs.GetInt("TeppenDairyChallenge") + "/ 7";

        //TeppenBattleSceneの非同期呼び出し
        async_TeppenBattleScene = SceneManager.LoadSceneAsync("TeppenBattleScene");
        async_TeppenBattleScene.allowSceneActivation = false;



    }

    // Update is called once per frame
    void Update()
    {
        //徐々に画面を薄くする
        if (StartFloorPanelAlphaColor.a > 0 || StartFloorTextAlphaColor.a > 0)
        {
            StartFloorPanelAlphaColor.a -= Time.deltaTime;
            StartFloorTextAlphaColor.a -= Time.deltaTime;
        }
        else
        {
            if(PlayerPrefs.GetInt("TeppenDairyChallenge") < 7)
            {
                ChallengeButton.interactable = true;
            }
            else
            {

            }
        }
        //操作したα値を取得する
        StartFloorPanel.color = StartFloorPanelAlphaColor;
        StartFloorText.color = StartFloorTextAlphaColor;

        
    }

    //使用キャラクターの表示
    private void PlayCharacterDisplay()
    {
        //メイン
        switch (SelectCharacterUI.animalName)
        {
            case "Dog":
                DogPanel.SetActive(true);
                break;
            case "Giraffe":
                GiraffePanel.SetActive(true);
                break;
            case "Elephant":
                ElephantPanel.SetActive(true);
                break;
            case "Tiger":
                TigerPanel.SetActive(true);
                break;
            case "Cat":
                CatPanel.SetActive(true);
                break;
            case "Rabbit":
                RabbitPanel.SetActive(true);
                break;
        }

        //サブ
        if (PlayerPrefs.GetInt("TeppenDogCanUse") == 1) DogSubPanel.SetActive(true);
        if (PlayerPrefs.GetInt("TeppenGiraffeCanUse") == 1) GiraffeSubPanel.SetActive(true);
        if (PlayerPrefs.GetInt("TeppenElephantCanUse") == 1) ElephantSubPanel.SetActive(true);
        if (PlayerPrefs.GetInt("TeppenTigerCanUse") == 1) TigerSubPanel.SetActive(true);
        if (PlayerPrefs.GetInt("TeppenCatCanUse") == 1) CatSubPanel.SetActive(true);
        if (PlayerPrefs.GetInt("TeppenRabbitCanUse") == 1) RabbitSubPanel.SetActive(true);
    }

    //挑戦ボタン押下
    public void OnClick_ChallengeButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //パネル
        if (PlayerPrefs.GetInt("TeppenFloor") == 1) ChallengePanel.SetActive(true);
        else
        {
            //TeppenMenuCanvas.SetActive(false);
            TeppenShopCanvas.SetActive(true);
            soundManager.BGMManager("BGM_TeppenShop");
        }
        
    }

    //挑戦ボタン押下後に出るダイアログでのボタン押下時
    public void OnClick_ChallengeYesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //パネル
        ChallengePanel.SetActive(false);
        //TeppenMenuCanvas.SetActive(false);
        TeppenShopCanvas.SetActive(true);
        soundManager.BGMManager("BGM_TeppenShop");
    }

    //リタイアボタン押下
    public void OnClick_RetireButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //パネル
        RetirePanel.SetActive(true);
    }

    //リタイアボタン押下後に出るダイアログでのボタン押下時(Yes)
    public void OnClick_RetireYesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //パネル
        RetirePanel.SetActive(false);
        TeppenMenuCanvas.SetActive(false);
        TeppenRecordCanvas.SetActive(true);
    }

    //リタイアボタン押下後に出るダイアログでのボタン押下時(No)
    public void OnClick_RetireNoButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //パネル
        RetirePanel.SetActive(false);
    }

    //メニューに戻るボタン押下
    public void OnClick_MenuButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("Menu");
    }
}

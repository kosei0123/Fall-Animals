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

    //挑戦ボタン押下の最初の1回目
    private static bool ChallengePanelFirst = false;

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
                SceneManager.LoadScene("TeppenRecord");
                break;
            case "Retire":
                /*リタイアボタン押下のところに記述*/
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //徐々に画面を薄くする
        if (StartFloorPanelAlphaColor.a > 0 || StartFloorTextAlphaColor.a > 0)
        {
            StartFloorPanelAlphaColor.a -= 0.01f;
            StartFloorTextAlphaColor.a -= 0.01f;
        }
        //操作したα値を取得する
        StartFloorPanel.color = StartFloorPanelAlphaColor;
        StartFloorText.color = StartFloorTextAlphaColor;

        
    }

    //挑戦ボタン押下
    public void OnClick_ChallengeButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //パネル
        if (ChallengePanelFirst == false) ChallengePanel.SetActive(true);
        else { SceneManager.LoadScene("TeppenShop"); }
        
    }

    //挑戦ボタン押下後に出るダイアログでのボタン押下時
    public void OnClick_ChallengeYesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //パネル
        ChallengePanelFirst = true;
        ChallengePanel.SetActive(false);
        SceneManager.LoadScene("TeppenShop");
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
        SceneManager.LoadScene("TeppenRecord");
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

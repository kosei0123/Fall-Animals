using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;
    //UserAuthのスクリプトの関数使用
    UserAuth userAuth;

    //自己Win数のテキスト表示
    [SerializeField]
    private Text WinCountText;
    //WinCountRankingPanelの表示
    [SerializeField]
    private GameObject WinCountRankingPanel;
    //Win数のランキングテキスト表示
    [SerializeField]
    public Text WinCountRankingNameText;
    [SerializeField]
    public Text WinCountRankingNumberText;

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

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //UserAuthのスクリプトの関数使用
        userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();


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
            WinCountText.text = "Win　" + PlayerPrefs.GetInt("WinCount").ToString("");
        }

        //日付の確認
        CheckToday();

        //デバイスに保持されているUnlockキャラクター情報を取得
        //0：アンロックされていない
        //1：アンロックされている
        if (!PlayerPrefs.HasKey("Unlock_Elephant"))
        {
            PlayerPrefs.SetInt("Unlock_Elephant", 0);
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

    //LoginBounusYesButtonボタンを押した時の挙動
    public void OnClick_LoginBounusYesButton()
    {
        //50コイン獲得する
        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") + getLoginBounusCoin);
        //ログインボーナスパネルを非表示にする
        LoginBounusPanel.SetActive(false);

        //ランキングパネルを表示する
        userAuth.TopRankers();
        WinCountRankingPanel.SetActive(true);
    }

    //WinCountRankingYesButtonボタンを押した時の挙動
    public void OnClick_WinCountRankingYesButton()
    {
        //ランキングパネルを非表示にする
        WinCountRankingPanel.SetActive(false);
    }

    //オフラインボタンを押した際の挙動
    public void OnClick_OfflineButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
    }

    //オンラインボタンを押した際の挙動
    public void OnClick_OnlineButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("EnterLobby");
    }

    //アンロックボタンを押した際の挙動
    public void OnClick_UnlockButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("Unlock");
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

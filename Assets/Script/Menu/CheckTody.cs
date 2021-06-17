using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckTody : MonoBehaviour
{
    //ログインボーナスパネルの表示
    [SerializeField]
    public GameObject LoginBounusPanel;
    //連続ログイン日数の表示
    [SerializeField]
    private Text ConsecutiveLoginDaysText;
    //取得ログインボーナスコイン
    [HideInInspector]
    public int getLoginBounusCoin = 50;

    //ウサギスタンプ
    [SerializeField]
    private GameObject MoveAnimalImage1;
    [SerializeField]
    private GameObject MoveAnimalImage2;
    [SerializeField]
    private GameObject MoveAnimalImage3;
    [SerializeField]
    private GameObject MoveAnimalImage4;
    [SerializeField]
    private GameObject MoveAnimalImage5;
    [SerializeField]
    private GameObject MoveAnimalImage6;
    [SerializeField]
    private GameObject MoveAnimalImage7;
    [SerializeField]
    private GameObject MoveAnimalImage8;
    [SerializeField]
    private GameObject MoveAnimalImage9;
    [SerializeField]
    private GameObject MoveAnimalImage10;
    private GameObject[] MoveAnimalImage = new GameObject[10];
    //フレーム
    [SerializeField]
    private GameObject Frame1;
    [SerializeField]
    private GameObject Frame2;
    [SerializeField]
    private GameObject Frame3;
    [SerializeField]
    private GameObject Frame4;
    [SerializeField]
    private GameObject Frame5;
    [SerializeField]
    private GameObject Frame6;
    [SerializeField]
    private GameObject Frame7;
    [SerializeField]
    private GameObject Frame8;
    [SerializeField]
    private GameObject Frame9;
    [SerializeField]
    private GameObject Frame10;

    //本日の日付を取得
    private DateTime now = DateTime.Now;
    //日付を連続数値で取得
    int todayInt = 0;
    //連続ログイン日数から日にちのみを切り出す
    private string bkIndex;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("Date", 10000000);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //日付の確認
    public void CheckToday()
    {
        
        //日付を連続数値で取得
        todayInt = now.Year * 10000 + now.Month * 100 + now.Day;
        

        //連続ログイン日数を保存
        if (!PlayerPrefs.HasKey("ConsecutiveLoginDays")) PlayerPrefs.SetInt("ConsecutiveLoginDays", 0);

        //デバイスに日付が保持されているか確認
        if (!PlayerPrefs.HasKey("Date"))
        {
            PlayerPrefs.SetInt("Date", 10000000);
        }

        //配列にて取得する
        //ウサギスタンプ
        MoveAnimalImage[0] = MoveAnimalImage1;
        MoveAnimalImage[1] = MoveAnimalImage2;
        MoveAnimalImage[2] = MoveAnimalImage3;
        MoveAnimalImage[3] = MoveAnimalImage4;
        MoveAnimalImage[4] = MoveAnimalImage5;
        MoveAnimalImage[5] = MoveAnimalImage6;
        MoveAnimalImage[6] = MoveAnimalImage7;
        MoveAnimalImage[7] = MoveAnimalImage8;
        MoveAnimalImage[8] = MoveAnimalImage9;
        MoveAnimalImage[9] = MoveAnimalImage10;

        //連続ログイン日数から日にちのみを切り出す
        bkIndex = PlayerPrefs.GetInt("Date").ToString("");
        bkIndex = bkIndex.Substring(bkIndex.Length - 2);

        //次の日(連続)かを確認する
        CheckNextDay();

        //本日獲得分の表示
        GetToday();

        //テッペンデイリー挑戦回数をリセットする
        PlayerPrefs.SetInt("TeppenDairyChallenge", 0);

    }

    //次の日(連続)かを確認する
    private void CheckNextDay()
    {
        //次の日か確認
        if (todayInt - PlayerPrefs.GetInt("Date") == 1)
        {
            //次の日であった場合の処理
            PlayerPrefs.SetInt("Date", todayInt);
            //連続ログイン日数を更新する
            PlayerPrefs.SetInt("ConsecutiveLoginDays", PlayerPrefs.GetInt("ConsecutiveLoginDays") + 1);

            //連続ログイン日数の表示
            ConsecutiveLoginDaysText.text = "連続ログイン" + PlayerPrefs.GetInt("ConsecutiveLoginDays").ToString("") + "日目";

            //ログインボーナスコインテキストを表示する
            GetLoginBounusCoins();
            //ログインボーナスパネルを表示する
            LoginBounusPanel.SetActive(true);
        }
        else if(todayInt - PlayerPrefs.GetInt("Date") <= 100 && (now.Month == 2 || now.Month == 4 || now.Month == 6 || now.Month == 8 || now.Month == 9 || now.Month == 11)
            && now.Day == 1 && bkIndex == "31")
        {
            //次の日であった場合の処理
            PlayerPrefs.SetInt("Date", todayInt);
            //連続ログイン日数を更新する
            PlayerPrefs.SetInt("ConsecutiveLoginDays", PlayerPrefs.GetInt("ConsecutiveLoginDays") + 1);

            //連続ログイン日数の表示
            ConsecutiveLoginDaysText.text = "連続ログイン" + PlayerPrefs.GetInt("ConsecutiveLoginDays").ToString("") + "日目";
            //ログインボーナスコインテキストを表示する
            GetLoginBounusCoins();
            //ログインボーナスパネルを表示する
            LoginBounusPanel.SetActive(true);
        }
        else if (todayInt - PlayerPrefs.GetInt("Date") <= 100 && (now.Month == 5 || now.Month == 7 || now.Month == 10 || now.Month == 12) && now.Day == 1 && bkIndex == "30")
        {
            //次の日であった場合の処理
            PlayerPrefs.SetInt("Date", todayInt);
            //連続ログイン日数を更新する
            PlayerPrefs.SetInt("ConsecutiveLoginDays", PlayerPrefs.GetInt("ConsecutiveLoginDays") + 1);

            //連続ログイン日数の表示
            ConsecutiveLoginDaysText.text = "連続ログイン" + PlayerPrefs.GetInt("ConsecutiveLoginDays").ToString("") + "日目";
            //ログインボーナスコインテキストを表示する
            GetLoginBounusCoins();
            //ログインボーナスパネルを表示する
            LoginBounusPanel.SetActive(true);
        }
        else if (todayInt - PlayerPrefs.GetInt("Date") <= 9000 && now.Month == 1 && now.Day == 1 && bkIndex == "31")//20210101-20201231=8870、20210101-20201031=9070
        {
            //次の日であった場合の処理
            PlayerPrefs.SetInt("Date", todayInt);
            //連続ログイン日数を更新する
            PlayerPrefs.SetInt("ConsecutiveLoginDays", PlayerPrefs.GetInt("ConsecutiveLoginDays") + 1);

            //連続ログイン日数の表示
            ConsecutiveLoginDaysText.text = "連続ログイン" + PlayerPrefs.GetInt("ConsecutiveLoginDays").ToString("") + "日目";
            //ログインボーナスコインテキストを表示する
            GetLoginBounusCoins();
            //ログインボーナスパネルを表示する
            LoginBounusPanel.SetActive(true);
        }
        else if (todayInt - PlayerPrefs.GetInt("Date") <= 100 && ((now.Year % 4 == 0 && now.Month == 3 && now.Day == 1 && bkIndex == "29") || (now.Year % 4 != 0 && now.Month == 3 && now.Day == 1 && bkIndex == "28")))
        {
            //次の日であった場合の処理
            PlayerPrefs.SetInt("Date", todayInt);
            //連続ログイン日数を更新する
            PlayerPrefs.SetInt("ConsecutiveLoginDays", PlayerPrefs.GetInt("ConsecutiveLoginDays") + 1);

            //連続ログイン日数の表示
            ConsecutiveLoginDaysText.text = "連続ログイン" + PlayerPrefs.GetInt("ConsecutiveLoginDays").ToString("") + "日目";
            //ログインボーナスコインテキストを表示する
            GetLoginBounusCoins();
            //ログインボーナスパネルを表示する
            LoginBounusPanel.SetActive(true);
        }
        else if(todayInt - PlayerPrefs.GetInt("Date") > 1)
        {
            //次の日であった場合の処理
            PlayerPrefs.SetInt("Date", todayInt);
            //連続ログイン日数を更新する
            PlayerPrefs.SetInt("ConsecutiveLoginDays", 1);

            //連続ログイン日数の表示
            ConsecutiveLoginDaysText.text = "連続ログイン" + PlayerPrefs.GetInt("ConsecutiveLoginDays").ToString("") + "日目";
            //ログインボーナスコインテキストを表示する
            GetLoginBounusCoins();
            //ログインボーナスパネルを表示する
            LoginBounusPanel.SetActive(true);
        }
    }

    //獲得するログインボーナスコイン
    private void GetLoginBounusCoins()
    {
        //10日連続ログインごとに500コイン
        //その他は50コイン
        if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 10 == 0) getLoginBounusCoin = 500;
        else if(PlayerPrefs.GetInt("ConsecutiveLoginDays") % 5 == 0) getLoginBounusCoin = 100;
        else { getLoginBounusCoin = 50; }
    }

    //本日獲得分の表示
    private void GetToday()
    {
        if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 10 == 0)
        {
            for(int i = 0; i < 9; i++)
            {
                MoveAnimalImage[i].SetActive(true);
            }
            Frame10.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 9 == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                MoveAnimalImage[i].SetActive(true);
            }
            Frame9.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 8 == 0)
        {
            for (int i = 0; i < 7; i++)
            {
                MoveAnimalImage[i].SetActive(true);
            }
            Frame8.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 7 == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                MoveAnimalImage[i].SetActive(true);
            }
            Frame7.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 6 == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                MoveAnimalImage[i].SetActive(true);
            }
            Frame6.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 5 == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                MoveAnimalImage[i].SetActive(true);
            }
            Frame5.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 4 == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                MoveAnimalImage[i].SetActive(true);
            }
            Frame4.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 3 == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                MoveAnimalImage[i].SetActive(true);
            }
            Frame3.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 2 == 0)
        {
            for (int i = 0; i < 1; i++)
            {
                MoveAnimalImage[i].SetActive(true);
            }
            Frame2.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ConsecutiveLoginDays") % 1 == 0)
        {
            Frame1.SetActive(true);
        }

    }
}

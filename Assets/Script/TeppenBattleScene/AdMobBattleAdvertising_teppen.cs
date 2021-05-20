#if UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdMobBattleAdvertising_teppen : MonoBehaviour
{
    //EndDialog_teppenの関数等を使う
    EndDialog_teppen endDialog_teppen;
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

    //1度広告を見たらボタンを押下できないようにする
    [SerializeField]
    private Button RewardAdvertisingButton;

    //リワード
    private RewardedAd rewardedAd;
    //広告ユニットID用
    private string adUnitId;

    // Start is called before the first frame update
    void Start()
    {
        //EndDialog_teppenの関数等を使う
        endDialog_teppen = GameObject.Find("DialogCanvas").GetComponent<EndDialog_teppen>();
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //RequestReward()関数を呼ぶ
        RequestReward();
    }

    private void RequestReward()
    {
        // 広告ユニットID
        //iPhoneでの動作
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //テスト
            //adUnitId = "ca-app-pub-3940256099942544/5224354917";
            adUnitId = "ca-app-pub-8452025378548231/9250095442";
        }
        //Androidでの動作
        else if (Application.platform == RuntimePlatform.Android)
        {
            //テストID
            //adUnitId = "ca-app-pub-3940256099942544/5224354917";
            adUnitId = "ca-app-pub-8452025378548231/1115773337";
        }

        //広告ユニットIDを指定してrewardedAdをインスタンス化する
        this.rewardedAd = new RewardedAd(adUnitId);

        //広告が読み込まれた時
        //this.rewardedAd.OnAdLoaded += HandleOnAdLoaded;

        //広告が画面いっぱいに表示された時
        //this.rewardedAd.OnAdOpening += HandleOnAdOpened;

        //Close時の処理
        this.rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;

        //動画の視聴が完了したら「HandleUserEarnedReward」を呼ぶ
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        //空のリクエストを作成
        AdRequest request = new AdRequest.Builder().Build();

        //動画のリワード広告をロードする
        this.rewardedAd.LoadAd(request);

    }

    //呼ばれた際動画が流れる
    public void ShowReward()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    //RewardAdvertisingButtonボタン押下時の挙動
    public void OnClick_RewardAdvertisingButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        
        //広告を流す
        ShowReward();
    }

    //広告が読み込まれた時
    //public void HandleOnAdLoaded(object sender, EventArgs args)
    //{
    //    //横向き固定にする
    //    Screen.orientation = ScreenOrientation.LandscapeLeft;
    //}

    //広告が画面いっぱいに表示された時
    //public void HandleOnAdOpened(object sender, EventArgs args)
    //{
    //    //横向き固定にする
    //    Screen.orientation = ScreenOrientation.LandscapeLeft;
    //}


    //Close時の処理
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        //ボタン押下不可にする
        RewardAdvertisingButton.interactable = false;
        //横向き固定にする
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    //動画の視聴が完了したら実行される
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //再度コインを獲得する
        endDialog_teppen.DialogPanelActive(PlayerPrefs.GetInt("TeppenFloor"));
        //横向き固定にする
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

#elif UNITY_ANDROID

#endif
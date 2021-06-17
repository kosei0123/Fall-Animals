#if UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdMobBattleAdvertising : MonoBehaviour
{
    //Pun2Scriptのpublic定数を使う
    Pun2Script pun2Script;
    //EndDialogの関数等を使う
    EndDialog endDialog;
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //MoveScreenTimerの定数を使う
    MoveScreenTimer moveScreenTimer;


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
        //Pun2Scriptのpublic定数を使う
        pun2Script = GameObject.Find("Pun2").GetComponent<Pun2Script>();
        //EndDialogの関数等を使う
        endDialog = GameObject.Find("DialogCanvas").GetComponent<EndDialog>();
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //MoveScreenTimerの定数を使う
        moveScreenTimer = GameObject.Find("TimerCanvas").GetComponent<MoveScreenTimer>();

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
        //シーン移動不可
        moveScreenTimer.moveScreenFlag = false;

        //広告を流す
        ShowReward();
    }

    //Close時の処理
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        //シーン移動可能
        moveScreenTimer.moveScreenFlag = true;
        //横向き固定にする
        if (Screen.width < Screen.height)
        {
            Screen.autorotateToLandscapeLeft = true;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        //ボタン押下不可にする
        RewardAdvertisingButton.interactable = false;
    }

    //動画の視聴が完了したら実行される
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //再度コインを獲得する
        endDialog.DialogPanelActive(pun2Script.battleRanking);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

#elif UNITY_ANDROID

#endif
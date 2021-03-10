using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class AdMobBattleAdvertising_offline : MonoBehaviour
{
    //BattleScene_offlineManagerのpublic定数を使う
    BattleScene_offlineManager battleScene_offlineManager;
    //EndDialog_offlineの関数等を使う
    EndDialog_offline endDialog_offline;
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
        //Pun2Scriptのpublic定数を使う
        battleScene_offlineManager = GameObject.Find("BattleScene_offlineManager").GetComponent<BattleScene_offlineManager>();
        //EndDialogの関数等を使う
        endDialog_offline = GameObject.Find("DialogCanvas").GetComponent<EndDialog_offline>();
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
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
        }

        //広告ユニットIDを指定してrewardedAdをインスタンス化する
        this.rewardedAd = new RewardedAd(adUnitId);

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
        //ボタン押下不可にする
        RewardAdvertisingButton.interactable = false;
    }

    //動画の視聴が完了したら実行される
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        //再度コインを獲得する
        endDialog_offline.DialogPanelActive(battleScene_offlineManager.timeRanking);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

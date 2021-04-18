using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobTitleAdvertising : MonoBehaviour
{
    //バナー
    public BannerView bannerView;
    //広告ユニットID用
    private string adUnitId;

    // Use this for initialization
    void Start()
    {
        //広告解除初期化
        //PlayerPrefs.DeleteKey("Unlock_WaitingRoomAdvertising");

        //タイトル広告解除初期値
        if (!PlayerPrefs.HasKey("Unlock_TitleAdvertising"))
        {
            PlayerPrefs.SetInt("Unlock_TitleAdvertising", 0);
            PlayerPrefs.Save();
        }
        //ゲーム開始前広告解除初期値Unlock_WaitingRoomAdvertising_offline
        if (!PlayerPrefs.HasKey("Unlock_WaitingRoomAdvertising"))
        {
            PlayerPrefs.SetInt("Unlock_WaitingRoomAdvertising", 0);
            PlayerPrefs.Save();
        }
        //ゲーム開始前広告解除初期値(オフライン)
        if (!PlayerPrefs.HasKey("Unlock_WaitingRoomAdvertising_offline"))
        {
            PlayerPrefs.SetInt("Unlock_WaitingRoomAdvertising_offline", 0);
            PlayerPrefs.Save();
        }

        // アプリID(不要)
        // Initialize the Google Mobile Ads SDK.
        //string appId = "あなたのアプリID";

        //アプリ起動時に必ず一回実行(ここでやるため他のスクリプトでやる必要なし)
        MobileAds.Initialize(initStatas => { });

        //広告解除していない場合かつオンライン時、RequestBanner()関数を呼ぶ
        if (PlayerPrefs.GetInt("Unlock_TitleAdvertising") == 0 && Application.internetReachability != NetworkReachability.NotReachable)
        {
            RequestBanner();
        }
    }
    private void RequestBanner()
    {
        // 広告ユニットID
        //iPhoneでの動作
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //テストID
            //adUnitId = "ca-app-pub-3940256099942544/2934735716";
            adUnitId = "ca-app-pub-8452025378548231/8071948989";
        }
        //Androidでの動作
        if (Application.platform == RuntimePlatform.Android)
        {
            //テストID
            //adUnitId = "ca-app-pub-3940256099942544/6300978111";
            adUnitId = "ca-app-pub-8452025378548231/5720506408";
        }

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);

    }
    // Update is called once per frame
    void Update()
    {
    }
}

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
        // アプリID(不要)
        // Initialize the Google Mobile Ads SDK.
        //string appId = "あなたのアプリID";

        //アプリ起動時に必ず一回実行(ここでやるため他のスクリプトでやる必要なし)
        MobileAds.Initialize(initStatas => { });
        //RequestBanner()関数を呼ぶ
        RequestBanner();
    }
    private void RequestBanner()
    {
        // 広告ユニットID
        //iPhoneでの動作
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            adUnitId = "ca-app-pub-3940256099942544/6300978111";
        }
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        // Create a 320x50 banner at the top of the screen.
        //bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

    }
    // Update is called once per frame
    void Update()
    {
    }
}

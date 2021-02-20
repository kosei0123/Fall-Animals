using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobTitleAdvertinsing : MonoBehaviour
{
    //バナー
    public BannerView bannerView;

    // Use this for initialization
    void Start()
    {
        // アプリID
        // Initialize the Google Mobile Ads SDK.
        //string appId = "あなたのアプリID";
        MobileAds.Initialize(initStatas => { });
        RequestBanner();
    }
    private void RequestBanner()
    {
        // 広告ユニットID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobWaitingRoomAdvertising : MonoBehaviour
{
    //バナー
    public BannerView bannerView;
    //広告ユニットID用
    private string adUnitId;

    // Use this for initialization
    void Start()
    {
        
        //アプリ起動時に必ず一回実行(ここでやるため他のスクリプトでやる必要なし)
        MobileAds.Initialize(initStatas => { });

        //広告解除していない場合、RequestBanner()関数を呼ぶ
        if (PlayerPrefs.GetInt("Unlock_WaitingRoomAdvertising") == 0)
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
            adUnitId = "ca-app-pub-8452025378548231/4247966035";
        }
        //Androidでの動作
        else if (Application.platform == RuntimePlatform.Android)
        {
            //テストID
            //adUnitId = "ca-app-pub-3940256099942544/6300978111";
            adUnitId = "ca-app-pub-8452025378548231/6706285016";
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

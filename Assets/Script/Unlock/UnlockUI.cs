using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnlockUI : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

    //コインの枚数を表示
    [SerializeField]
    private Text MyCoinText;
    //キャラクタースクロールビュー
    [SerializeField]
    private GameObject CharacterScrollView;
    //ステージスクロールビュー
    [SerializeField]
    private GameObject StageScrollView;
    //その他スクロールビュー
    [SerializeField]
    private GameObject OtherScrollView;

    //キリン
    //購入ボタン
    [SerializeField]
    private Button GiraffeBuyButton;
    //値段テキスト
    [SerializeField]
    private Text GiraffeBuyText;
    //購入完了パネル
    [SerializeField]
    private GameObject GiraffeBuyDonePanel;
    //象
    //購入ボタン
    [SerializeField]
    private Button ElephantBuyButton;
    //値段テキスト
    [SerializeField]
    private Text ElephantBuyText;
    //購入完了パネル
    [SerializeField]
    private GameObject ElephantBuyDonePanel;
    //虎
    //購入ボタン
    [SerializeField]
    private Button TigerBuyButton;
    //値段テキスト
    [SerializeField]
    private Text TigerBuyText;
    //購入完了パネル
    [SerializeField]
    private GameObject TigerBuyDonePanel;

    //タイトル広告解除
    //購入ボタン
    [SerializeField]
    private Button TitleAdvertisingBuyButton;
    //値段テキスト
    [SerializeField]
    private Text TitleAdvertisingBuyText;
    //購入完了パネル
    [SerializeField]
    private GameObject TitleAdvertisingBuyDonePanel;
    //ゲーム開始前広告解除
    //購入ボタン
    [SerializeField]
    private Button WaitingRoomAdvertisingBuyButton;
    //値段テキスト
    [SerializeField]
    private Text WaitingRoomAdvertisingBuyText;
    //購入完了パネル
    [SerializeField]
    private GameObject WaitingRoomAdvertisingBuyDonePanel;

    //BuyPanel
    [SerializeField]
    private GameObject BuyPanel;
    //BuyDonePanel
    [SerializeField]
    private GameObject BuyDonePanel;

    //どの動物をアンロックさせるか
    private string unlockName;

    //値段
    //キリン
    private int giraffePrice = 4000;
    //象
    private int elephantPrice = 500;
    //虎
    private int tigerPrice = 2000;
    //タイトル広告解除
    private int titleAdvertisingPrice = 20000;
    //ゲーム開始前広告解除
    private int waitingRoomAdvertisingPrice = 30000;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //初期表示はキャラクタースクロールビューを表示させておく
        CharacterScrollView.SetActive(true);
        StageScrollView.SetActive(false);
        OtherScrollView.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Buyボタンの押下可能条件
        CheckBuy();
        //表示値段の取得
        CheckTextPrice();
        //動物購入済みかを確認
        CheckBuyDone();

        //デバイスに保持されているコインの枚数を表示
        MyCoinText.text = PlayerPrefs.GetInt("myCoin").ToString("");
    }

    //Buyボタンの押下可能条件
    private void CheckBuy()
    {
        //キリン
        if (PlayerPrefs.GetInt("myCoin") >= giraffePrice && PlayerPrefs.GetInt("Unlock_Giraffe") == 0) GiraffeBuyButton.interactable = true;
        else { GiraffeBuyButton.interactable = false; }
        //象
        if (PlayerPrefs.GetInt("myCoin") >= elephantPrice && PlayerPrefs.GetInt("Unlock_Elephant") == 0) ElephantBuyButton.interactable = true;
        else { ElephantBuyButton.interactable = false; }
        //虎
        if (PlayerPrefs.GetInt("myCoin") >= tigerPrice && PlayerPrefs.GetInt("Unlock_Tiger") == 0) TigerBuyButton.interactable = true;
        else { TigerBuyButton.interactable = false; }

        //タイトル広告解除
        if (PlayerPrefs.GetInt("myCoin") >= titleAdvertisingPrice && PlayerPrefs.GetInt("Unlock_TitleAdvertising") == 0) TitleAdvertisingBuyButton.interactable = true;
        else { TitleAdvertisingBuyButton.interactable = false; }
        //ゲーム開始前広告解除
        if (PlayerPrefs.GetInt("myCoin") >= waitingRoomAdvertisingPrice && PlayerPrefs.GetInt("Unlock_WaitingRoomAdvertising") == 0) WaitingRoomAdvertisingBuyButton.interactable = true;
        else { WaitingRoomAdvertisingBuyButton.interactable = false; }
    }

    //表示値段の取得
    private void CheckTextPrice()
    {
        //キリン
        GiraffeBuyText.text = giraffePrice.ToString("");
        //象
        ElephantBuyText.text = elephantPrice.ToString("");
        //虎
        TigerBuyText.text = tigerPrice.ToString("");

        //タイトル広告解除
        TitleAdvertisingBuyText.text = titleAdvertisingPrice.ToString("");
        //ゲーム開始前広告解除
        WaitingRoomAdvertisingBuyText.text = waitingRoomAdvertisingPrice.ToString("");
    }

    //動物購入済みかを確認
    private void CheckBuyDone()
    {
        //キリン
        if (PlayerPrefs.GetInt("Unlock_Giraffe") == 1) GiraffeBuyDonePanel.SetActive(true);
        else { GiraffeBuyDonePanel.SetActive(false); }
        //象
        if (PlayerPrefs.GetInt("Unlock_Elephant") == 1) ElephantBuyDonePanel.SetActive(true);
        else { ElephantBuyDonePanel.SetActive(false); }
        //虎
        if (PlayerPrefs.GetInt("Unlock_Tiger") == 1) TigerBuyDonePanel.SetActive(true);
        else { TigerBuyDonePanel.SetActive(false); }

        //タイトル広告解除
        if (PlayerPrefs.GetInt("Unlock_TitleAdvertising") == 1) TitleAdvertisingBuyDonePanel.SetActive(true);
        else { TitleAdvertisingBuyDonePanel.SetActive(false); }
        //ゲーム開始前広告解除
        if (PlayerPrefs.GetInt("Unlock_WaitingRoomAdvertising") == 1) WaitingRoomAdvertisingBuyDonePanel.SetActive(true);
        else { WaitingRoomAdvertisingBuyDonePanel.SetActive(false); }
    }


    //CharacterButtonボタンを押した際の挙動
    public void OnClick_CharacterButton()
    {
        CharacterScrollView.SetActive(true);
        StageScrollView.SetActive(false);
        OtherScrollView.SetActive(false);
        //SEの使用
        soundManager.SEManager("Button_sound1");
    }

    //StageButtonボタンを押した際の挙動
    public void OnClick_StageButton()
    {
        CharacterScrollView.SetActive(false);
        StageScrollView.SetActive(true);
        OtherScrollView.SetActive(false);
        //SEの使用
        soundManager.SEManager("Button_sound1");
    }

    //OtherButtonボタンを押した際の挙動
    public void OnClick_OtherButton()
    {
        CharacterScrollView.SetActive(false);
        StageScrollView.SetActive(false);
        OtherScrollView.SetActive(true);
        //SEの使用
        soundManager.SEManager("Button_sound1");
    }

    //GirrafeBuyButtonボタンを押した際の挙動
    public void OnClick_GirrafeBuyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //象を指定する
        unlockName = "Girrafe";
        //BuyPanelを表示
        BuyPanel.SetActive(true);
    }

    //ElephantBuyButtonボタンを押した際の挙動
    public void OnClick_ElephantBuyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //象を指定する
        unlockName = "Elephant";
        //BuyPanelを表示
        BuyPanel.SetActive(true);
    }

    //TigerBuyButtonボタンを押した際の挙動
    public void OnClick_TigerBuyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //虎を指定する
        unlockName = "Tiger";
        //BuyPanelを表示
        BuyPanel.SetActive(true);
    }

    //TitleAdvertisingBuyButtonを押した際の挙動
    public void OnClick_TitleAdvertisingBuyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //タイトル広告を指定する
        unlockName = "TitleAdvertising";
        //BuyPanelを表示
        BuyPanel.SetActive(true);
    }

    //WaitingRoomAdvertisingBuyButtonを押した際の挙動
    public void OnClick_WaitingRoomAdvertisingBuyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //ゲームまえ広告を指定する
        unlockName = "WaitingRoomAdvertising";
        //BuyPanelを表示
        BuyPanel.SetActive(true);
    }

    //BuyPanelにてYesButtonボタンを押した際の挙動
    public void OnClick_YesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //アンロックする
        switch (unlockName)
        {
            //キリン
            case "Girrafe":
                //アンロック解除
                PlayerPrefs.SetInt("Unlock_Giraffe", 1);
                //コインを減少させる
                PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - giraffePrice);
                break;
            //象
            case "Elephant":
                //アンロック解除
                PlayerPrefs.SetInt("Unlock_Elephant", 1);
                //コインを減少させる
                PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - elephantPrice);
                break;
            //虎
            case "Tiger":
                //アンロック解除
                PlayerPrefs.SetInt("Unlock_Tiger", 1);
                //コインを減少させる
                PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - tigerPrice);
                break;
            //タイトル広告解除
            case "TitleAdvertising":
                //アンロック解除
                PlayerPrefs.SetInt("Unlock_TitleAdvertising", 1);
                //コインを減少させる
                PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - titleAdvertisingPrice);
                break;
            //ゲーム開始前広告解除
            case "WaitingRoomAdvertising":
                //アンロック解除
                PlayerPrefs.SetInt("Unlock_WaitingRoomAdvertising", 1);
                //コインを減少させる
                PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - waitingRoomAdvertisingPrice);
                break;
            default:
                break;
        }

        //BuyPanelを非表示
        BuyPanel.SetActive(false);
        //BuyDonePanelを表示
        BuyDonePanel.SetActive(true);
    }

    //BuyPanelにてNoButtonボタンを押した際の挙動
    public void OnClick_NoButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //BuyPanelを非表示
        BuyPanel.SetActive(false);
    }

    //BuyDonePanelにてBuyDoneYesButtonボタンを押した際の挙動
    public void OnClick_BuyDoneYesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //BuyDonePanelを表示にする
        BuyDonePanel.SetActive(false);
    }

    //UnityIAPMoveボタンを押した時の挙動
    public void OnClick_UnityIAPMoveButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("UnityIAP");
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

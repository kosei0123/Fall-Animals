using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockStageUI : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

    //ステージ4
    //購入ボタン
    [SerializeField]
    private Button Stage4BuyButton;
    //値段テキスト
    [SerializeField]
    private Text Stage4BuyText;
    //購入完了パネル
    [SerializeField]
    private GameObject Stage4BuyDonePanel;
    //ステージ5
    //購入ボタン
    [SerializeField]
    private Button Stage5BuyButton;
    //値段テキスト
    [SerializeField]
    private Text Stage5BuyText;
    //購入完了パネル
    [SerializeField]
    private GameObject Stage5BuyDonePanel;
    //ステージ6
    //購入ボタン
    [SerializeField]
    private Button Stage6BuyButton;
    //値段テキスト
    [SerializeField]
    private Text Stage6BuyText;
    //購入完了パネル
    [SerializeField]
    private GameObject Stage6BuyDonePanel;

    //BuyStagePanel
    [SerializeField]
    private GameObject BuyStagePanel;
    //BuyStageDonePanel
    [SerializeField]
    private GameObject BuyStageDonePanel;

    //どのステージをアンロックさせるか
    [HideInInspector]
    public static string unlockStageName;

    //値段
    //ステージ4
    private int stage4Price = 500;
    //ステージ5
    private int stage5Price = 300;
    //ステージ6
    private int stage6Price = 500;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Buyボタンの押下可能条件
        CheckBuy();
        //表示値段の取得
        CheckTextPrice();
        //購入済みかを確認
        CheckBuyDone();
    }

    //Buyボタンの押下可能条件
    private void CheckBuy()
    {
        //ステージ4
        if (PlayerPrefs.GetInt("myCoin") >= stage4Price && PlayerPrefs.GetInt("Unlock_Stage4") == 0) Stage4BuyButton.interactable = true;
        else { Stage4BuyButton.interactable = false; }
        //ステージ5
        if (PlayerPrefs.GetInt("myCoin") >= stage5Price && PlayerPrefs.GetInt("Unlock_Stage5") == 0) Stage5BuyButton.interactable = true;
        else { Stage5BuyButton.interactable = false; }
        //ステージ6
        if (PlayerPrefs.GetInt("myCoin") >= stage6Price && PlayerPrefs.GetInt("Unlock_Stage6") == 0) Stage6BuyButton.interactable = true;
        else { Stage6BuyButton.interactable = false; }
    }

    //表示値段の取得
    private void CheckTextPrice()
    {
        //ステージ4
        Stage4BuyText.text = stage4Price.ToString("");
        //ステージ5
        Stage5BuyText.text = stage5Price.ToString("");
        //ステージ6
        Stage6BuyText.text = stage6Price.ToString("");
    }

    //動物購入済みかを確認
    private void CheckBuyDone()
    {
        //ステージ4
        if (PlayerPrefs.GetInt("Unlock_Stage4") == 1) Stage4BuyDonePanel.SetActive(true);
        else { Stage4BuyDonePanel.SetActive(false); }
        //ステージ5
        if (PlayerPrefs.GetInt("Unlock_Stage5") == 1) Stage5BuyDonePanel.SetActive(true);
        else { Stage5BuyDonePanel.SetActive(false); }
        //ステージ6
        if (PlayerPrefs.GetInt("Unlock_Stage6") == 1) Stage6BuyDonePanel.SetActive(true);
        else { Stage6BuyDonePanel.SetActive(false); }
    }

    //Stage4のプレビュー確認
    public void OnClick_Stage4CheckButton()
    {
        unlockStageName = "Stage4";
        SceneManager.LoadScene("Preview");
    }

    //Stage5のプレビュー確認
    public void OnClick_Stage5CheckButton()
    {
        unlockStageName = "Stage5";
        SceneManager.LoadScene("Preview");
    }

    //Stage6のプレビュー確認
    public void OnClick_Stage6CheckButton()
    {
        unlockStageName = "Stage6";
        SceneManager.LoadScene("Preview");
    }

    //Stage4BuyButtonボタンを押した際の挙動
    public void OnClick_Stage4BuyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //Stage4を指定する
        unlockStageName = "Stage4";
        //BuyPanelを表示
        BuyStagePanel.SetActive(true);
    }

    //Stage5BuyButtonボタンを押した際の挙動
    public void OnClick_Stage5BuyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //Stage5を指定する
        unlockStageName = "Stage5";
        //BuyPanelを表示
        BuyStagePanel.SetActive(true);
    }

    //Stage6BuyButtonボタンを押した際の挙動
    public void OnClick_Stage6BuyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //Stage5を指定する
        unlockStageName = "Stage6";
        //BuyPanelを表示
        BuyStagePanel.SetActive(true);
    }

    //BuyStagePanelにてYesButtonボタンを押した際の挙動
    public void OnClick_YesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //アンロックする
        switch (unlockStageName)
        {
            //ステージ4
            case "Stage4":
                //アンロック解除
                PlayerPrefs.SetInt("Unlock_Stage4", 1);
                //ステージ状態をONにする
                PlayerPrefs.SetString("Unlock_Stage4_ON", "true");
                //コインを減少させる
                PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - stage4Price);
                break;
            //ステージ5
            case "Stage5":
                PlayerPrefs.SetInt("Unlock_Stage5", 1);
                PlayerPrefs.SetString("Unlock_Stage5_ON", "true");
                PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - stage5Price);
                break;
            //ステージ6
            case "Stage6":
                PlayerPrefs.SetInt("Unlock_Stage6", 1);
                PlayerPrefs.SetString("Unlock_Stage6_ON", "true");
                PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - stage6Price);
                break;
            default:
                break;
        }

        //BuyPanelを非表示
        BuyStagePanel.SetActive(false);
        //BuyStageDonePanelを表示
        BuyStageDonePanel.SetActive(true);
    }

    //BuyStagePanelにてNoButtonボタンを押した際の挙動
    public void OnClick_NoButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //BuyStagePanelを非表示
        BuyStagePanel.SetActive(false);
    }

    //BuyStageDonePanelにてBuyStageDoneYesButtonボタンを押した際の挙動
    public void OnClick_BuyStageDoneYesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //BuyDonePanelを表示にする
        BuyStageDonePanel.SetActive(false);
    }

    
}

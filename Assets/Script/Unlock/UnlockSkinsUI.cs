using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnlockSkinsUI : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //UnlockUIのスクリプトの関数使用
    UnlockUI unlock;

    //キャンディ
    //購入完了パネル
    [SerializeField]
    private GameObject CandyBuyDonePanel;
    //結果イメージ
    [SerializeField]
    private GameObject CandyResultImage;
    //王冠
    //購入完了パネル
    [SerializeField]
    private GameObject CrownBuyDonePanel;
    //結果イメージ
    [SerializeField]
    private GameObject CrownResultImage;
    //雲
    //購入完了パネル
    [SerializeField]
    private GameObject CloudBuyDonePanel;
    //結果イメージ
    [SerializeField]
    private GameObject CloudResultImage;
    //マップピン
    //購入完了パネル
    [SerializeField]
    private GameObject MappinBuyDonePanel;
    //結果イメージ
    [SerializeField]
    private GameObject MappinResultImage;
    //クリスタル
    //購入完了パネル
    [SerializeField]
    private GameObject CrystalBuyDonePanel;
    //結果イメージ
    [SerializeField]
    private GameObject CrystalResultImage;

    //ガチャ
    //開始ボタン
    [SerializeField]
    private Button SkinsCapsuleStartButton;
    //テキスト
    [SerializeField]
    private Text SkinsCapsuleStartText;

    //CapsuleStartPanel
    [SerializeField]
    private GameObject CapsuleStartPanel;

    //ガチャ結果表示パネル
    [SerializeField]
    private GameObject CapsuleResultPanel;
    //スキンイメージの親オブジェクト
    [SerializeField]
    private GameObject CapsuleResultChildPanel;

    //どのスキンをアンロックするか
    private int unlockSkinsName;

    //値段
    //ガチャ
    private int skinsCapsuleStartPrice = 50;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //UnlockUIのスクリプトの関数使用
        unlock = this.gameObject.GetComponent<UnlockUI>();
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
    }

    //ガチャ開始ボタンの押下可能条件
    private void CheckBuy()
    {
        if (PlayerPrefs.GetInt("myCoin") >= skinsCapsuleStartPrice) SkinsCapsuleStartButton.interactable = true;
        else { SkinsCapsuleStartButton.interactable = false; }
    }

    //表示値段の取得
    private void CheckTextPrice()
    {
        SkinsCapsuleStartText.text = skinsCapsuleStartPrice.ToString("") + "\n" + "ガチャを回す";
    }

    //獲得済みかを確認
    private void CheckBuyDone()
    {
        //キャンディ
        if (PlayerPrefs.GetInt("Unlock_Candy") == 1) CandyBuyDonePanel.SetActive(true);
        else { CandyBuyDonePanel.SetActive(false); }
        //王冠
        if (PlayerPrefs.GetInt("Unlock_Crown") == 1) CrownBuyDonePanel.SetActive(true);
        else { CrownBuyDonePanel.SetActive(false); }
        //雲
        if (PlayerPrefs.GetInt("Unlock_Cloud") == 1) CloudBuyDonePanel.SetActive(true);
        else { CloudBuyDonePanel.SetActive(false); }
        //マップピン
        if (PlayerPrefs.GetInt("Unlock_Mappin") == 1) MappinBuyDonePanel.SetActive(true);
        else { MappinBuyDonePanel.SetActive(false); }
        //クリスタル
        if (PlayerPrefs.GetInt("Unlock_Crystal") == 1) CrystalBuyDonePanel.SetActive(true);
        else { CrystalBuyDonePanel.SetActive(false); }
    }

    //SkinsCapsuleStartButtonボタンを押した際の挙動
    public void OnClick_SkinsCapsuleStartButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //CapsuleStartPanelを表示
        CapsuleStartPanel.SetActive(true);
    }

    //CapsuleStartPanelにてYesButtonボタンを押した際の挙動
    public void OnClick_YesButton()
    {
        //ランダム値をいれる
        unlockSkinsName = Random.Range(1,6);

        //ガチャ結果表示パネルの表示
        CapsuleResultPanel.SetActive(true);

        switch (unlockSkinsName)
        {
            case 1:
                PlayerPrefs.SetInt("Unlock_Candy", 1);
                CandyResultImage.SetActive(true);
                break;
            case 2:
                PlayerPrefs.SetInt("Unlock_Crown", 1);
                CrownResultImage.SetActive(true);
                break;
            case 3:
                PlayerPrefs.SetInt("Unlock_Cloud", 1);
                CloudResultImage.SetActive(true);
                break;
            case 4:
                PlayerPrefs.SetInt("Unlock_Mappin", 1);
                MappinResultImage.SetActive(true);
                break;
            case 5:
                PlayerPrefs.SetInt("Unlock_Crystal", 1);
                CrystalResultImage.SetActive(true);
                break;
            default:
                break;
        }

        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - skinsCapsuleStartPrice);
    }

    //CapsuleStartPanelにてNoButtonボタンを押した際の挙動
    public void OnClick_NoButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //CapsuleStartPanelを非表示
        CapsuleStartPanel.SetActive(false);
    }

    //CapsuleResultYesButton押下時
    public void OnClick_CapsuleResultYesButton()
    {
        //スキンイメージの全非表示
        foreach (Transform childTransform in CapsuleResultChildPanel.transform)
        {
            childTransform.gameObject.SetActive(false);
        }
        //ガチャ結果表示パネルの非表示
        CapsuleResultPanel.SetActive(false);
        //CapsuleStartPanelを非表示
        CapsuleStartPanel.SetActive(false);
    }
}

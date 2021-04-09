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
    //購入ボタン
    [SerializeField]
    private Button CandyBuyButton;
    //値段テキスト
    [SerializeField]
    private Text CandyBuyText;
    //購入完了パネル
    [SerializeField]
    private GameObject CandyBuyDonePanel;

    //BuyPanel
    [SerializeField]
    private GameObject BuyPanel;

    //値段
    //キャンディ
    [HideInInspector]
    public int candyPrice = 300;

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

    //Buyボタンの押下可能条件
    private void CheckBuy()
    {
        //キャンディ
        if (PlayerPrefs.GetInt("myCoin") >= candyPrice && PlayerPrefs.GetInt("Unlock_Candy") == 0) CandyBuyButton.interactable = true;
        else { CandyBuyButton.interactable = false; }
    }

    //表示値段の取得
    private void CheckTextPrice()
    {
        //キャンディ
        CandyBuyText.text = candyPrice.ToString("");
    }

    //購入済みかを確認
    private void CheckBuyDone()
    {
        //キャンディ
        if (PlayerPrefs.GetInt("Unlock_Candy") == 1) CandyBuyDonePanel.SetActive(true);
        else { CandyBuyDonePanel.SetActive(false); }
    }

    //CandyBuyButtonボタンを押した際の挙動
    public void OnClick_CandyBuyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //キャンディを指定する
        unlock.unlockName = "Candy";
        //BuyPanelを表示
        BuyPanel.SetActive(true);
    }
}

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

    //BuyPanel
    [SerializeField]
    private GameObject BuyPanel;
    //BuyDonePanel
    [SerializeField]
    private GameObject BuyDonePanel;

    //どの動物をアンロックさせるか
    private string unlockAnimal;

    //値段
    //象
    private int elephantPrice = 1000;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //初期表示はキャラクタースクロールビューを表示させておく
        CharacterScrollView.SetActive(true);
        StageScrollView.SetActive(false);

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
        //象
        if (PlayerPrefs.GetInt("myCoin") > elephantPrice && PlayerPrefs.GetInt("Unlock_Elephant") == 0)
        {
            ElephantBuyButton.interactable = true;
        }
        else
        {
            ElephantBuyButton.interactable = false;
        }
    }

    //表示値段の取得
    private void CheckTextPrice()
    {
        //象
        ElephantBuyText.text = elephantPrice.ToString("");
    }

    //動物購入済みかを確認
    private void CheckBuyDone()
    {
        //象
        if (PlayerPrefs.GetInt("Unlock_Elephant") == 1)
        {
            ElephantBuyDonePanel.SetActive(true);
        }
        else
        {
            ElephantBuyDonePanel.SetActive(false);
        }
    }


    //CharacterButtonボタンを押した際の挙動
    public void OnClick_CharacterButton()
    {
        CharacterScrollView.SetActive(true);
        StageScrollView.SetActive(false);
        //SEの使用
        soundManager.SEManager("Button_sound1");
    }

    //StageButtonボタンを押した際の挙動
    public void OnClick_StageButton()
    {
        CharacterScrollView.SetActive(false);
        StageScrollView.SetActive(true);
        //SEの使用
        soundManager.SEManager("Button_sound1");
    }

    //ElephantBuyButtonボタンを押した際の挙動
    public void OnClick_ElephantBuyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //象を指定する
        unlockAnimal = "Elephant";
        //BuyPanelを表示
        BuyPanel.SetActive(true);
    }

    //BuyPanelにてYesButtonボタンを押した際の挙動
    public void OnClick_YesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //アンロックする
        switch (unlockAnimal)
        {
            //象
            case "Elephant":
                //アンロック解除
                PlayerPrefs.SetInt("Unlock_Elephant", 1);
                //コインを減少させる
                PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - elephantPrice);
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

    //メニューに戻るボタン押下
    public void OnClick_MenuButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("Menu");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeppenShopUI : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

    //コインの枚数を表示
    [SerializeField]
    private Text MyCoinText;

    //BuyPriceTextの表示
    [SerializeField]
    private Text BuyPriceText;
    private int BuyPriceInt = 0;

    //商品の詳細説明文
    [SerializeField]
    private Text ShopDetailsText;
    //商品の詳細イメージの親オブジェクト
    [SerializeField]
    private GameObject ShopDetailsPanel;
    //Updownの詳細イメージ表示
    [SerializeField]
    private GameObject UpdownImage;

    //DShoes
    //ボタン表示
    [SerializeField]
    private GameObject DShoesButton;
    //値段
    private int DShoesPrice = 10;
    //値段表示
    [SerializeField]
    private Text DShoesPriceText;
    //DShoesの詳細イメージ表示
    [SerializeField]
    private GameObject DShoesImage;
    //trueであれば合計金額に追加
    private bool DShoesFlag = false;
    //DShoe2
    //ボタン表示
    [SerializeField]
    private GameObject DShoes2Button;
    //値段
    private int DShoes2Price = 80;
    //値段表示
    [SerializeField]
    private Text DShoes2PriceText;
    //trueであれば合計金額に追加
    private bool DShoes2Flag = false;
    //DShoe3
    //ボタン表示
    [SerializeField]
    private GameObject DShoes3Button;
    //値段
    private int DShoes3Price = 200;
    //値段表示
    [SerializeField]
    private Text DShoes3PriceText;
    //trueであれば合計金額に追加
    private bool DShoes3Flag = false;

    //MinusTime
    //ボタン表示
    [SerializeField]
    private GameObject MinusTimeButton;
    //値段
    private int MinusTimePrice = 10;
    //値段表示
    [SerializeField]
    private Text MinusTimePriceText;
    //trueであれば合計金額に追加
    private bool MinusTimeFlag = false;
    //MinusTime2
    //ボタン表示
    [SerializeField]
    private GameObject MinusTime2Button;
    //値段
    private int MinusTime2Price = 10;
    //値段表示
    [SerializeField]
    private Text MinusTime2PriceText;
    //trueであれば合計金額に追加
    private bool MinusTime2Flag = false;
    //MinusTime3
    //ボタン表示
    [SerializeField]
    private GameObject MinusTime3Button;
    //値段
    private int MinusTime3Price = 10;
    //値段表示
    [SerializeField]
    private Text MinusTime3PriceText;
    //trueであれば合計金額に追加
    private bool MinusTime3Flag = false;
    //実際にバトルでマイナスする時間の合計
    public static float MinusTimeRealTotal = 0;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //SEの使用(猫の鳴き声)
        soundManager.SEManager("CatNakigoe_sound1");

        //ショップリストの追加(ランダム)
        for (int i = 0; i < 3; i++)
        {
            int[] randomShopList = new int[3];
            randomShopList[i] = Random.Range(0, 6);

            //商品の表示
            switch (randomShopList[i])
            {
                case 0:
                    if (PlayerPrefs.GetInt("DShoesFlag") == 0)
                    {
                        //ボタンの表示
                        DShoesButton.SetActive(true);
                        //値段の表示
                        DShoesPriceText.text = DShoesPrice.ToString("");
                    }
                    break;
                case 1:
                    if (PlayerPrefs.GetInt("DShoes2Flag") == 0)
                    {
                        //ボタンの表示
                        DShoes2Button.SetActive(true);
                        //値段の表示
                        DShoes2PriceText.text = DShoes2Price.ToString("");
                    }
                    break;
                case 2:
                    if (PlayerPrefs.GetInt("DShoes3Flag") == 0)
                    {
                        //ボタンの表示
                        DShoes3Button.SetActive(true);
                        //値段の表示
                        DShoes3PriceText.text = DShoes3Price.ToString("");
                    }
                    break;
                case 3:
                    //ボタンの表示
                    MinusTimeButton.SetActive(true);
                    //値段の表示
                    MinusTimePriceText.text = MinusTimePrice.ToString("");
                    break;
                case 4:
                    //ボタンの表示
                    MinusTime2Button.SetActive(true);
                    //値段の表示
                    MinusTime2PriceText.text = MinusTime2Price.ToString("");
                    break;
                case 5:
                    //ボタンの表示
                    MinusTime3Button.SetActive(true);
                    //値段の表示
                    MinusTime3PriceText.text = MinusTime3Price.ToString("");
                    break;
                default:
                    break;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        //合計値段テキスト表示
        BuyPriceText.text = "計：" + BuyPriceInt.ToString("") + "コイン";
        //マイコインの表示
        MyCoinText.text = PlayerPrefs.GetInt("myCoin").ToString("");
    }

    //DShoesボタンを押した時
    public void OnClick_DShoesButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (DShoesFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "Dシューズ\n\n移動速度+5";
            //DShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += DShoesPrice;
            //ボタンの色変更
            DShoesButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            DShoesFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= DShoesPrice;
            DShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoesFlag = false;
        }
    }

    //DShoes2ボタンを押した時
    public void OnClick_DShoes2Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (DShoes2Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "Dシューズ2\n\n移動速度+10";
            //DShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += DShoes2Price;
            //ボタンの色変更
            DShoes2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            DShoes2Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= DShoes2Price;
            DShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes2Flag = false;
        }
    }

    //DShoes3ボタンを押した時
    public void OnClick_DShoes3Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (DShoes3Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "Dシューズ3\n\n移動速度+15";
            //DShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoesImage.SetActive(true);
            //値段の追加
            BuyPriceInt += DShoes3Price;
            //ボタンの色変更
            DShoes3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            DShoes3Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= DShoes3Price;
            DShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes3Flag = false;
        }
    }

    //MinusTimeButtonボタンを押した時
    public void OnClick_MinusTimeButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (MinusTimeFlag == false)
        {
            //詳細表示
            ShopDetailsText.text = "マイナスタイム\n\n時間-5";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += MinusTimePrice;
            //ボタンの色変更
            MinusTimeButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            MinusTimeFlag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= MinusTimePrice;
            MinusTimeButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            MinusTimeFlag = false;
        }
    }

    //MinusTime2Buttonボタンを押した時
    public void OnClick_MinusTime2Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (MinusTime2Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "マイナスタイム2\n\n時間-20";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += MinusTime2Price;
            //ボタンの色変更
            MinusTime2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            MinusTime2Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= MinusTime2Price;
            MinusTime2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            MinusTime2Flag = false;
        }
    }

    //MinusTime3Buttonボタンを押した時
    public void OnClick_MinusTime3Button()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");

        //trueであれば合計金額に追加
        if (MinusTime3Flag == false)
        {
            //詳細表示
            ShopDetailsText.text = "マイナスタイム3\n\n時間-50";
            //MinusTimeのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            UpdownImage.SetActive(true);
            //値段の追加
            BuyPriceInt += MinusTime3Price;
            //ボタンの色変更
            MinusTime3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //合計金額に追加している状態
            MinusTime3Flag = true;
        }
        else
        {
            ShopDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            BuyPriceInt -= MinusTime3Price;
            MinusTime3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            MinusTime3Flag = false;
        }
    }

    //TeppenBattleSceneへすすむ
    public void OnClick_CloseButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        if (PlayerPrefs.GetInt("myCoin") >= BuyPriceInt)
        {
            //購入によるお金の減少
            PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") - BuyPriceInt);
            //購入アイテム
            //DShoes
            if (DShoesFlag == true) PlayerPrefs.SetInt("DShoesFlag", 1);
            if (DShoes2Flag == true) PlayerPrefs.SetInt("DShoes2Flag", 1);
            if (DShoes3Flag == true) PlayerPrefs.SetInt("DShoes3Flag", 1);
            //MinusTime
            if (MinusTimeFlag == true) MinusTimeRealTotal += 5.0f;
            if (MinusTime2Flag == true) MinusTimeRealTotal += 20.0f;
            if (MinusTime3Flag == true) MinusTimeRealTotal += 50.0f;

            //画面回転の制御
            if (Screen.width > Screen.height) Screen.autorotateToPortrait = false;
            else
            {
                Screen.autorotateToLandscapeRight = false;
                Screen.autorotateToLandscapeLeft = false;
            }

            //シーン移動
            SceneManager.LoadScene("TeppenBattleScene");
        }
        else
        {

        }
        
    }
}

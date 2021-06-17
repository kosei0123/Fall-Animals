using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeppenMenuShopList : MonoBehaviour
{
    //商品の詳細説明文
    [SerializeField]
    private Text ItemDetailsText;
    //商品の詳細イメージの親オブジェクト
    [SerializeField]
    private GameObject ShopDetailsPanel;

    //DShoes
    //ボタン表示
    [SerializeField]
    private GameObject DShoesButton;
    //DShoesの詳細イメージ表示
    [SerializeField]
    private GameObject DShoesImage;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool DShoesUseFlag = false;
    //DShoes2
    //ボタン表示
    [SerializeField]
    private GameObject DShoes2Button;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool DShoes2UseFlag = false;
    //DShoes3
    //ボタン表示
    [SerializeField]
    private GameObject DShoes3Button;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool DShoes3UseFlag = false;

    //JShoes
    //ボタン表示
    [SerializeField]
    private GameObject JShoesButton;
    //JShoesの詳細イメージ表示
    [SerializeField]
    private GameObject JShoesImage;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool JShoesUseFlag = false;
    //JShoes2
    //ボタン表示
    [SerializeField]
    private GameObject JShoes2Button;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool JShoes2UseFlag = false;
    //JShoes3
    //ボタン表示
    [SerializeField]
    private GameObject JShoes3Button;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool JShoes3UseFlag = false;

    //AShoes
    //ボタン表示
    [SerializeField]
    private GameObject AShoesButton;
    //AShoesの詳細イメージ表示
    [SerializeField]
    private GameObject AShoesImage;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool AShoesUseFlag = false;
    //AShoes2
    //ボタン表示
    [SerializeField]
    private GameObject AShoes2Button;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool AShoes2UseFlag = false;
    //AShoes3
    //ボタン表示
    [SerializeField]
    private GameObject AShoes3Button;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool AShoes3UseFlag = false;

    //SuperHand
    //ボタン表示
    [SerializeField]
    private GameObject SuperHandButton;
    //SuperHandの詳細イメージ表示
    [SerializeField]
    private GameObject SuperHandImage;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool SuperHandUseFlag = false;

    //JWing
    //ボタン表示
    [SerializeField]
    private GameObject JWingButton;
    //JWingの詳細イメージ表示
    [SerializeField]
    private GameObject JWingImage;
    //trueであれば合計金額に追加
    [HideInInspector]
    public static bool JWingUseFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //ボタンの表示
        //DShoes
        if (PlayerPrefs.GetInt("DShoesFlag") == 1)
        {
            DShoesButton.SetActive(true);
            DShoesUseFlag = false;
        }
        if (PlayerPrefs.GetInt("DShoes2Flag") == 1)
        {
            DShoes2Button.SetActive(true);
            DShoes2UseFlag = false;
        }
        if (PlayerPrefs.GetInt("DShoes3Flag") == 1)
        {
            DShoes3Button.SetActive(true);
            DShoes3UseFlag = false;
        }
        //JShoes
        if (PlayerPrefs.GetInt("JShoesFlag") == 1)
        {
            JShoesButton.SetActive(true);
            JShoesUseFlag = false;
        }
        if (PlayerPrefs.GetInt("JShoes2Flag") == 1)
        {
            JShoes2Button.SetActive(true);
            JShoes2UseFlag = false;
        }
        if (PlayerPrefs.GetInt("JShoes3Flag") == 1)
        {
            JShoes3Button.SetActive(true);
            JShoes3UseFlag = false;
        }
        //AShoes
        if (PlayerPrefs.GetInt("AShoesFlag") == 1)
        {
            AShoesButton.SetActive(true);
            AShoesUseFlag = false;
        }
        if (PlayerPrefs.GetInt("AShoes2Flag") == 1)
        {
            AShoes2Button.SetActive(true);
            AShoes2UseFlag = false;
        }
        if (PlayerPrefs.GetInt("AShoes3Flag") == 1)
        {
            AShoes3Button.SetActive(true);
            AShoes3UseFlag = false;
        }
        //SuperHand
        if (PlayerPrefs.GetInt("SuperHandFlag") == 1)
        {
            SuperHandButton.SetActive(true);
            SuperHandUseFlag = false;
        }
        //JWing
        if (PlayerPrefs.GetInt("JWingFlag") == 1)
        {
            JWingButton.SetActive(true);
            JWingUseFlag = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Dシューズ
    public void OnClick_DShoesButton()
    {
        if (DShoesUseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "ダッシューズ\n\n移動速度+5";
            //DShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoesImage.SetActive(true);
            //ボタンの色変更
            DShoesButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            DShoesUseFlag = true;
            //その他シューズを使用変更する
            DShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes2UseFlag = false;
            DShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes3UseFlag = false;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoesUseFlag = false;
        }
    }
    //Dシューズ2
    public void OnClick_DShoes2Button()
    {
        if (DShoes2UseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "ダッシューズ2\n\n移動速度+10";
            //DShoes2のイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoesImage.SetActive(true);
            //ボタンの色変更
            DShoes2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            DShoes2UseFlag = true;
            //その他シューズを使用変更する
            DShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoesUseFlag = false;
            DShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes3UseFlag = false;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes2UseFlag = false;
        }
    }
    //Dシューズ3
    public void OnClick_DShoes3Button()
    {
        if (DShoes3UseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "ダッシューズ3\n\n移動速度+15";
            //DShoes3のイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoesImage.SetActive(true);
            //ボタンの色変更
            DShoes3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            DShoes3UseFlag = true;
            //その他シューズを使用変更する
            DShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoesUseFlag = false;
            DShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes2UseFlag = false;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            DShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes3UseFlag = false;
        }
    }

    //Jシューズ
    public void OnClick_JShoesButton()
    {
        if (JShoesUseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "ジャンシューズ\n\nジャンプ力+2";
            //JShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JShoesImage.SetActive(true);
            //ボタンの色変更
            JShoesButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            JShoesUseFlag = true;
            //その他シューズを使用変更する
            JShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoes2UseFlag = false;
            JShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoes3UseFlag = false;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoesUseFlag = false;
        }
    }
    //Jシューズ2
    public void OnClick_JShoes2Button()
    {
        if (JShoes2UseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "ジャンシューズ2\n\nジャンプ力+4";
            //JShoes2のイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JShoesImage.SetActive(true);
            //ボタンの色変更
            JShoes2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            JShoes2UseFlag = true;
            //その他シューズを使用変更する
            JShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoesUseFlag = false;
            JShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoes3UseFlag = false;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoes2UseFlag = false;
        }
    }
    //Jシューズ3
    public void OnClick_JShoes3Button()
    {
        if (JShoes3UseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "ジャンシューズ3\n\nジャンプ力+6";
            //JShoes3のイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JShoesImage.SetActive(true);
            //ボタンの色変更
            JShoes3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            JShoes3UseFlag = true;
            //その他シューズを使用変更する
            JShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoesUseFlag = false;
            JShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoes2UseFlag = false;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JShoes3UseFlag = false;
        }
    }

    //Aシューズ
    public void OnClick_AShoesButton()
    {
        if (AShoesUseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "空ダッシューズ\n\n空中移動速度+5";
            //AShoesのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            AShoesImage.SetActive(true);
            //ボタンの色変更
            AShoesButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            AShoesUseFlag = true;
            //その他シューズを使用変更する
            AShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoes2UseFlag = false;
            AShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoes3UseFlag = false;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            AShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoesUseFlag = false;
        }
    }
    //Aシューズ2
    public void OnClick_AShoes2Button()
    {
        if (AShoes2UseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "空ダッシューズ2\n\n空中移動速度+10";
            //AShoes2のイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            AShoesImage.SetActive(true);
            //ボタンの色変更
            AShoes2Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            AShoes2UseFlag = true;
            //その他シューズを使用変更する
            AShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoesUseFlag = false;
            AShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoes3UseFlag = false;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            AShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoes2UseFlag = false;
        }
    }
    //Aシューズ3
    public void OnClick_AShoes3Button()
    {
        if (AShoes3UseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "空ダッシューズ3\n\n空中移動速度+15";
            //AShoes3のイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            AShoesImage.SetActive(true);
            //ボタンの色変更
            AShoes3Button.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            AShoes3UseFlag = true;
            //その他シューズを使用変更する
            AShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoesUseFlag = false;
            AShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoes2UseFlag = false;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            AShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            AShoes3UseFlag = false;
        }
    }

    //スーパーハンド
    public void OnClick_SuperHandButton()
    {
        if (SuperHandUseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "スーパーハンド\n\n開始から10秒間は物との接触が可能";
            //SuperHandのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            SuperHandImage.SetActive(true);
            //ボタンの色変更
            SuperHandButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            SuperHandUseFlag = true;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            SuperHandButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            SuperHandUseFlag = false;
        }
    }

    //Jウィング
    public void OnClick_JWingButton()
    {
        if (JWingUseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "翼羽\n\nジャンプ回数+1";
            //JWingのイメージ表示
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JWingImage.SetActive(true);
            //ボタンの色変更
            JWingButton.GetComponent<Image>().color = new Color32(238, 255, 227, 255);
            //使用予定
            JWingUseFlag = true;
        }
        else
        {
            ItemDetailsText.text = "";
            foreach (Transform childTransform in ShopDetailsPanel.transform) childTransform.gameObject.SetActive(false);
            JWingButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            JWingUseFlag = false;
        }
    }
}

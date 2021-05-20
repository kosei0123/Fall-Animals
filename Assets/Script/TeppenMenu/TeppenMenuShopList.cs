using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeppenMenuShopList : MonoBehaviour
{
    //商品の詳細説明文
    [SerializeField]
    private Text ItemDetailsText;

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

    // Start is called before the first frame update
    void Start()
    {
        //ボタンの表示
        //Shoes
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //シューズ
    public void OnClick_DShoesButton()
    {
        if (DShoesUseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "Dシューズ\n\n移動速度+5";
            //DShoesのイメージ表示
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
            DShoesImage.SetActive(false);
            DShoesButton.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoesUseFlag = false;
        }
    }
    //シューズ2
    public void OnClick_DShoes2Button()
    {
        if (DShoes2UseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "Dシューズ\n\n移動速度+10";
            //DShoesのイメージ表示
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
            DShoesImage.SetActive(false);
            DShoes2Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes2UseFlag = false;
        }
    }
    //シューズ3
    public void OnClick_DShoes3Button()
    {
        if (DShoes3UseFlag == false)
        {
            //詳細表示
            ItemDetailsText.text = "Dシューズ\n\n移動速度+15";
            //DShoesのイメージ表示
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
            DShoesImage.SetActive(false);
            DShoes3Button.GetComponent<Image>().color = new Color32(208, 255, 177, 255);
            DShoes3UseFlag = false;
        }
    }
}

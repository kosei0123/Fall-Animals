using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemDisplay_teppen : MonoBehaviour
{
    //Timer_teppenのpublic定数を使う
    Timer_teppen timer_teppen;

    //スーパーハンド
    [SerializeField]
    private GameObject SuperHandImage;
    //スーパーハンド
    [SerializeField]
    private GameObject JWingImage;
    //Dシューズ
    [SerializeField]
    private GameObject DShoesImage;
    //Jシューズ
    [SerializeField]
    private GameObject JShoesImage;
    //Aシューズ
    [SerializeField]
    private GameObject AShoesImage;
    //ミッションA
    [SerializeField]
    private GameObject MissionAImage;
    //ミッションB
    [SerializeField]
    private GameObject MissionBImage;

    // Start is called before the first frame update
    void Start()
    {
        //Timer_teppenのpublic定数を使う
        timer_teppen = GameObject.Find("TimerCanvas").GetComponent<Timer_teppen>();

        //イメージの表示
        //アイテム
        if (TeppenMenuShopList.SuperHandUseFlag == true) SuperHandImage.SetActive(true);
        if (TeppenMenuShopList.JWingUseFlag == true) JWingImage.SetActive(true);
        if (TeppenMenuShopList.DShoesUseFlag == true || TeppenMenuShopList.DShoes2UseFlag == true || TeppenMenuShopList.DShoes3UseFlag == true) DShoesImage.SetActive(true);
        if (TeppenMenuShopList.JShoesUseFlag == true || TeppenMenuShopList.JShoes2UseFlag == true || TeppenMenuShopList.JShoes3UseFlag == true) JShoesImage.SetActive(true);
        if (TeppenMenuShopList.AShoesUseFlag == true || TeppenMenuShopList.AShoes2UseFlag == true || TeppenMenuShopList.AShoes3UseFlag == true) AShoesImage.SetActive(true);
        //ミッション
        if (TeppenShopUI.MissionASuccessFlag == true) MissionAImage.SetActive(true);
        if (TeppenShopUI.MissionBSuccessFlag == true) MissionBImage.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        //スーパーハンド
        if (TeppenMenuShopList.SuperHandUseFlag == true && timer_teppen.elapsedTime > 10) SuperHandImage.SetActive(false);
        //ミッション
        if (TeppenShopUI.MissionASuccessFlag == false) MissionAImage.SetActive(false);
        if (TeppenShopUI.MissionBSuccessFlag == false) MissionBImage.SetActive(false);
    }
}

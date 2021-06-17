using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTeppenShopList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ショップリストのランダム値
        if (!PlayerPrefs.HasKey("TeppenRandomShopList1")) PlayerPrefs.SetInt("TeppenRandomShopList1", -1);
        if (!PlayerPrefs.HasKey("TeppenRandomShopList2")) PlayerPrefs.SetInt("TeppenRandomShopList2", -1);
        if (!PlayerPrefs.HasKey("TeppenRandomShopList3")) PlayerPrefs.SetInt("TeppenRandomShopList3", -1);

        //テッペンの階数をデバイスに保存
        if (!PlayerPrefs.HasKey("TeppenFloor")) PlayerPrefs.SetInt("TeppenFloor", 1);

        //テッペン挑戦回数
        if (!PlayerPrefs.HasKey("TeppenDairyChallenge")) PlayerPrefs.SetInt("TeppenDairyChallenge", 0);

        //Dシューズ
        if (!PlayerPrefs.HasKey("DShoesFlag")) PlayerPrefs.SetInt("DShoesFlag", 0);
        if (!PlayerPrefs.HasKey("DShoes2Flag")) PlayerPrefs.SetInt("DShoes2Flag", 0);
        if (!PlayerPrefs.HasKey("DShoes3Flag")) PlayerPrefs.SetInt("DShoes3Flag", 0);
        //Jシューズ
        if (!PlayerPrefs.HasKey("JShoesFlag")) PlayerPrefs.SetInt("JShoesFlag", 0);
        if (!PlayerPrefs.HasKey("JShoes2Flag")) PlayerPrefs.SetInt("JShoes2Flag", 0);
        if (!PlayerPrefs.HasKey("JShoes3Flag")) PlayerPrefs.SetInt("JShoes3Flag", 0);
        //Aシューズ
        if (!PlayerPrefs.HasKey("AShoesFlag")) PlayerPrefs.SetInt("AShoesFlag", 0);
        if (!PlayerPrefs.HasKey("AShoes2Flag")) PlayerPrefs.SetInt("AShoes2Flag", 0);
        if (!PlayerPrefs.HasKey("AShoes3Flag")) PlayerPrefs.SetInt("AShoes3Flag", 0);
        //スーパーハンド
        if (!PlayerPrefs.HasKey("SuperHandFlag")) PlayerPrefs.SetInt("SuperHandFlag", 0);
        //Jウィング
        if (!PlayerPrefs.HasKey("JWingFlag")) PlayerPrefs.SetInt("JWingFlag", 0);
        //キャラクター変更
        if (!PlayerPrefs.HasKey("TeppenDogCanUse")) PlayerPrefs.SetInt("TeppenDogCanUse", 0);
        if (!PlayerPrefs.HasKey("TeppenGiraffeCanUse")) PlayerPrefs.SetInt("TeppenGiraffeCanUse", 0);
        if (!PlayerPrefs.HasKey("TeppenElephantCanUse")) PlayerPrefs.SetInt("TeppenElephantCanUse", 0);
        if (!PlayerPrefs.HasKey("TeppenTigerCanUse")) PlayerPrefs.SetInt("TeppenTigerCanUse", 0);
        if (!PlayerPrefs.HasKey("TeppenCatCanUse")) PlayerPrefs.SetInt("TeppenCatCanUse", 0);
        if (!PlayerPrefs.HasKey("TeppenRabbitCanUse")) PlayerPrefs.SetInt("TeppenRabbitCanUse", 0);

        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

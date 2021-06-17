using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeppenRecordUI : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //UserAuthのスクリプトの関数使用
    UserAuth userAuth;

    //到達階数テキスト
    [SerializeField]
    private Text FloorText;
    //獲得コインテキスト
    [SerializeField]
    private Text GetCoinText;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //UserAuthのスクリプトの関数使用
        userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();

        //到達階数テキストの表示
        FloorText.text = "到達階数：" + PlayerPrefs.GetInt("TeppenFloor").ToString("") + "階";
        //獲得コインの表示
        GetCoinText.text = "獲得コイン：" + PlayerPrefs.GetInt("getScheduledCoin") + "コイン";

        //ステータスを戻す
        PlayerPrefs.SetString("TeppenStatus", "Play");

        //ベストテッペンフロアの更新
        if(PlayerPrefs.GetInt("TeppenFloor") > PlayerPrefs.GetInt("BestTeppenFloor"))
        {
            PlayerPrefs.SetInt("BestTeppenFloor", PlayerPrefs.GetInt("TeppenFloor"));
            PlayerPrefs.Save();

            //ネットワーク接続確認
            //インターネット接続なし
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                //オンライン時にデータを保存する
                PlayerPrefs.SetInt("bestTeppenRecord", 1);
                PlayerPrefs.Save();
            }
            //インターネット接続あり
            else
            {
                //サーバにテッペンハイスコアを保存
                userAuth.save_OfflineTeppen();
            }

        }

        //獲得コインの取得
        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") + PlayerPrefs.GetInt("getScheduledCoin"));

        //テッペンフロアのリセット
        PlayerPrefs.SetInt("TeppenFloor", 1);
        //獲得予定コインのリセット
        PlayerPrefs.SetInt("getScheduledCoin", 0);

        //所持品をリセットする
        BrigItemReset();


        //セーブ
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //所持品をリセットする
    private void BrigItemReset()
    {
        //ショップリストのランダム値
        PlayerPrefs.SetInt("TeppenRandomShopList1", -1);
        PlayerPrefs.SetInt("TeppenRandomShopList2", -1);
        PlayerPrefs.SetInt("TeppenRandomShopList3", -1);
        //Dシューズ
        PlayerPrefs.SetInt("DShoesFlag", 0);
        PlayerPrefs.SetInt("DShoes2Flag", 0);
        PlayerPrefs.SetInt("DShoes3Flag", 0);
        //Jシューズ
        PlayerPrefs.SetInt("JShoesFlag", 0);
        PlayerPrefs.SetInt("JShoes2Flag", 0);
        PlayerPrefs.SetInt("JShoes3Flag", 0);
        //Aシューズ
        PlayerPrefs.SetInt("AShoesFlag", 0);
        PlayerPrefs.SetInt("AShoes2Flag", 0);
        PlayerPrefs.SetInt("AShoes3Flag", 0);
        //スーパーハンド
        PlayerPrefs.SetInt("SuperHandFlag", 0);
        //Jウィング
        PlayerPrefs.SetInt("JWingFlag", 0);
        //チェンジ動物
        PlayerPrefs.SetInt("TeppenDogCanUse", 0);
        PlayerPrefs.SetInt("TeppenGiraffeCanUse", 0);
        PlayerPrefs.SetInt("TeppenElephantCanUse", 0);
        PlayerPrefs.SetInt("TeppenTigerCanUse", 0);
        PlayerPrefs.SetInt("TeppenCatCanUse", 0);
        PlayerPrefs.SetInt("TeppenRabbitCanUse", 0);
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

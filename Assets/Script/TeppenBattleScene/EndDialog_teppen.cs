using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndDialog_teppen : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //BattleScene_teppenManagerのpublic定数を使う
    BattleScene_teppenManager battleScene_teppenManager;

    //バトル終了時のダイアログ
    [SerializeField]
    private GameObject DialogPanel;

    //紙飛行機の親オブジェクト
    [SerializeField]
    private GameObject AirplaneParent;
    //岩の親オブジェクト
    [SerializeField]
    private GameObject RockParent;

    //広告表示ボタン
    [SerializeField]
    private Button RewardAdvertisingButton;

    //ミッションクリアパネル
    [SerializeField]
    private GameObject MissionSuccessPanel;
    //フロアテキスト表示
    [SerializeField]
    private Text FloorText;
    //ゲットコイン表示
    [SerializeField]
    private Text GetCoinText;
    private int getTotalCoin;

    //テッペンメニューへの非同期シーン遷移用
    private AsyncOperation async_TeppenMenu;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //BattleScene_teppenManagerのpublic定数を使う
        battleScene_teppenManager = GameObject.Find("BattleScene_offlineManager").GetComponent<BattleScene_teppenManager>();

        

    }

    // Update is called once per frame
    void Update()
    {
        //横画面時のみボタンを押下できる
        if (Screen.width > Screen.height)
        {
#if UNITY_IOS
            RewardAdvertisingButton.interactable = true;
#elif UNITY_ANDROID
            RewardAdvertisingButton.interactable = false;
#endif
        }
        else
        {
            //RewardAdvertisingButton.interactable = false;
        }
    }

    //バトル終了時のダイアログ表示
    public void DialogPanelActive(int floor)
    {
        //メモリ解放
        Resources.UnloadUnusedAssets();

        //画面遷移
        if(async_TeppenMenu == null) async_TeppenMenu = SceneManager.LoadSceneAsync("TeppenMenu");
        async_TeppenMenu.allowSceneActivation = false;

        //挑戦回数の更新
        PlayerPrefs.SetInt("TeppenDairyChallenge", PlayerPrefs.GetInt("TeppenDairyChallenge") + 1);

        //バトル終了時ダイアログ表示
        DialogPanel.SetActive(true);
        //リプレイボタン
        //ReplayButton.SetActive(true);

        //購入商品の使用終了
        ShopListItemUseFinish();

        //ぶつかったり落下した場合
        if (battleScene_teppenManager.damagedFlag == true)
        {
            //フロアを表示する
            FloorText.text = "ゲームオーバー";

            //ゲットコインの表示
            getTotalCoin += battleScene_teppenManager.getBattleCoin;
            GetCoinText.text = getTotalCoin.ToString() + "コインGET!!";

            //コイン獲得
            PlayerPrefs.SetInt("getScheduledCoin", PlayerPrefs.GetInt("getScheduledCoin") + battleScene_teppenManager.getBattleCoin);

            //ステータスをゲームオーバーに更新
            PlayerPrefs.SetString("TeppenStatus", "GameOver");
        }
        else
        {
            //フロアを表示する
            FloorText.text = floor.ToString("") + "階到達！";

            //ゲットコインの表示
            getTotalCoin += battleScene_teppenManager.getBattleCoin + PlayerPrefs.GetInt("TeppenFloor") * 5;
            GetCoinText.text = getTotalCoin.ToString() + "コインGET!!";

            //ミッションクリア状況
            MissonSuccessPanelDisplay();

            //コイン&フロアボーナスの獲得(上に行けば行くほど高くなる)
            PlayerPrefs.SetInt("getScheduledCoin", PlayerPrefs.GetInt("getScheduledCoin") + battleScene_teppenManager.getBattleCoin + PlayerPrefs.GetInt("TeppenFloor") * 5);
        }
        PlayerPrefs.Save();
    }

    //購入商品の使用終了
    private void ShopListItemUseFinish()
    {
        //ショップリストのランダム値
        PlayerPrefs.SetInt("TeppenRandomShopList1", -1);
        PlayerPrefs.SetInt("TeppenRandomShopList2", -1);
        PlayerPrefs.SetInt("TeppenRandomShopList3", -1);

        //Dシューズ
        if (TeppenMenuShopList.DShoesUseFlag == true)
        {
            PlayerPrefs.SetInt("DShoesFlag", 0);
            TeppenMenuShopList.DShoesUseFlag = false;
        }
        if (TeppenMenuShopList.DShoes2UseFlag == true)
        {
            PlayerPrefs.SetInt("DShoes2Flag", 0);
            TeppenMenuShopList.DShoes2UseFlag = false;
        }
        if (TeppenMenuShopList.DShoes3UseFlag == true)
        {
            PlayerPrefs.SetInt("DShoes3Flag", 0);
            TeppenMenuShopList.DShoes3UseFlag = false;
        }
        //Jシューズ
        if (TeppenMenuShopList.JShoesUseFlag == true)
        {
            PlayerPrefs.SetInt("JShoesFlag", 0);
            TeppenMenuShopList.JShoesUseFlag = false;
        }
        if (TeppenMenuShopList.JShoes2UseFlag == true)
        {
            PlayerPrefs.SetInt("JShoes2Flag", 0);
            TeppenMenuShopList.JShoes2UseFlag = false;
        }
        if (TeppenMenuShopList.JShoes3UseFlag == true)
        {
            PlayerPrefs.SetInt("JShoes3Flag", 0);
            TeppenMenuShopList.JShoes3UseFlag = false;
        }
        //Aシューズ
        if (TeppenMenuShopList.AShoesUseFlag == true)
        {
            PlayerPrefs.SetInt("AShoesFlag", 0);
            TeppenMenuShopList.AShoesUseFlag = false;
        }
        if (TeppenMenuShopList.AShoes2UseFlag == true)
        {
            PlayerPrefs.SetInt("AShoes2Flag", 0);
            TeppenMenuShopList.AShoes2UseFlag = false;
        }
        if (TeppenMenuShopList.AShoes3UseFlag == true)
        {
            PlayerPrefs.SetInt("AShoes3Flag", 0);
            TeppenMenuShopList.AShoes3UseFlag = false;
        }
        //スーパーハンド
        if (TeppenMenuShopList.SuperHandUseFlag == true)
        {
            PlayerPrefs.SetInt("SuperHandFlag", 0);
            TeppenMenuShopList.SuperHandUseFlag = false;
        }
        //Jウィング
        if (TeppenMenuShopList.JWingUseFlag == true)
        {
            PlayerPrefs.SetInt("JWingFlag", 0);
            TeppenMenuShopList.JWingUseFlag = false;
        }

        //マイナスタイム
        if (TeppenShopUI.MinusTimeRealTotal > 0) TeppenShopUI.MinusTimeRealTotal = 0;
        //ロックスロー
        if (TeppenShopUI.RockSlowRealTotal > 0) TeppenShopUI.RockSlowRealTotal = 0;
        //ヒコーキスロー
        if (TeppenShopUI.AirplaneSlowRealTotal > 0) TeppenShopUI.AirplaneSlowRealTotal = 0;
        //ステージ指定
        TeppenShopUI.stageAssignmentNumber = 0;
        //トランポリンの使用
        TeppenShopUI.TrampolineRealFlag = false;
    }

    //ミッションクリアパネルの表示
    private void MissonSuccessPanelDisplay()
    {
        if(TeppenShopUI.MissionASuccessFlag == true)
        {
            MissionSuccessPanel.SetActive(true);
            PlayerPrefs.SetInt("getScheduledCoin", PlayerPrefs.GetInt("getScheduledCoin") + 200);
            TeppenShopUI.MissionASuccessFlag = false;
        }
        if (TeppenShopUI.MissionBSuccessFlag == true)
        {
            MissionSuccessPanel.SetActive(true);
            PlayerPrefs.SetInt("getScheduledCoin", PlayerPrefs.GetInt("getScheduledCoin") + 150);
            TeppenShopUI.MissionBSuccessFlag = false;
        }

        PlayerPrefs.Save();
    }

    
    //ダイアログの「もどる」選択
    public void OnClick_AgainButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //画面遷移
        SceneManager.LoadScene("TeppenMenu");
        //async_TeppenMenu.allowSceneActivation = true;

    }
}

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
    //Replayの関数を使う
    Replay.ReplayManager replayManager;

    //バトル終了時のダイアログ
    [SerializeField]
    private GameObject DialogPanel;
    //リプレイボタン
    [SerializeField]
    private GameObject ReplayButton;
    private bool replayFlag = false;

    //紙飛行機の親オブジェクト
    [SerializeField]
    private GameObject AirplaneParent;
    //岩の親オブジェクト
    [SerializeField]
    private GameObject RockParent;

    //広告表示ボタン
    [SerializeField]
    private Button RewardAdvertisingButton;

    //フロアテキスト表示
    [SerializeField]
    private Text FloorText;
    //ゲットコイン表示
    [SerializeField]
    private Text GetCoinText;
    private int getTotalCoin;


    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //BattleScene_teppenManagerのpublic定数を使う
        battleScene_teppenManager = GameObject.Find("BattleScene_offlineManager").GetComponent<BattleScene_teppenManager>();
        //Replayの関数を使う
        replayManager = GameObject.Find("REPLAY").GetComponent<Replay.ReplayManager>();

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

        //再リプレイ時
        if (replayManager._slide.value == 0 && replayFlag == true)
        {
            //紙飛行機
            foreach (Transform childTransform in AirplaneParent.transform)
            {
                childTransform.gameObject.SetActive(true);
            }
            //岩
            foreach (Transform childTransform in RockParent.transform)
            {
                childTransform.gameObject.SetActive(true);
            }
        }
    }

    //バトル終了時のダイアログ表示
    public void DialogPanelActive(int floor)
    {
        //バトル終了時ダイアログ表示
        DialogPanel.SetActive(true);
        //リプレイボタン
        ReplayButton.SetActive(true);

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

            //コイン&フロアボーナスの獲得(上に行けば行くほど高くなる)
            PlayerPrefs.SetInt("getScheduledCoin", PlayerPrefs.GetInt("getScheduledCoin") + battleScene_teppenManager.getBattleCoin + PlayerPrefs.GetInt("TeppenFloor") * 5);
        }
        
    }

    //購入商品の使用終了
    private void ShopListItemUseFinish()
    {
        //シューズ
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

        //マイナスタイム
        if (TeppenShopUI.MinusTimeRealTotal > 0) TeppenShopUI.MinusTimeRealTotal = 0;
    }

    //リプレイボタンを押した時
    public void OnClick_ReplayButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        if(replayFlag == false)
        {
            //バトル終了時ダイアログ非表示
            DialogPanel.SetActive(false);

            //リプレイ
            replayManager.StartReplay();
            replayManager._slide.value = 0;

            replayFlag = true;
        }
        else
        {
            //バトル終了時ダイアログ表示
            DialogPanel.SetActive(true);

            //リプレイの中断+非表示
            replayManager.Pause();
            replayManager._replayCanvas.SetActive(false);

            replayFlag = false;
        }
        
    }

    //ダイアログの「もどる」選択
    public void OnClick_AgainButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //画面遷移
        SceneManager.LoadScene("TeppenMenu");

    }
}

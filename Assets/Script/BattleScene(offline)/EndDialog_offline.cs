using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndDialog_offline : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //BattleScene_offlineManagerのpublic定数を使う
    BattleScene_offlineManager battleScene_offlineManager;
    //Replayの関数を使う
    Replay.ReplayManager replayManager;

    //バトル終了時のダイアログ
    [SerializeField]
    private GameObject DialogPanel;
    //リプレイボタン
    [SerializeField]
    private GameObject ReplayButton;

    //広告表示ボタン
    [SerializeField]
    private Button RewardAdvertisingButton;

    //順位テキスト表示
    [SerializeField]
    private Text TimeText;
    //ゲットコイン表示
    [SerializeField]
    private Text GetCoinText;
    private int getTotalCoin;

    //ベストタイムかの確認
    public bool bestTimeFlag = false;


    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //BattleScene_offlineManagerのpublic定数を使う
        battleScene_offlineManager = GameObject.Find("BattleScene_offlineManager").GetComponent<BattleScene_offlineManager>();
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
    }

    //バトル終了時のダイアログ表示
    public void DialogPanelActive(float time)
    {
        //バトル終了時ダイアログ表示
        DialogPanel.SetActive(true);
        //リプレイボタン
        ReplayButton.SetActive(true);

        //タイムを表示する
        if (bestTimeFlag == true)
        {
            TimeText.text = time.ToString("") + "秒 (ベストタイム)";
        }
        else
        {
            TimeText.text = time.ToString("") + "秒";
        }
        

        //ゲットコインの表示
        getTotalCoin += battleScene_offlineManager.getBattleCoin;
        GetCoinText.text = getTotalCoin.ToString() + "コインGET!!";
        //デバイスの保持する
        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") + battleScene_offlineManager.getBattleCoin);
    }

    //リプレイボタンを押した時
    public void OnClick_ReplayButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //バトル終了時ダイアログ表示
        DialogPanel.SetActive(false);

        //リプレイ
        replayManager.StartReplay();
    }

    //ダイアログの「もどる」選択
    public void OnClick_AgainButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //画面遷移
        SceneManager.LoadScene("Menu");

    }

    //ダイアログの「終了」選択
    //public void OnClick_EndButton()
    //{
    //    //SEの使用
    //    soundManager.SEManager("Button_sound1");

    //    //アプリケーションの終了
    //    Application.Quit();
    //}
}

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

    //バトル終了時のダイアログ
    [SerializeField]
    private GameObject DialogPanel;

    //順位テキスト表示
    [SerializeField]
    private Text TimeText;
    //ゲットコイン表示
    [SerializeField]
    private Text GetCoinText;
    private int getTotalCoin;


    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //BattleScene_offlineManagerのpublic定数を使う
        battleScene_offlineManager = GameObject.Find("BattleScene_offlineManager").GetComponent<BattleScene_offlineManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //バトル終了時のダイアログ表示
    public void DialogPanelActive(float time)
    {
        //バトル終了時ダイアログ表示
        DialogPanel.SetActive(true);

        //順位を表示する
        TimeText.text = time.ToString("") + "秒";

        //ゲットコインの表示
        getTotalCoin += battleScene_offlineManager.getBattleCoin;
        GetCoinText.text = getTotalCoin.ToString() + "コインGET!!";
        //デバイスの保持する
        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") + battleScene_offlineManager.getBattleCoin);
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
    public void OnClick_EndButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //アプリケーションの終了
        Application.Quit();
    }
}

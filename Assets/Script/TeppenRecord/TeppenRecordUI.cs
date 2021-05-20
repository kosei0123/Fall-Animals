using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeppenRecordUI : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

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
        //シューズ
        PlayerPrefs.SetInt("DShoesFlag", 0);
        PlayerPrefs.SetInt("DShoes2Flag", 0);
        PlayerPrefs.SetInt("DShoes3Flag", 0);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitingRoom_offline : MonoBehaviour
{
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;

    //ランダム値の取得(ステージ)
    //private int randomStage;

    //スタート時間を設定する
    [SerializeField]
    private Text StartTimeText;
    private float waitingBattleStartTime;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //ステージを確定する
        //randomStage = Random.Range(1, 4);

        //バトルスタート時間を設定する
        waitingBattleStartTime = 3.0f;

    }

    // Update is called once per frame
    void Update()
    {
        //バトルスタート時間を表示する
        StartTimeText.text = ((int)waitingBattleStartTime).ToString("D2");

        //バトルスタート時間を減らしていく
        waitingBattleStartTime -= Time.deltaTime;

        //時間が0になったとき
        if (waitingBattleStartTime <= 0)
        {
            //画面遷移
            SceneManager.LoadScene("BattleScene(offline)");
        }
    }

    //メニューボタンを押下した際の挙動
    public void OnClick_MenuButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //画面遷移
        SceneManager.LoadScene("Menu");
    }

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //画面遷移
            //SceneManager.LoadScene("Menu");
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
        //画面遷移
        //SceneManager.LoadScene("Menu");
    }

    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(150, 30, 150, 70), "番号1 : " + WaitingPlayerNickName);
        //GUI.TextField(new Rect(350, 30, 150, 70), "番号2 : " + WaitingPlayer2NickName);
        //GUI.TextField(new Rect(550, 30, 150, 70), "番号3 : " + WaitingPlayer3NickName);
        //GUI.TextField(new Rect(750, 30, 150, 70), "番号4 : " + WaitingPlayer4NickName);

        //GUI.TextField(new Rect(150, 150, 150, 70), "番号1 : " + PhotonNetwork.LocalPlayer.CustomProperties["playerCreatedNumber"]);
    }
}

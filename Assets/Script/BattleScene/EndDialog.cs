using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class EndDialog : MonoBehaviourPunCallbacks
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //Pun2Scriptのpublic定数を使う
    Pun2Script pun2Script;

    //バトル終了時のダイアログ
    [SerializeField]
    private GameObject DialogPanel;

    //順位テキスト表示
    [SerializeField]
    private Text RankingText;
    //ゲットコイン表示
    [SerializeField]
    private Text GetCoinText;
    private int getTotalCoin;

    //取得したコインの値
    private int getCoin;


    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //Pun2Scriptのpublic定数を使う
        pun2Script = GameObject.Find("Pun2").GetComponent<Pun2Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //バトル終了時のダイアログ表示
    public void DialogPanelActive(int ranking)
    {
        //バトル終了時ダイアログ表示
        DialogPanel.SetActive(true);

        //順位を表示する
        RankingText.text = ranking.ToString() + " 位 ";

        //ゲットコインの表示
        getTotalCoin += GetCoin(ranking) + pun2Script.getBattleCoin;
        GetCoinText.text = getTotalCoin.ToString() + "コインGET!!";
        //デバイスの保持する
        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") + GetCoin(ranking) + pun2Script.getBattleCoin);
    }

    //ゲットするコインの計算
    private int GetCoin(int ranking)
    {
        //ゲットするコインは人数により変動する
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] == 4)
        {
            switch (ranking)
            {
                case 4:
                    getCoin = 10;
                    break;
                case 3:
                    getCoin = 20;
                    break;
                case 2:
                    getCoin = 30;
                    break;
                case 1:
                    getCoin = 40;
                    break;
                default:
                    getCoin = 40;
                    break;
            }
        }
        else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] == 3)
        {
            switch (ranking)
            {
                case 3:
                    getCoin = 10;
                    break;
                case 2:
                    getCoin = 20;
                    break;
                case 1:
                    getCoin = 30;
                    break;
                default:
                    getCoin = 30;
                    break;
            }
        }
        else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] == 2)
        {
            switch (ranking)
            {
                case 2:
                    getCoin = 10;
                    break;
                case 1:
                    getCoin = 20;
                    break;
                default:
                    getCoin = 20;
                    break;
            }
        }
        else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] == 1)
        {
            switch (ranking)
            {
                case 1:
                    getCoin = 10;
                    break;
                default:
                    getCoin = 10;
                    break;
            }
        }

        return getCoin;
    }

    //ダイアログの「再接続」選択
    public void OnClick_AgainButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //マスタークライアントの切断をルーム全体で検知する
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] = true;
            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
        }
        

        //画面遷移等(0.5秒後)
        Invoke("AgainEndDialog_SceneMove", 0.5f);

    }

    //ダイアログの「終了」選択
    public void OnClick_EndButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //マスタークライアントの切断をルーム全体で検知する
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] = true;
            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
        }

        //終了等(0.5秒後)
        Invoke("EndEndDialog_SceneMove", 0.5f);
    }

    //再接続時
    private void AgainEndDialog_SceneMove()
    {
        //画面遷移
        SceneManager.LoadScene("Menu");
    }

    //終了時のPhotonの切断
    private void EndEndDialog_SceneMove()
    {
        //アプリケーションの終了
        Application.Quit();
    }
}

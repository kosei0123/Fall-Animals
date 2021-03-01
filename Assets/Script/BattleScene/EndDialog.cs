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
        getTotalCoin += GetCoin(ranking);
        GetCoinText.text = getTotalCoin.ToString() + "コインGET!!";
        //デバイスの保持する
        PlayerPrefs.SetInt("myCoin", PlayerPrefs.GetInt("myCoin") + GetCoin(ranking));
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

        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }

        //画面遷移
        SceneManager.LoadScene("Menu");
    }

    //ダイアログの「終了」選択
    public void OnClick_EndButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        Application.Quit();
    }
}

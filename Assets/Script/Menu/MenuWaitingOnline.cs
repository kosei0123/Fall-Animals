using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MenuWaitingOnline : MonoBehaviour
{
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;

    //シングルトンにて1度のみ作成
    private static MenuWaitingOnline menuWaitingOnline_instance;

    //オンライン待ち時のパネル表示
    [SerializeField]
    private GameObject WaitingOnlinePanel;

    //オンライン待ちかどうか
    public static bool menuWaitingOnlineFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //オブジェクトが作成され、一度のみ永久に破壊されない
        if (menuWaitingOnline_instance == null)
        {
            menuWaitingOnline_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //オンライン待機中のパネル表示
        if (menuWaitingOnlineFlag == true) WaitingOnlinePanel.SetActive(true);
        else { WaitingOnlinePanel.SetActive(false); }

        //オンライン待機中でなければリターンする
        if (menuWaitingOnlineFlag == false) return;

        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] >= 2)
        {
            SceneManager.LoadScene("WaitingRoom");
            Destroy(this.gameObject);
        }
    }

    //キャンセルボタン押下時
    public void OnClick_CancelButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");

        //オンライン待機中のパネル非表示
        WaitingOnlinePanel.SetActive(false);
        //オンライン待機中を解除する
        menuWaitingOnlineFlag = false;

        //プレイヤーNo.を空にする
        var prps = PhotonNetwork.LocalPlayer.CustomProperties;
        //prps["NoKick"] = "false";
        prps["playerCreatedNumber"] = null;
        PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

        //Photon解除(0.5秒後)
        Invoke("MenuWaitingOnline_PhotonOff", 0.5f);
    }

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //オンライン待機中のパネル非表示
            WaitingOnlinePanel.SetActive(false);
            //オンライン待機中を解除する
            menuWaitingOnlineFlag = false;

            //プレイヤーNo.を空にする
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            //prps["NoKick"] = "false";
            prps["playerCreatedNumber"] = null;
            PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

            //Photonに接続を解除する
            if (PhotonNetwork.IsConnected == true)
            {
                PhotonNetwork.Disconnect();
            }
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
        //オンライン待機中のパネル非表示
        WaitingOnlinePanel.SetActive(false);
        //オンライン待機中を解除する
        menuWaitingOnlineFlag = false;

        //プレイヤーNo.を空にする
        var prps = PhotonNetwork.LocalPlayer.CustomProperties;
        //prps["NoKick"] = "false";
        prps["playerCreatedNumber"] = null;
        PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }
    }

    //Photon接続解除
    private void MenuWaitingOnline_PhotonOff()
    {
        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }
    }
}

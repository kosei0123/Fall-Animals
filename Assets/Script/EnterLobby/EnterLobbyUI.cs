using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class EnterLobbyUI : MonoBehaviourPunCallbacks
{
    //LobbyManagerスクリプトのpublic定数使用
    LobbyManager lobbyManager;

    //Photon接続人数を表示
    public Text ConnectCountText;
    //Buttonのコンポーネントを取得
    public Button EnterLobbyButton;

    // Start is called before the first frame update
    void Start()
    {
        //LobbyManagerスクリプトのpublic定数使用
        lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();

        //Photonに接続できていなければ、Photonに接続する
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //現在何人のプレイヤーがPhotonに接続しているのかを表示
        ConnectCountText.text = "接続プレイヤー：" + PhotonNetwork.CountOfPlayers.ToString() + " / 20";

        //Photonに接続人数がMaxでない時にボタン押下可能
        if (PhotonNetwork.CountOfPlayers <= 20)
        {
            EnterLobbyButton.interactable = true;
        }
        else
        {
            EnterLobbyButton.interactable = false;
        }
    }

    //ロビーに参加するボタン押下
    public void OnClick_EnterLobbyButton()
    {
        //Photonに接続人数がMaxでない時に画面遷移する
        if (PhotonNetwork.CountOfPlayers <= 20 && lobbyManager.joinedRoomFlag == true)
        {
            
            //画面遷移
            SceneManager.LoadScene("SelectPlayerName");
        }
        else
        {

        }
    }

    //メニューに戻るボタン押下
    public void OnClick_MenuButton()
    {
        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }

        //画面遷移
        SceneManager.LoadScene("Menu");
    }
}

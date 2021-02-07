using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class EnterLobbyUI : MonoBehaviourPunCallbacks
{
    //Photon接続人数を表示
    public Text ConnectCountText;
    //Buttonのコンポーネントを取得
    public Button EnterLobbyButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //現在何人のプレイヤーがPhotonに接続しているのかを表示
        ConnectCountText.text = "接続プレイヤー：" + PhotonNetwork.CountOfPlayers.ToString() + " / 20" ;
        

        //Photonに接続人数がMaxでない時にボタン押下可能
        if (PhotonNetwork.CountOfPlayers <= 2)
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
        if (PhotonNetwork.CountOfPlayers <= 20)
        {
            //Photonに接続できていなければ、Photonに接続する
            if (PhotonNetwork.IsConnected == false)
            {
                PhotonNetwork.ConnectUsingSettings();
            }
            //画面遷移
            SceneManager.LoadScene("SelectPlayerName");
        }
        else
        {

        }
    }

    //ホームに戻るボタン押下
    public void OnClick_HomeButton()
    {
        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }

        //画面遷移
        SceneManager.LoadScene("Title");
    }
}

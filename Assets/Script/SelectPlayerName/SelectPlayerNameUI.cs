using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SelectPlayerNameUI : MonoBehaviour
{
    //ニックネーム取得用
    private string nickname;
    //ルーム名表示用
    [SerializeField]
    private Text RoomText;

    // Start is called before the first frame update
    void Start()
    {
        //ルーム名表示
        RoomText.text = PhotonNetwork.CurrentRoom.Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //SelectNameButton押下した際の挙動
    public void OnClick_SelectNameButton()
    {
        nickname = GameObject.Find("InputField").GetComponent<InputField>().text;

        //ニックネームが空の場合は適当なものを代入する
        if (string.IsNullOrEmpty(nickname))
        {
            nickname = "Player(" + Random.Range(1, 999) + ")";
        }

        PhotonNetwork.LocalPlayer.NickName = nickname;

        //画面遷移
        SceneManager.LoadScene("SelectCharacter");
    }

    //メニューボタン押下した際の挙動
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

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            WaitingPlayerCount_PhotonOff();
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
        WaitingPlayerCount_PhotonOff();
    }

    //Photon接続解除や画面の遷移
    private void WaitingPlayerCount_PhotonOff()
    {
        //画面遷移
        SceneManager.LoadScene("Menu");

        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }
    }

    //表示処理
    private void OnGUI()
    {


        //GUI.TextField(new Rect(150, 30, 150, 70), "Room名 : " + PhotonNetwork.CurrentRoom.Name);
        //GUI.TextField(new Rect(400, 30, 150, 70), "HP(2P) : " + samplePun2Script.GetPlayer2Information().CustomProperties["HP"].ToString());

        //GUI.TextField(new Rect(650, 30, 150, 70), "HP(3P) : ".ToString());
        //GUI.TextField(new Rect(900, 30, 150, 70), "HP(4P) : ".ToString());
    }
}

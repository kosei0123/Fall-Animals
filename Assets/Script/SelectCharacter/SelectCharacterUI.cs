using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class SelectCharacterUI : MonoBehaviour
{
    //Buttonのコンポーネントを取得
    [SerializeField]
    private Button SelectCharacterOKButton;

    //選択キャラの名前表示
    [SerializeField]
    private Text SelectCharacterText;

    //プレイキャラの名前取得
    public static string animalName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //キャラ選択できていない場合はOKボタンを押せないようにする
        if(animalName == null)
        {
            //ボタン選択不可
            SelectCharacterOKButton.interactable = false;
        }
        else
        {
            //ボタン選択可
            SelectCharacterOKButton.interactable = true;
            //選択キャラの名前表示
            SelectCharacterText.text = animalName;
        }

        
    }

    //Giraffeボタン押下した際の処理
    public void OnClick_GiraffeButton()
    {
        //プレイキャラの名前取得
        animalName = "Giraffe";
    }

    //Unityちゃんボタン押下した際の処理
    public void OnClick_UnityChanButton()
    {
        //プレイキャラの名前取得
        animalName = "animal1";
    }

    //キャラ選択後に画面遷移を行う
    public void OnClick_SelectCharacterOKButton()
    {
        SceneManager.LoadScene("WaitingRoom");
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

}

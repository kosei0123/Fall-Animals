using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class SelectCharacterUI : MonoBehaviour
{
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;

    //Buttonのコンポーネントを取得
    [SerializeField]
    private Button SelectCharacterOKButton;
    //選択キャラの名前表示
    [SerializeField]
    private Text SelectCharacterText;
    //象パネルの表示
    [SerializeField]
    private GameObject ElephantPanel;

    //プレイキャラの名前取得
    public static string animalName;

    //一定時間操作がなかった時に接続を切る用
    private float disconnectTime;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //時間の設定(20秒)
        disconnectTime = 20;
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


        //アンロックされたキャラクターを表示する
        CheckUnlock();


        //一定時間操作がなかった時に退出
        if (disconnectTime > 0)
        {
            disconnectTime -= Time.deltaTime;
        }
        else
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

    //アンロックされたキャラクターを表示する
    private void CheckUnlock()
    {
        //象パネル
        if (PlayerPrefs.GetInt("Unlock_Elephant") == 1)
        {
            ElephantPanel.SetActive(true);
        }
    }

    //Giraffeボタン押下した際の処理
    public void OnClick_GiraffeButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //プレイキャラの名前取得
        animalName = "Giraffe";
    }

    //Elephantボタン押下した際の処理
    public void OnClick_ElephantButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //プレイキャラの名前取得
        animalName = "Elephant";
    }

    //Dogボタン押下した際の処理
    public void OnClick_DogButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //プレイキャラの名前取得
        animalName = "Dog";
    }

    //Unityちゃんボタン押下した際の処理
    public void OnClick_UnityChanButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //プレイキャラの名前取得
        animalName = "animal1";
    }

    //キャラ選択後に画面遷移を行う
    public void OnClick_SelectCharacterOKButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
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

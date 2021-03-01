using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class SelectCharacterUI : MonoBehaviourPunCallbacks
{
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;

    //Buttonのコンポーネントを取得
    [SerializeField]
    private Button SelectCharacterOKButton;
    //ルーム名表示用
    [SerializeField]
    private Text RoomText;
    //選択キャラの名前表示
    [SerializeField]
    private Text SelectCharacterText;
    //キリンパネルの表示
    [SerializeField]
    private GameObject GiraffePanel;
    //象パネルの表示
    [SerializeField]
    private GameObject ElephantPanel;
    //虎パネルの表示
    [SerializeField]
    private GameObject TigerPanel;

    //プレイキャラの名前取得
    public static string animalName;

    //一定時間操作がなかった時に接続を切る用
    private float disconnectTime;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //ルーム名表示
        RoomText.text = PhotonNetwork.CurrentRoom.Name;

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

        //人数により部屋をクローズする
        LobbyManager.UpdateRoomOptions(true);

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
        //キリンパネル
        if (PlayerPrefs.GetInt("Unlock_Giraffe") == 1)
        {
            GiraffePanel.SetActive(true);
        }
        //象パネル
        if (PlayerPrefs.GetInt("Unlock_Elephant") == 1)
        {
            ElephantPanel.SetActive(true);
        }
        //虎パネル
        if (PlayerPrefs.GetInt("Unlock_Tiger") == 1)
        {
            TigerPanel.SetActive(true);
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

    //Tigerボタン押下した際の処理
    public void OnClick_TigerButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //プレイキャラの名前取得
        animalName = "Tiger";
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


    //キックされた時用
    public override void OnLeftRoom()
    {
        SelectCharacterUI_PhotonOff();
    }

    //メニューボタン押下した際の挙動
    public void OnClick_MenuButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        SelectCharacterUI_PhotonOff();
    }

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SelectCharacterUI_PhotonOff();
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
        SelectCharacterUI_PhotonOff();
    }

    //Photon接続解除や画面の遷移
    private void SelectCharacterUI_PhotonOff()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SelectPlayerNameUI : MonoBehaviour
{
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;
    //UserAuthのスクリプトの関数使用
    UserAuth userAuth;
    //TabooWordListのスクリプト関数使用
    TabooWordList tabooWordList;

    //禁止文字を入力した際に表示
    [SerializeField]
    private GameObject TabooText;
    //初期設定時のネットワークエラー
    [SerializeField]
    private GameObject FirstNotNetworkPanel;

    //ニックネーム取得用
    private string nickname;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //UserAuthのスクリプトの関数使用
        userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();
        //TabooWordListのスクリプト関数使用
        tabooWordList = GameObject.Find("Canvas").GetComponent<TabooWordList>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    //SelectNameButton押下した際の挙動
    public void OnClick_SelectNameButton()
    {
        //オフラインであれば抜ける
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            FirstNotNetworkPanel.SetActive(true);
            return;
        }

        nickname = GameObject.Find("InputField").GetComponent<InputField>().text;

        //ニックネームが空の場合は適当なものを代入する
        if (string.IsNullOrEmpty(nickname))
        {
            nickname = "Player(" + Random.Range(1, 9999) + ")";
        }

        //禁止用語が含まれている
        if (tabooWordList.TabooWord(nickname))
        {
            TabooText.SetActive(true);
        }
        //禁止用語が含まれていない
        else
        {
            //ニックネームを一度だけ登録
            PlayerPrefs.SetString("NickName", nickname + "(" + Random.Range(1, 9999) + ")");

            //mobile backendに接続しサインインする
            userAuth.signUp(PlayerPrefs.GetString("NickName"));
            //mobile backendに接続し名前とスコアを初期登録する
            userAuth.firstSetNameScore();
            //mobile backendに接続しテッペンスコアを初期登録する
            userAuth.firstSetTeppenScore();

            //SEの使用
            soundManager.SEManager("Button_sound1");
            //画面遷移
            SceneManager.LoadScene("Menu");
        }
        
    }

    //オンライン未接続時のYesボタン押下時
    public void OnClick_FirstNotNetworkYesButton()
    {
        FirstNotNetworkPanel.SetActive(false);
    }

    //メニューボタン押下した際の挙動
    public void OnClick_TitleButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("Title");
    }


    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //画面遷移
            SceneManager.LoadScene("Title");
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
        //画面遷移
        SceneManager.LoadScene("Title");
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

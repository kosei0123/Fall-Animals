using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;

    //コインの枚数を表示
    [SerializeField]
    private Text MyCoinText;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        

        //デバイスに保持されているコインの枚数を表示
        MyCoinText.text = "コイン：" + PlayerPrefs.GetInt("myCoin");

        
    }

    //オフラインボタンを押した際の挙動
    public void OnClick_OfflineButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
    }

    //オンラインボタンを押した際の挙動
    public void OnClick_OnlineButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("EnterLobby");
    }

    //アンロックボタンを押した際の挙動
    public void OnClick_UnlockButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
    }

    //タイトルボタンを押した際の挙動
    public void OnClick_TitleButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("Title");
    }
}

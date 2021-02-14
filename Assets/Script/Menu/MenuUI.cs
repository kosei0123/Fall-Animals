using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    //コインの枚数を表示
    [SerializeField]
    private Text MyCoinText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

        //デバイスに保持されているコインの枚数を表示
        MyCoinText.text = "コイン：" + PlayerPrefs.GetInt("myCoin");

        
    }

    //オンラインボタンを押した際の挙動
    public void OnClick_OnlineButton()
    {
        //画面遷移
        SceneManager.LoadScene("EnterLobby");
    }

    //タイトルボタンを押した際の挙動
    public void OnClick_TitleButton()
    {
        //画面遷移
        SceneManager.LoadScene("Title");
    }
}

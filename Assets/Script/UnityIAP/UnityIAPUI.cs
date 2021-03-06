using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityIAPUI : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //メニューに戻るボタン押下
    public void OnClick_MenuButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //画面遷移
        SceneManager.LoadScene("Menu");
    }
}

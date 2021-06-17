using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnityIAPUI : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

    //スクロールパネルGameObjectの取得
    [SerializeField]
    private GameObject BillingListScrollView;

    //スクロールパネルを動的に動かす
    RectTransform rectTransform;
    private float rectHeight = 0;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //スクロールパネルのRectTransformの変更
        rectTransform = BillingListScrollView.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //スクロールパネルを動的に動かす(10倍速)
        if (rectHeight < 500.0f)
        {
            for (int i = 0; i < 60; i++)
            {
                if (rectHeight < 500.0f) rectHeight++;
            }
        }
            

        rectTransform.sizeDelta = new Vector2(800.0f, rectHeight);
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

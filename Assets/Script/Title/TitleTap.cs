using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class TitleTap : MonoBehaviour
{
    //TitleManagerのpublic定数を取得
    TitleManager titleManager;

    //TapTextオブジェクトを指定する
    [SerializeField]
    private Text TapText;

    // Start is called before the first frame update
    void Start()
    {
        //TitleManagerのpublic定数を取得
        titleManager = GameObject.Find("TitleManager").GetComponent<TitleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //テキストの点滅表示を行う
        TapText.color = GetAlphaColor(TapText.color);

        //画面のどこかをタップした際の動作
        if (Input.GetMouseButtonUp(0))
        {
            //オブジェクトの消去
            Destroy(titleManager.titleAnimal);
            Destroy(titleManager.titleRock);
            //画面遷移
            SceneManager.LoadScene("Menu");
        }
    }

    //テキストの点滅表示を行う関数
    private Color GetAlphaColor(Color color)
    {
        //α値を0~1で取得する
        color.a = (Mathf.Sin(Time.time * 2) / 2) + 0.5f;

        return color;
    }
}

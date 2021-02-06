using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleTap : MonoBehaviour
{
    //TapTextオブジェクトを指定する
    [SerializeField]
    private Text TapText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //テキストの点滅表示を行う
        TapText.color = GetAlphaColor(TapText.color);

        //画面のどこかをタップした際の動作
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("SelectPlayerName");
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

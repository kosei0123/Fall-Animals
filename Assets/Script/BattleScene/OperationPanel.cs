﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationPanel : MonoBehaviour
{
    //ScreenTouchのpublic定数を使用
    ScreenTouch screenTouch;
    //Timerのpublic定数を使う
    Timer timer;
    //Pun2Scriptのpublic定数を使う
    Pun2Script pun2Script;

    //ボタンを取得
    [SerializeField]
    private GameObject UpButtonGameObject;
    [SerializeField]
    private GameObject UpButtonTextGameObject;
    [SerializeField]
    private GameObject DownButtonGameObject;
    [SerializeField]
    private GameObject DownButtonTextGameObject;
    [SerializeField]
    private GameObject RightButtonGameObject;
    [SerializeField]
    private GameObject RightButtonTextGameObject;
    [SerializeField]
    private GameObject LeftButtonGameObject;
    [SerializeField]
    private GameObject LeftButtonTextGameObject;

    // Start is called before the first frame update
    void Start()
    {
        //ScreenTouchのpublic定数を使用
        screenTouch = GameObject.Find("ScreenTouch").GetComponent<ScreenTouch>();
        //Timerのpublic定数を使う
        timer = GameObject.Find("TimerCanvas").GetComponent<Timer>();
        //Pun2Scriptのpublic定数を使う
        pun2Script = GameObject.Find("Pun2").GetComponent<Pun2Script>();

        //テキストを表示する
        UpButtonTextGameObject.SetActive(true);
        DownButtonTextGameObject.SetActive(true);
        RightButtonTextGameObject.SetActive(true);
        LeftButtonTextGameObject.SetActive(true);
        //ボタンを表示する
        UpButtonGameObject.SetActive(true);
        DownButtonGameObject.SetActive(true);
        RightButtonGameObject.SetActive(true);
        LeftButtonGameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //一定時間後に非表示にする
        if (timer.elapsedTime >= 2.0f)
        {
            //テキストを非表示にする
            UpButtonTextGameObject.SetActive(false);
            DownButtonTextGameObject.SetActive(false);
            RightButtonTextGameObject.SetActive(false);
            LeftButtonTextGameObject.SetActive(false);
            //ボタンを透明にする
            UpButtonGameObject.GetComponent<Image>().color = GetAlphaColor(UpButtonGameObject.GetComponent<Image>().color);
            DownButtonGameObject.GetComponent<Image>().color = GetAlphaColor(DownButtonGameObject.GetComponent<Image>().color);
            RightButtonGameObject.GetComponent<Image>().color = GetAlphaColor(RightButtonGameObject.GetComponent<Image>().color);
            LeftButtonGameObject.GetComponent<Image>().color = GetAlphaColor(LeftButtonGameObject.GetComponent<Image>().color);
        }

        //バトル終了時にボタンを非表示にする
        if (pun2Script.battleFinishFlag == true)
        {
            UpButtonGameObject.SetActive(false);
            DownButtonGameObject.SetActive(false);
            RightButtonGameObject.SetActive(false);
            LeftButtonGameObject.SetActive(false);
        }
    }

    //透明にする
    private Color GetAlphaColor(Color color)
    {
        //α値を0で取得する
        color.a = 0;

        return color;
    }
}

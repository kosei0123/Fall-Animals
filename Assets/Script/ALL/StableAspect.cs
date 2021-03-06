﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StableAspect : MonoBehaviour
{

    private Camera refCamera;

    //前の向きと現在の向き
    private string beforeScreenDirection = "";
    private string currentScreenDirection = "";

    // 固定したい表示サイズ
    private bool aspectScreenCheckFlag = false;
    //private static int aspectScreenWidth = 1334;
    //private static int aspectScreenHeight = 750;
    private static int aspectScreenWidth = 1650;
    private static int aspectScreenHeight = 927;

    // 画像のPixel Per Unit
    private float pixelPerUnit = 100f;

    int m_width = -1;
    int m_height = -1;

    void Awake()
    {
        //画面サイズを取得する
        if (aspectScreenCheckFlag == false && Screen.width > Screen.height)
        {
            aspectScreenWidth = Screen.width;
            aspectScreenHeight = Screen.height;
            aspectScreenCheckFlag = true;
        }
        else if (aspectScreenCheckFlag == false && Screen.width < Screen.height)
        {
            aspectScreenWidth = Screen.height;
            aspectScreenHeight = Screen.width;
            aspectScreenCheckFlag = true;
        }

        if (refCamera == null)
        {
            // カメラコンポーネントを取得します
            refCamera = GetComponent<Camera>();
        }
        UpdateCamera();
    }

    void Start()
    {
        
    }

    private void Update()
    {
        //現在の画面の向き取得
        if (Screen.width > Screen.height)
        {
            currentScreenDirection = "Horizon";
        }
        else
        {
            currentScreenDirection = "Vertical";
        }

        //向きが異なっていた場合のみの変更
        if (beforeScreenDirection != currentScreenDirection)
        {
            UpdateCameraWithCheck();
        }
        
    }

    void UpdateCameraWithCheck()
    {
        if (m_width == Screen.width && m_height == Screen.height)
        {
            return;
        }
        UpdateCamera();
    }

    void UpdateCamera()
    {
        float screen_w = (float)Screen.width;
        float screen_h = (float)Screen.height;
        float target_w = (float)aspectScreenWidth;
        float target_h = (float)aspectScreenHeight;

        //アスペクト比
        float aspect = screen_w / screen_h;
        float targetAcpect = target_w / target_h;
        float orthographicSize = (target_h / 2f / pixelPerUnit);

        //縦に長い
        if (aspect < targetAcpect)
        {
            float bgScale_w = target_w / screen_w;
            float camHeight = target_h / (screen_h * bgScale_w);
            refCamera.rect = new Rect(0f, (1f - camHeight) * 0.5f, 1f, camHeight);
        }
        // 横に長い
        else
        {
            // カメラのorthographicSizeを横の長さに合わせて設定しなおす
            float bgScale = aspect / targetAcpect;
            orthographicSize *= bgScale;

            float bgScale_h = target_h / screen_h;
            float camWidth = target_w / (screen_w * bgScale_h);
            refCamera.rect = new Rect((1f - camWidth) * 0.5f, 0f, camWidth, 1f);
        }

        refCamera.orthographicSize = orthographicSize;

        m_width = Screen.width;
        m_height = Screen.height;
    }

}

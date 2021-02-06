﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTouch : MonoBehaviour
{
    //CharacterMainMoveのスクリプトを使用する
    public GameObject target;
    CharacterMainMove characterMainMove;

    //画面の縦3分割
    [HideInInspector]
    public float screenUp;
    [HideInInspector]
    public float screenDown;
    //画面の横2分割
    [HideInInspector]
    public float screenMiddle;


    // Start is called before the first frame update
    void Start()
    {
        //画面の位置取得
        screenUp = (Screen.height * 2) / 3;
        screenDown = Screen.height / 3;
        screenMiddle = Screen.width  / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //1度のみ実行される
        if (target != null && characterMainMove == null)
        {
            //CharacterMainMoveのスクリプトを使用する
            characterMainMove = target.GetComponent<CharacterMainMove>();
        }

        //タップした瞬間
        if (Input.GetMouseButtonDown(0))
        {
            //縦3分割の1番上をタップ(ジャンプ)
            if(Input.mousePosition.y >= screenUp)
            {
                Debug.Log("J");
            }
            //縦3分割の上から2番目をタップ(移動)
            else if (Input.mousePosition.y < screenUp && Input.mousePosition.y > screenDown)
            {
                //横2分割の右側をタップ(右移動)
                if (Input.mousePosition.x > screenMiddle)
                {
                    characterMainMove.moveDirection = 1.0f;
                }
                //横2分割の左側をタップ(左移動)
                else if (Input.mousePosition.x <= screenMiddle)
                {
                    characterMainMove.moveDirection = -1.0f;
                }
                
            }
            //縦3分割の1番下をタップ(しゃがみ)
            else if (Input.mousePosition.y <= screenDown)
            {
                Debug.Log("D");
            }
        }

        //タップしっぱなし
        if (Input.GetMouseButton(0))
        {
            //縦3分割の上から2番目をタップ(移動)
            if (Input.mousePosition.y < screenUp && Input.mousePosition.y > screenDown)
            {
                //横2分割の右側をタップ(右移動)
                if (Input.mousePosition.x > screenMiddle)
                {
                    characterMainMove.moveDirection = 1.0f;
                }
                //横2分割の左側をタップ(左移動)
                else if (Input.mousePosition.x <= screenMiddle)
                {
                    characterMainMove.moveDirection = -1.0f;
                }

            }
        }
        
    }
}

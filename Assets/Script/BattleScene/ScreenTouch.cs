﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTouch : MonoBehaviour
{
    //CharacterMainMoveのスクリプトを使用する
    public GameObject target;
    CharacterMainMove characterMainMove;

    //横向き用+縦向き用
    //画面の縦3分割
    [HideInInspector]
    public float screenUp;
    [HideInInspector]
    public float screenDown;
    //画面の横2分割
    [HideInInspector]
    public float screenMiddle;

    //縦向き用
    //[HideInInspector]
    //public float screenUp_bottom;
    //[HideInInspector]
    //public float screenUp_top;
    //[HideInInspector]
    //public float screenDown_top;

    // Start is called before the first frame update
    void Start()
    {
        //画面タッチ+画面操作方法用
        screenUp = (Screen.height * 2) / 3;
        screenDown = Screen.height / 3;
        screenMiddle = Screen.width / 2;

        ////画面の位置取得(横向き)
        //if (Screen.width > Screen.height)
        //{
            
        //}
        ////画面の位置取得(縦向き)
        //else if (Screen.width < Screen.height)
        //{
        //    //画面タッチ用
        //    screenUp = (Screen.height * 2) / 9;
        //    screenDown = Screen.height / 9;
        //    screenMiddle = Screen.width / 2;
        //    //画面操作方法用
        //    screenUp_bottom = (Screen.height * 7) / 9;
        //    screenUp_top = (Screen.height * 6) / 9;
        //    screenDown_top = (Screen.height * 8) / 9;
        //}
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
        //nullでない場合、何度も呼び出される
        else if (target != null && characterMainMove != null)
        {
            //自分の画面の自キャラのみ操作できるようにする
            if (characterMainMove.onlineflag == false)
            {
                return;
            }
        }
        else
        {
            return;
        }

        //iPhoneまたはANDROIDでの動作
        //if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //{
        //    //マルチタップの実装
        //    var touchCount = Input.touchCount;

        //    for (var i = 0; i < touchCount; i++)
        //    {
        //        var touch = Input.GetTouch(i);
        //        switch (touch.phase)
        //        {
        //            //画面に指が触れたときの処理
        //            case TouchPhase.Began:
        //                //縦3分割の1番上をタップ(ジャンプ)
        //                if (touch.position.y >= screenUp && characterMainMove.sitFlag == false)
        //                {
        //                    characterMainMove.jumpFlag = true;
        //                    characterMainMove.jumpCount++;
        //                }
        //                //縦3分割の上から2番目をタップ(移動)
        //                if (touch.position.y < screenUp && touch.position.y > screenDown)
        //                {
        //                    //しゃがみ時
        //                    if (characterMainMove.sitFlag == true)
        //                    {
        //                        characterMainMove.moveDirection = 0;
        //                    }
        //                    //しゃがんでいない時
        //                    else
        //                    {
        //                        //横2分割の右側をタップ(右移動)
        //                        if (touch.position.x > screenMiddle)
        //                        {
        //                            characterMainMove.moveDirection = 1.0f;
        //                        }
        //                        //横2分割の左側をタップ(左移動)
        //                        else if (touch.position.x <= screenMiddle)
        //                        {
        //                            characterMainMove.moveDirection = -1.0f;
        //                        }
        //                    }
        //                }
        //                //縦3分割の1番下をタップ(しゃがみ)
        //                if (touch.position.y <= screenDown)
        //                {
        //                    //地面に足がついていれば
        //                    if (characterMainMove.isGround == true)
        //                    {
        //                        characterMainMove.moveDirection = 0;
        //                        characterMainMove.sitFlag = true;
        //                    }
        //                }
        //                break;

        //            //画面上で指が動いたときの処理
        //            case TouchPhase.Moved:
        //                //縦3分割の上から2番目をタップ(移動)
        //                if (touch.position.y < screenUp && touch.position.y > screenDown)
        //                {
        //                    //しゃがみ時
        //                    if (characterMainMove.sitFlag == true)
        //                    {
        //                        characterMainMove.moveDirection = 0;
        //                    }
        //                    //しゃがんでいない時
        //                    else
        //                    {
        //                        //横2分割の右側をタップ(右移動)
        //                        if (touch.position.x > screenMiddle)
        //                        {
        //                            characterMainMove.moveDirection = 1.0f;
        //                        }
        //                        //横2分割の左側をタップ(左移動)
        //                        else if (touch.position.x <= screenMiddle)
        //                        {
        //                            characterMainMove.moveDirection = -1.0f;
        //                        }
        //                    }
        //                }
        //                //縦3分割の1番下をタップ(しゃがみ)
        //                if (touch.position.y <= screenDown)
        //                {
        //                    //地面に足がついていれば
        //                    if (characterMainMove.isGround == true)
        //                    {
        //                        characterMainMove.moveDirection = 0;
        //                        characterMainMove.sitFlag = true;
        //                    }
        //                }
        //                break;

        //            //画面に触れてはいるが動いていないときの処理
        //            case TouchPhase.Stationary:
        //                //縦3分割の上から2番目をタップ(移動)
        //                if (touch.position.y < screenUp && touch.position.y > screenDown)
        //                {
        //                    //しゃがみ時
        //                    if (characterMainMove.sitFlag == true)
        //                    {
        //                        characterMainMove.moveDirection = 0;
        //                    }
        //                    //しゃがんでいない時
        //                    else
        //                    {
        //                        //横2分割の右側をタップ(右移動)
        //                        if (touch.position.x > screenMiddle)
        //                        {
        //                            characterMainMove.moveDirection = 1.0f;
        //                        }
        //                        //横2分割の左側をタップ(左移動)
        //                        else if (touch.position.x <= screenMiddle)
        //                        {
        //                            characterMainMove.moveDirection = -1.0f;
        //                        }
        //                    }
        //                }
        //                //縦3分割の1番下をタップ(しゃがみ)
        //                if (touch.position.y <= screenDown)
        //                {
        //                    //地面に足がついていれば
        //                    if (characterMainMove.isGround == true)
        //                    {
        //                        characterMainMove.moveDirection = 0;
        //                        characterMainMove.sitFlag = true;
        //                    }
        //                }
        //                break;

        //            //画面から指が離れたときの処理
        //            case TouchPhase.Ended:
        //                characterMainMove.moveDirection = 0;
        //                characterMainMove.sitFlag = false;
        //                break;

        //            //システムがタッチの追跡をキャンセルしたときの処理
        //            case TouchPhase.Canceled:
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //}

        //エディタでの動作
        //if(Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        //{
        //    //タップした瞬間
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        //縦3分割の1番上をタップ(ジャンプ)
        //        if (Input.mousePosition.y >= screenUp && characterMainMove.sitFlag == false)
        //        {
        //            characterMainMove.jumpFlag = true;
        //            characterMainMove.jumpCount++;
        //        }
        //        //縦3分割の上から2番目をタップ(移動)
        //        if (Input.mousePosition.y < screenUp && Input.mousePosition.y > screenDown)
        //        {
        //            //しゃがみ時
        //            if (characterMainMove.sitFlag == true)
        //            {
        //                characterMainMove.moveDirection = 0;
        //            }
        //            //しゃがんでいない時
        //            else
        //            {
        //                //横2分割の右側をタップ(右移動)
        //                if (Input.mousePosition.x > screenMiddle)
        //                {
        //                    characterMainMove.moveDirection = 1.0f;
        //                }
        //                //横2分割の左側をタップ(左移動)
        //                else if (Input.mousePosition.x <= screenMiddle)
        //                {
        //                    characterMainMove.moveDirection = -1.0f;
        //                }
        //            }

        //        }
        //        //縦3分割の1番下をタップ(しゃがみ)
        //        if (Input.mousePosition.y <= screenDown)
        //        {
        //            //地面に足がついていれば
        //            if (characterMainMove.isGround == true)
        //            {
        //                characterMainMove.moveDirection = 0;
        //                characterMainMove.sitFlag = true;
        //            }
        //        }
        //    }

        //    //タップしっぱなし
        //    if (Input.GetMouseButton(0))
        //    {
        //        //縦3分割の上から2番目をタップ(移動)
        //        if (Input.mousePosition.y < screenUp && Input.mousePosition.y > screenDown)
        //        {
        //            //しゃがみ時
        //            if (characterMainMove.sitFlag == true)
        //            {
        //                characterMainMove.moveDirection = 0;
        //            }
        //            //しゃがんでいない時
        //            else
        //            {
        //                //横2分割の右側をタップ(右移動)
        //                if (Input.mousePosition.x > screenMiddle)
        //                {
        //                    characterMainMove.moveDirection = 1.0f;
        //                }
        //                //横2分割の左側をタップ(左移動)
        //                else if (Input.mousePosition.x <= screenMiddle)
        //                {
        //                    characterMainMove.moveDirection = -1.0f;
        //                }
        //            }
        //        }
        //        //縦3分割の1番下をタップ(しゃがみ)
        //        if (Input.mousePosition.y <= screenDown)
        //        {
        //            //地面に足がついていれば
        //            if (characterMainMove.isGround == true)
        //            {
        //                characterMainMove.moveDirection = 0;
        //                characterMainMove.sitFlag = true;
        //            }
        //        }
        //    }

        //    //タップ解除
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        characterMainMove.moveDirection = 0.0f;
        //        characterMainMove.sitFlag = false;
        //    }
        //}
    }

    //ジャンプ
    public void PointerDown_UpButton()
    {
        if (characterMainMove.sitFlag == false && characterMainMove.jumpCount == 0)
        {
            characterMainMove.jumpFlag = true;
            characterMainMove.jumpCount++;
        }
        
    }

    //右移動(押下しっぱなし)
    public void PointerDown_RightButton()
    {
        //しゃがみ時
        if (characterMainMove.sitFlag == true)
        {
            characterMainMove.moveDirection = 0;
        }
        //しゃがんでいない時
        else
        {
            characterMainMove.moveDirection = 1.0f;
        }
    }

    //右移動(離す)
    public void PointerUp_RightButton()
    {
        if (characterMainMove.moveDirection > 0)
        {
            characterMainMove.moveDirection = 0.0f;
        }
        
    }

    //左移動(押下しっぱなし)
    public void PointerDown_LeftButton()
    {
        //しゃがみ時
        if (characterMainMove.sitFlag == true)
        {
            characterMainMove.moveDirection = 0;
        }
        //しゃがんでいない時
        else
        {
            characterMainMove.moveDirection = -1.0f;
        }
    }

    //左移動(離す)
    public void PointerUp_LeftButton()
    {
        if (characterMainMove.moveDirection < 0)
        {
            characterMainMove.moveDirection = 0.0f;
        }
    }

    //下(押下しっぱなし)
    public void PointerDown_DownButton()
    {
        if (characterMainMove.isGround == true)
        {
            characterMainMove.moveDirection = 0;
            characterMainMove.sitFlag = true;
        }
        
    }

    //下(離す)
    public void PointerUp_DownButton()
    {
        characterMainMove.sitFlag = false;
        
        
    }

    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(150, 30, 150, 70), "test : " + test);
        //GUI.TextField(new Rect(650, 30, 150, 70), "test2 : " + test2);

    }
}

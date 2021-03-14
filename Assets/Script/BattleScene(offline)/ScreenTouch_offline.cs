using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTouch_offline : MonoBehaviour
{
    //CharacterMainMoveのスクリプトを使用する
    public GameObject target;
    CharacterMainMove_offline characterMainMove_offline;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        //1度のみ実行される
        if (target != null && characterMainMove_offline == null)
        {
            //CharacterMainMoveのスクリプトを使用する
            characterMainMove_offline = target.GetComponent<CharacterMainMove_offline>();
        }
        //nullでない場合、何度も呼び出される
        else if (target != null && characterMainMove_offline != null)
        {
            //自分の画面の自キャラのみ操作できるようにする
            if (characterMainMove_offline.offlineflag == false)
            {
                return;
            }
        }
        else
        {
            return;
        }

        //iPhoneまたはANDROIDでの動作
        //if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
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
        //                if (touch.position.y >= screenUp && characterMainMove_offline.sitFlag == false)
        //                {
        //                    characterMainMove_offline.jumpFlag = true;
        //                    characterMainMove_offline.jumpCount++;
        //                }
        //                //縦3分割の上から2番目をタップ(移動)
        //                if (touch.position.y < screenUp && touch.position.y > screenDown)
        //                {
        //                    //しゃがみ時
        //                    if (characterMainMove_offline.sitFlag == true)
        //                    {
        //                        characterMainMove_offline.moveDirection = 0;
        //                    }
        //                    //しゃがんでいない時
        //                    else
        //                    {
        //                        //横2分割の右側をタップ(右移動)
        //                        if (touch.position.x > screenMiddle)
        //                        {
        //                            characterMainMove_offline.moveDirection = 1.0f;
        //                        }
        //                        //横2分割の左側をタップ(左移動)
        //                        else if (touch.position.x <= screenMiddle)
        //                        {
        //                            characterMainMove_offline.moveDirection = -1.0f;
        //                        }
        //                    }
        //                }
        //                //縦3分割の1番下をタップ(しゃがみ)
        //                if (touch.position.y <= screenDown)
        //                {
        //                    //地面に足がついていれば
        //                    if (characterMainMove_offline.isGround == true)
        //                    {
        //                        characterMainMove_offline.moveDirection = 0;
        //                        characterMainMove_offline.sitFlag = true;
        //                    }
        //                }
        //                break;

        //            //画面上で指が動いたときの処理
        //            case TouchPhase.Moved:
        //                //縦3分割の上から2番目をタップ(移動)
        //                if (touch.position.y < screenUp && touch.position.y > screenDown)
        //                {
        //                    //しゃがみ時
        //                    if (characterMainMove_offline.sitFlag == true)
        //                    {
        //                        characterMainMove_offline.moveDirection = 0;
        //                    }
        //                    //しゃがんでいない時
        //                    else
        //                    {
        //                        //横2分割の右側をタップ(右移動)
        //                        if (touch.position.x > screenMiddle)
        //                        {
        //                            characterMainMove_offline.moveDirection = 1.0f;
        //                        }
        //                        //横2分割の左側をタップ(左移動)
        //                        else if (touch.position.x <= screenMiddle)
        //                        {
        //                            characterMainMove_offline.moveDirection = -1.0f;
        //                        }
        //                    }
        //                }
        //                //縦3分割の1番下をタップ(しゃがみ)
        //                if (touch.position.y <= screenDown)
        //                {
        //                    //地面に足がついていれば
        //                    if (characterMainMove_offline.isGround == true)
        //                    {
        //                        characterMainMove_offline.moveDirection = 0;
        //                        characterMainMove_offline.sitFlag = true;
        //                    }
        //                }
        //                break;

        //            //画面に触れてはいるが動いていないときの処理
        //            case TouchPhase.Stationary:
        //                //縦3分割の上から2番目をタップ(移動)
        //                if (touch.position.y < screenUp && touch.position.y > screenDown)
        //                {
        //                    //しゃがみ時
        //                    if (characterMainMove_offline.sitFlag == true)
        //                    {
        //                        characterMainMove_offline.moveDirection = 0;
        //                    }
        //                    //しゃがんでいない時
        //                    else
        //                    {
        //                        //横2分割の右側をタップ(右移動)
        //                        if (touch.position.x > screenMiddle)
        //                        {
        //                            characterMainMove_offline.moveDirection = 1.0f;
        //                        }
        //                        //横2分割の左側をタップ(左移動)
        //                        else if (touch.position.x <= screenMiddle)
        //                        {
        //                            characterMainMove_offline.moveDirection = -1.0f;
        //                        }
        //                    }
        //                }
        //                //縦3分割の1番下をタップ(しゃがみ)
        //                if (touch.position.y <= screenDown)
        //                {
        //                    //地面に足がついていれば
        //                    if (characterMainMove_offline.isGround == true)
        //                    {
        //                        characterMainMove_offline.moveDirection = 0;
        //                        characterMainMove_offline.sitFlag = true;
        //                    }
        //                }
        //                break;

        //            //画面から指が離れたときの処理
        //            case TouchPhase.Ended:
        //                characterMainMove_offline.moveDirection = 0;
        //                characterMainMove_offline.sitFlag = false;
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
        //if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        //{
        //    //タップした瞬間
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        //縦3分割の1番上をタップ(ジャンプ)
        //        if (Input.mousePosition.y >= screenUp && characterMainMove_offline.sitFlag == false)
        //        {
        //            characterMainMove_offline.jumpFlag = true;
        //            characterMainMove_offline.jumpCount++;
        //        }
        //        //縦3分割の上から2番目をタップ(移動)
        //        if (Input.mousePosition.y < screenUp && Input.mousePosition.y > screenDown)
        //        {
        //            //しゃがみ時
        //            if (characterMainMove_offline.sitFlag == true)
        //            {
        //                characterMainMove_offline.moveDirection = 0;
        //            }
        //            //しゃがんでいない時
        //            else
        //            {
        //                //横2分割の右側をタップ(右移動)
        //                if (Input.mousePosition.x > screenMiddle)
        //                {
        //                    characterMainMove_offline.moveDirection = 1.0f;
        //                }
        //                //横2分割の左側をタップ(左移動)
        //                else if (Input.mousePosition.x <= screenMiddle)
        //                {
        //                    characterMainMove_offline.moveDirection = -1.0f;
        //                }
        //            }

        //        }
        //        //縦3分割の1番下をタップ(しゃがみ)
        //        if (Input.mousePosition.y <= screenDown)
        //        {
        //            //地面に足がついていれば
        //            if (characterMainMove_offline.isGround == true)
        //            {
        //                characterMainMove_offline.moveDirection = 0;
        //                characterMainMove_offline.sitFlag = true;
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
        //            if (characterMainMove_offline.sitFlag == true)
        //            {
        //                characterMainMove_offline.moveDirection = 0;
        //            }
        //            //しゃがんでいない時
        //            else
        //            {
        //                //横2分割の右側をタップ(右移動)
        //                if (Input.mousePosition.x > screenMiddle)
        //                {
        //                    characterMainMove_offline.moveDirection = 1.0f;
        //                }
        //                //横2分割の左側をタップ(左移動)
        //                else if (Input.mousePosition.x <= screenMiddle)
        //                {
        //                    characterMainMove_offline.moveDirection = -1.0f;
        //                }
        //            }
        //        }
        //        //縦3分割の1番下をタップ(しゃがみ)
        //        if (Input.mousePosition.y <= screenDown)
        //        {
        //            //地面に足がついていれば
        //            if (characterMainMove_offline.isGround == true)
        //            {
        //                characterMainMove_offline.moveDirection = 0;
        //                characterMainMove_offline.sitFlag = true;
        //            }
        //        }
        //    }

        //    //タップ解除
        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        characterMainMove_offline.moveDirection = 0.0f;
        //        characterMainMove_offline.sitFlag = false;
        //    }
        //}
    }

    //ジャンプ
    public void OnClick_UpButton()
    {
        if (characterMainMove_offline.sitFlag == false)
        {
            characterMainMove_offline.jumpFlag = true;
            characterMainMove_offline.jumpCount++;
        }
    }

    //右移動(押下しっぱなし)
    public void PointerDown_RightButton()
    {
        //しゃがみ時
        if (characterMainMove_offline.sitFlag == true)
        {
            characterMainMove_offline.moveDirection = 0;
        }
        //しゃがんでいない時
        else
        {
            characterMainMove_offline.moveDirection = 1.0f;
        }
    }

    //右移動(離す)
    public void PointerUp_RightButton()
    {
        if (characterMainMove_offline.moveDirection > 0)
        {
            characterMainMove_offline.moveDirection = 0.0f;
        }
        
    }

    //左移動(押下しっぱなし)
    public void PointerDown_LeftButton()
    {
        //しゃがみ時
        if (characterMainMove_offline.sitFlag == true)
        {
            characterMainMove_offline.moveDirection = 0;
        }
        //しゃがんでいない時
        else
        {
            characterMainMove_offline.moveDirection = -1.0f;
        }
    }

    //左移動(離す)
    public void PointerUp_LeftButton()
    {
        if (characterMainMove_offline.moveDirection < 0)
        {
            characterMainMove_offline.moveDirection = 0.0f;
        }
        
    }

    //下(押下しっぱなし)
    public void PointerDown_DownButton()
    {
        if (characterMainMove_offline.isGround == true)
        {
            characterMainMove_offline.moveDirection = 0;
            characterMainMove_offline.sitFlag = true;
        }
    }

    //下(離す)
    public void PointerUp_DownButton()
    {
        characterMainMove_offline.sitFlag = false;
    }

    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(150, 30, 150, 70), "test : " + test);
        //GUI.TextField(new Rect(650, 30, 150, 70), "test2 : " + test2);

    }
}

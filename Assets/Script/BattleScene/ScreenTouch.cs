using System.Collections;
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
        Debug.Log(Screen.height);
        Debug.Log(Screen.width);
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
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //マルチタップの実装
            var touchCount = Input.touchCount;

            for (var i = 0; i < touchCount; i++)
            {
                var touch = Input.GetTouch(i);
                switch (touch.phase)
                {
                    //画面に指が触れたときの処理
                    case TouchPhase.Began:
                        //縦3分割の1番上をタップ(ジャンプ)
                        if (touch.position.y >= screenUp)
                        {
                            characterMainMove.jumpFlag = true;
                            characterMainMove.jumpCount++;
                        }
                        //縦3分割の上から2番目をタップ(移動)
                        if (touch.position.y < screenUp && touch.position.y > screenDown)
                        {
                            //横2分割の右側をタップ(右移動)
                            if (touch.position.x > screenMiddle)
                            {
                                characterMainMove.moveDirection = 1.0f;
                            }
                            //横2分割の左側をタップ(左移動)
                            else if (touch.position.x <= screenMiddle)
                            {
                                characterMainMove.moveDirection = -1.0f;
                            }

                        }
                        //縦3分割の1番下をタップ(しゃがみ)
                        if (touch.position.y <= screenDown && characterMainMove.isGround == true)
                        {
                            Debug.Log("D");
                        }
                        break;

                    //画面上で指が動いたときの処理
                    case TouchPhase.Moved:
                        //縦3分割の上から2番目をタップ(移動)
                        if (touch.position.y < screenUp && touch.position.y > screenDown)
                        {
                            //横2分割の右側をタップ(右移動)
                            if (touch.position.x > screenMiddle)
                            {
                                characterMainMove.moveDirection = 1.0f;
                            }
                            //横2分割の左側をタップ(左移動)
                            else if (touch.position.x <= screenMiddle)
                            {
                                characterMainMove.moveDirection = -1.0f;
                            }
                        }
                        break;

                    //画面に触れてはいるが動いていないときの処理
                    case TouchPhase.Stationary:
                        //縦3分割の上から2番目をタップ(移動)
                        if (touch.position.y < screenUp && touch.position.y > screenDown)
                        {
                            //横2分割の右側をタップ(右移動)
                            if (touch.position.x > screenMiddle)
                            {
                                characterMainMove.moveDirection = 1.0f;
                            }
                            //横2分割の左側をタップ(左移動)
                            else if (touch.position.x <= screenMiddle)
                            {
                                characterMainMove.moveDirection = -1.0f;
                            }
                        }
                        break;

                    //画面から指が離れたときの処理
                    case TouchPhase.Ended:
                        //横2分割の右側をタップ解除(右移動)
                        if (touch.position.x > screenMiddle)
                        {
                            characterMainMove.moveDirection = 0.0f;
                        }
                        //横2分割の左側をタップ解除(左移動)
                        else if (touch.position.x <= screenMiddle)
                        {
                            characterMainMove.moveDirection = 0.0f;
                        }
                        break;

                    //システムがタッチの追跡をキャンセルしたときの処理
                    case TouchPhase.Canceled:
                        break;

                    default:
                        break;
                }
            }
        }

        //エディタでの動作
        if(Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            //タップした瞬間
            if (Input.GetMouseButtonDown(0))
            {
                //縦3分割の1番上をタップ(ジャンプ)
                if (Input.mousePosition.y >= screenUp && characterMainMove.jumpCount == 0)
                {
                    characterMainMove.jumpFlag = true;
                    characterMainMove.jumpCount++;
                }
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
                //縦3分割の1番下をタップ(しゃがみ)
                if (Input.mousePosition.y <= screenDown && characterMainMove.isGround == true)
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

            //タップ解除
            if (Input.GetMouseButtonUp(0))
            {
                //横2分割の右側をタップ解除(右移動)
                if (Input.mousePosition.x > screenMiddle)
                {
                    characterMainMove.moveDirection = 0.0f;
                }
                //横2分割の左側をタップ解除(左移動)
                else if (Input.mousePosition.x <= screenMiddle)
                {
                    characterMainMove.moveDirection = 0.0f;
                }
            }
        }
    }

    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(150, 30, 150, 70), "test : " + test);
        //GUI.TextField(new Rect(650, 30, 150, 70), "test2 : " + test2);

    }
}

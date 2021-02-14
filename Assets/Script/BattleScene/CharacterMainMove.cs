using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CharacterMainMove : MonoBehaviourPunCallbacks,IPunObservable
{
    //Pun2Scriptのpublic定数を使う
    Pun2Script pun2Script;

    //初期状態でfalseを入れ、オンライン時に自プレイヤーのみ操作できるようにする
    public bool onlineflag = false;

    //キャラクターにかかる重力や摩擦
    public Rigidbody rb;
    //キャラクターのニックネームを取得
    public Text NameText;
    //ニックネームの表示位置の調整
    private Vector3 nickNamePositionTweak = new Vector3(0, 2.0f, 0);
    //プレイヤーのワールド座標
    private Vector3 playerWorldPosition;
    //ラグ時の位置予想
    private Vector3 networkPosition;

    //キャラクターの移動方向
    [HideInInspector]
    public float moveDirection;
    //速度
    private float runSpeed = 5.0f;

    //ジャンプフラグ
    [HideInInspector]
    public bool jumpFlag = false;
    // ジャンプ威力
    [HideInInspector]
    public float jumpPower = 7.0f;
    //ジャンプ回数
    [HideInInspector]
    public int jumpCount = 0;

    //アニメーション
    public Animator anim;

    //キャラクターの位置や向きのキャッシュ用
    public Transform transformCache;

    //地面にいるかフラグ
    public bool isGround = false;
    //地面チェックのコライダー
    public Collider groudCheck_Collider;

    //ニックネームフラグ
    public bool animal1NickNameFlag = false;
    public bool animal2NickNameFlag = false;
    public bool animal3NickNameFlag = false;
    public bool animal4NickNameFlag = false;


    public void SetFlag(bool f)
    {
        onlineflag = f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Pun2Scriptのpublic定数を使う
        pun2Script = GameObject.Find("Pun2").GetComponent<Pun2Script>();

        //FPSを60に設定
        Application.targetFrameRate = 60;

        //重力や摩擦
        rb = this.GetComponent<Rigidbody>();
        // Animatorコンポーネントを取得する
        anim = this.GetComponent<Animator>();

        //Transformをキャッシュする
        transformCache = transform;
    }

    // Update is called once per frame
    void Update()
    {

        //自分の画面の自キャラのみ操作できるようにする
        if (onlineflag == false)
        {
            return;
        }

        //GroundCheckをtrueに
        groudCheck_Collider.enabled = true;
        //ニックネームを表示
        NameText.text = PhotonNetwork.LocalPlayer.NickName;
        //NameText.text = pun2Script.GetAnimalInformation().NickName;
        NameText.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position + nickNamePositionTweak);

        //プレイヤーの向きの反転
        if ((transformCache.localScale.z < 0 && moveDirection > 0.1) || (transformCache.localScale.z > 0 && moveDirection < -0.1))
        {
            transformCache.localScale = new Vector3(transformCache.localScale.x, transformCache.localScale.y, transformCache.localScale.z * (-1));
        }

        // Animator側で設定している"Speed"パラメタにmoveDirectionを渡す
        anim.SetFloat("Speed", moveDirection);

        
        //画面外にでないように制御
        //定義
        playerWorldPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
        //条件式
        if ((playerWorldPosition.x > 0 && playerWorldPosition.x < Screen.width) ||
            (playerWorldPosition.x <= 0 && moveDirection > 0.1) || (playerWorldPosition.x >= Screen.width && moveDirection < -0.1))
        {
            //キャラクターの移動処理
            rb.velocity = new Vector3(moveDirection * runSpeed, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }


        //ジャンプ
        if (jumpFlag == true && jumpCount == 1)
        {
            //上方向に力を加える
            rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            //ジャンプアニメーション
            anim.SetBool("Jump", true);
            jumpFlag = false;
        }
        else
        {
            //ジャンプアニメーションをきる
            anim.SetBool("Jump", false);
        }

        //接地判定
        //接地している
        if (isGround == true)
        {
            jumpCount = 0;
            //アニメーションの速度変更
            anim.SetFloat("RunSpeed", 1.0f);
        }
        //接地していない
        else
        {
            if (jumpCount == 0)
            {
                jumpCount = 1;
            }
            //アニメーションの速度変更
            anim.SetFloat("RunSpeed", 0.6f);

        }

    }

    //同期
    [System.Obsolete]
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //データの送信
            //名前
            stream.SendNext(this.name);
            //位置と加速度
            stream.SendNext(rb.velocity);
        }
        else
        {
            //データの受信
            //名前
            gameObject.name = (string)stream.ReceiveNext();
            //位置と加速度
            GetComponent<Rigidbody>().velocity = (Vector3)stream.ReceiveNext();

            if (animal1NickNameFlag == false || animal2NickNameFlag == false || animal3NickNameFlag == false || animal4NickNameFlag == false)
            {
                ShowNickName();
            }
        }
    }

    //他プレイヤー画面にてニックネームの共有
    private void ShowNickName()
    {
        if (gameObject.name == pun2Script.animalName + "1")
        {
            gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimalInformation().NickName;
            animal1NickNameFlag = true;
        }
        if (gameObject.name == pun2Script.animalName + "2")
        {
            gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimal2Information().NickName;
            animal2NickNameFlag = true;
        }
        if (gameObject.name == pun2Script.animalName + "3")
        {
            gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimal3Information().NickName;
            animal3NickNameFlag = true;
        }
        if (gameObject.name == pun2Script.animalName + "4")
        {
            gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimal4Information().NickName;
            animal4NickNameFlag = true;
        }
    }
}

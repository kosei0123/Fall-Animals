using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CharacterMainMove : MonoBehaviourPunCallbacks
{
    //初期状態でfalseを入れ、オンライン時に自プレイヤーのみ操作できるようにする
    public bool onlineflag = false;

    // プレイヤーへの参照
    public GameObject player;
    //キャラクターにかかる重力や摩擦
    public Rigidbody rb;
    //キャラクターのニックネームを取得
    public Text NameText;
    //ニックネームの表示位置の調整
    private Vector3 nickNamePositionTweak = new Vector3(0, 2.0f, 0);


    //キャラクターの移動方向
    [HideInInspector]
    public float moveDirection;
    //速度
    private float runSpeed = 5.0f;

    //ジャンプフラグ
    public bool jumpFlag = false;
    // ジャンプ威力
    private float jumpPower = 7.0f;
    //ジャンプ回数
    public int jumpCount = 0;

    //アニメーション
    public Animator anim;

    //キャラクターの位置や向きのキャッシュ用
    public Transform transformCache;

    //地面にいるかフラグ
    public bool isGround = false;
    //地面チェックのコライダー
    public static Collider groudCheck_Collider;


    public void SetFlag(bool f)
    {
        onlineflag = f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //FPSを60に設定
        Application.targetFrameRate = 60;

        //プレイヤーへの参照
        player = GameObject.FindWithTag("Player");
        //重力や摩擦
        rb = player.GetComponent<Rigidbody>();
        // Animatorコンポーネントを取得する
        anim = player.GetComponent<Animator>();

        //Transformをキャッシュする
        transformCache = transform;

        //地面チェックのコライダー
        groudCheck_Collider = GameObject.Find("GroundCheck").GetComponent<BoxCollider>();
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
        NameText.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main,player.transform.position + nickNamePositionTweak);

        //プレイヤーの向きの反転
        if ((transformCache.localScale.z < 0 && moveDirection > 0.1) || (transformCache.localScale.z > 0 && moveDirection < -0.1))
        {
            transformCache.localScale = new Vector3(transformCache.localScale.x, transformCache.localScale.y, transformCache.localScale.z * (-1));
        }

        // Animator側で設定している"Speed"パラメタにhを渡す
        anim.SetFloat("Speed", moveDirection);
        //キャラクターの移動処理
        rb.velocity = new Vector3(moveDirection * runSpeed, rb.velocity.y, 0);

        //ジャンプ
        if(jumpFlag == true && jumpCount == 1)
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
}

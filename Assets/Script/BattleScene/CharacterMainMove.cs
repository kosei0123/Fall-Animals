using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterMainMove : MonoBehaviourPunCallbacks
{
    //初期状態でfalseを入れ、オンライン時に自プレイヤーのみ操作できるようにする
    public bool onlineflag = false;

    // プレイヤーへの参照
    public GameObject player;
    //キャラクターにかかる重力や摩擦
    public Rigidbody rb;

    //キャラクターの移動方向
    [HideInInspector]
    public float moveDirection;
    // キャラクターコントローラ（カプセルコライダ）の移動量
    private Vector3 velocity;
    //速度
    private float runSpeed = 5.0f;

    //ジャンプフラグ
    public bool jumpFlag = false;
    // ジャンプ威力
    private float jumpPower = 7.0f;

    //アニメーション
    public Animator anim;

    //キャラクターの位置や向きのキャッシュ用
    public Transform transformCache;


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
    }

    // Update is called once per frame
    void Update()
    {
        //自分の画面の自キャラのみ操作できるようにする
        if (onlineflag == false)
        {
            return;
        }

        //プレイヤーの向きの反転
        if ((transformCache.localScale.z < 0 && moveDirection > 0.1) || (transformCache.localScale.z > 0 && moveDirection < -0.1))
        {
            transformCache.localScale = new Vector3(transformCache.localScale.x, transformCache.localScale.y, transform.localScale.z * (-1));
        }

        // Animator側で設定している"Speed"パラメタにhを渡す
        anim.SetFloat("Speed", moveDirection);
        //キャラクターの移動処理
        rb.velocity = new Vector3(moveDirection * runSpeed, rb.velocity.y, 0);

        //ジャンプ
        if(jumpFlag == true)
        {
            //上方向に力を加える
            rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            anim.SetBool("Jump", true);     // Animatorにジャンプに切り替えるフラグを送る
            jumpFlag = false;
        }

    }
}

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
    private float runSpeed = 40.0f;

    //アニメーション
    public Animator anim;


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

    }

    // Update is called once per frame
    void Update()
    {
        //自分の画面の自キャラのみ操作できるようにする
        if (onlineflag == false)
        {
            return;
        }

        // Animator側で設定している"Speed"パラメタにhを渡す
        anim.SetFloat("Speed", moveDirection);
        // 以下、キャラクターの移動処理
        velocity = new Vector3(moveDirection, 0, 0);
        rb.velocity = new Vector3(velocity.x * runSpeed, rb.velocity.y, 0);

        Debug.Log(rb.velocity);

    }
}

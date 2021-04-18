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
    private Vector3 nickNamePositionTweak = new Vector3(0, 4.0f, 0);
    //プレイヤーのワールド座標
    private Vector3 playerWorldPosition;
    //ラグ時の位置予想
    private Vector3 networkPosition;

    //メッシュコライダ
    private Collider meshCol;
    //ボックスコライダ
    private Collider boxCol;

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

    //しゃがみフラグ
    [HideInInspector]
    public bool sitFlag = false;

    //アニメーション
    public Animator anim;

    //すり抜け床でない地面にいるときにフラグ(レイヤー用)
    public bool GroundSlidingFlag = false;

    //パーティクル
    public ParticleSystem particle;

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
        //パーティクルを取得する
        particle = this.GetComponent<ParticleSystem>();

        //コライダの設定
        //メッシュコライダーの設定
        meshCol = this.transform.GetChild(0).gameObject.GetComponent<MeshCollider>();
        //ボックスコライダーの設定
        boxCol = this.GetComponent<BoxCollider>();

        //ニックネームを表示(ID抜き取る)
        NameText.text = PhotonNetwork.LocalPlayer.NickName.Substring(0, PhotonNetwork.LocalPlayer.NickName.LastIndexOf("("));

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

        //Transformをキャッシュする
        if (transformCache == null)
        {
            transformCache = transform;
        }

        //GroundCheckをtrueに
        groudCheck_Collider.enabled = true;
        //ニックネームを表示(ID抜き取る)
        NameText.text = PhotonNetwork.LocalPlayer.NickName.Substring(0, PhotonNetwork.LocalPlayer.NickName.LastIndexOf("("));
        //プレイ動画用
        //NameText.text = PhotonNetwork.LocalPlayer.CustomProperties["playerCreatedNumber"] + "P";
        NameText.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position + nickNamePositionTweak);

        //ジャンプ力の設定
        AnimalAbilitySettings();

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
        if (jumpFlag == true)
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

        //しゃがみ
        if (sitFlag == true)
        {
            anim.SetBool("Sit", true);
            //コライダーの設定
            meshCol.enabled = false;
            boxCol.enabled = true;
            
        }
        else
        {
            anim.SetBool("Sit", false);
            //コライダーの設定
            meshCol.enabled = true;
            boxCol.enabled = false;
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

        //レイヤー
        //ジャンプ中のレイヤー変更
        if (this.rb.velocity.y > 0)
        {
            //SlidingPlayer
            this.gameObject.layer = 13;
            //子オブジェクトもレイヤー変更
            foreach (Transform childTransform in gameObject.transform)
            {
                childTransform.gameObject.layer = 13;
            }

        }
        //すり抜け床でない地面 or プレイヤーの上にいるとき
        else if (GroundSlidingFlag == false)
        {
            //SlidingPlayer
            this.gameObject.layer = 13;
            //子オブジェクトもレイヤー変更
            foreach (Transform childTransform in gameObject.transform)
            {
                childTransform.gameObject.layer = 13;
            }
        }
        else
        {
            //Player
            this.gameObject.layer = 10;
            //子オブジェクトもレイヤー変更
            foreach (Transform childTransform in gameObject.transform)
            {
                childTransform.gameObject.layer = 10;
            }
        }

    }



    /// <summary>
    /// スピード&ジャンプ力の設定
    /// </summary>
    private void AnimalAbilitySettings()
    {
        //キリン
        if (SelectCharacterUI.animalName == "Giraffe")
        {
            //ジャンプ力
            jumpPower = 9.0f;
            //スピード
            if (isGround)
            {
                runSpeed = 4.5f;
            }
            else
            {
                runSpeed = 4.5f;
            }
            
        }
        //象
        else if (SelectCharacterUI.animalName == "Elephant")
        {
            //ジャンプ力
            jumpPower = 8.5f;
            //スピード
            if (isGround)
            {
                runSpeed = 6.5f;
            }
            else
            {
                runSpeed = 6.0f;
            }
        }
        //犬
        else if (SelectCharacterUI.animalName == "Dog")
        {
            //ジャンプ力
            jumpPower = 13.0f;
            //スピード
            if (isGround)
            {
                runSpeed = 12.0f;
            }
            else
            {
                runSpeed = 7.0f;
            }
        }
        //虎
        else if (SelectCharacterUI.animalName == "Tiger")
        {
            //ジャンプ力
            jumpPower = 12.5f;
            //スピード
            if (isGround)
            {
                runSpeed = 15.0f;
            }
            else
            {
                runSpeed = 8.0f;
            }
        }
    }

    //同期
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        //Pun2Scriptのpublic定数を使う
        pun2Script = GameObject.Find("Pun2").GetComponent<Pun2Script>();

        //コライダの設定
        //メッシュコライダーの設定
        meshCol = this.transform.GetChild(0).gameObject.GetComponent<MeshCollider>();
        //ボックスコライダーの設定
        boxCol = this.GetComponent<BoxCollider>();

        if (stream.IsWriting)
        {
            //データの送信
            //名前
            stream.SendNext(this.name);
            //レイヤー
            stream.SendNext(this.gameObject.layer);
            //foreach (Transform childTransform in gameObject.transform)
            //{
            //    stream.SendNext(childTransform.gameObject.layer);
            //}
            stream.SendNext(this.transform.GetChild(0).gameObject.layer);
            stream.SendNext(this.transform.GetChild(1).gameObject.layer);
            stream.SendNext(this.transform.GetChild(2).gameObject.layer);
            stream.SendNext(this.transform.GetChild(3).gameObject.layer);
            //コライダー
            stream.SendNext(meshCol.enabled);
            stream.SendNext(boxCol.enabled);
            //位置と加速度
            stream.SendNext(rb.velocity);
        }
        else
        {
            //データの受信
            //名前
            gameObject.name = (string)stream.ReceiveNext();
            //レイヤー
            gameObject.layer = (int)stream.ReceiveNext();
            //foreach (Transform childTransform in gameObject.transform)
            //{
            //    childTransform.gameObject.layer = (int)stream.ReceiveNext();
            //}
            transform.GetChild(0).gameObject.layer = (int)stream.ReceiveNext();
            transform.GetChild(1).gameObject.layer = (int)stream.ReceiveNext();
            transform.GetChild(2).gameObject.layer = (int)stream.ReceiveNext();
            transform.GetChild(3).gameObject.layer = (int)stream.ReceiveNext();
            //コライダー
            this.transform.GetChild(0).gameObject.GetComponent<MeshCollider>().enabled = (bool)stream.ReceiveNext();
            this.gameObject.GetComponent <BoxCollider>().enabled = (bool)stream.ReceiveNext();
            //位置と加速度
            GetComponent<Rigidbody>().velocity = (Vector3)stream.ReceiveNext();

            //ニックネームの表示
            if (animal1NickNameFlag == false || animal2NickNameFlag == false || animal3NickNameFlag == false || animal4NickNameFlag == false)
            {
                ShowNickName();                
            }
        }
    }

    //他プレイヤー画面にてニックネームの共有
    private void ShowNickName()
    {
        if (gameObject.name == "animal1" && pun2Script.GetAnimalInformation() != null)
        {
            gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimalInformation().NickName.Substring(0, pun2Script.GetAnimalInformation().NickName.LastIndexOf("("));
            animal1NickNameFlag = true;
        }
        if (gameObject.name == "animal2" && pun2Script.GetAnimal2Information() != null)
        {
            gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimal2Information().NickName.Substring(0, pun2Script.GetAnimal2Information().NickName.LastIndexOf("("));
            animal2NickNameFlag = true;
        }
        if (gameObject.name == "animal3" && pun2Script.GetAnimal3Information() != null)
        {
            gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimal3Information().NickName.Substring(0, pun2Script.GetAnimal3Information().NickName.LastIndexOf("("));
            animal3NickNameFlag = true;
        }
        if (gameObject.name == "animal4" && pun2Script.GetAnimal4Information() != null)
        {
            gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimal4Information().NickName.Substring(0, pun2Script.GetAnimal4Information().NickName.LastIndexOf("("));
            animal4NickNameFlag = true;
        }

        //動画用
        //if (gameObject.name == "animal1" && pun2Script.GetAnimalInformation() != null)
        //{
        //    gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimalInformation().CustomProperties["playerCreatedNumber"] + "P";
        //    animal1NickNameFlag = true;
        //}
        //if (gameObject.name == "animal2" && pun2Script.GetAnimal2Information() != null)
        //{
        //    gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimal2Information().CustomProperties["playerCreatedNumber"] + "P";
        //    animal2NickNameFlag = true;
        //}
        //if (gameObject.name == "animal3" && pun2Script.GetAnimal3Information() != null)
        //{
        //    gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimal3Information().CustomProperties["playerCreatedNumber"] + "P";
        //    animal3NickNameFlag = true;
        //}
        //if (gameObject.name == "animal4" && pun2Script.GetAnimal4Information() != null)
        //{
        //    gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = pun2Script.GetAnimal4Information().CustomProperties["playerCreatedNumber"] + "P";
        //    animal4NickNameFlag = true;
        //}

    }

    //順位表示処理
    private void OnGUI()
    {
        if(onlineflag == false)
        {
            return;
        }
        //GUI.TextField(new Rect(400, 30, 150, 70), "isGround : " + isGround);

    }
}

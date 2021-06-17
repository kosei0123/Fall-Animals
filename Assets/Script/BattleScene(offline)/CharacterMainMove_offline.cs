using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMainMove_offline : MonoBehaviour
{
    //Pun2Scriptのpublic定数を使う
    BattleScene_offlineManager battleScene_offlineManager;

    //初期状態でtrueをいれる
    public bool offlineflag;

    //キャラクターにかかる重力や摩擦
    public Rigidbody rb;
    //キャラクターのニックネームを取得
    public Text NameText;
    //ニックネームの表示位置の調整
    private Vector3 nickNamePositionTweak = new Vector3(0, 4.0f, 0);
    //プレイヤーのワールド座標
    private Vector3 playerWorldPosition;

    //メッシュコライダ
    private Collider[] meshboxCol;
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

    //トランポリンジャンプ
    public float trampolineJumpPower = 1.0f;

    public void SetFlag(bool f)
    {
        offlineflag = f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Pun2Scriptのpublic定数を使う
        battleScene_offlineManager = GameObject.Find("BattleScene_offlineManager").GetComponent<BattleScene_offlineManager>();

        //障害物に当たった際にfalseをいれる
        offlineflag = true;

        //重力や摩擦
        rb = this.GetComponent<Rigidbody>();
        // Animatorコンポーネントを取得する
        anim = this.GetComponent<Animator>();
        //パーティクルを取得する
        particle = this.GetComponent<ParticleSystem>();

        //コライダの設定
        //メッシュコライダーの設定
        meshboxCol = this.transform.GetChild(0).gameObject.GetComponents<BoxCollider>();
        //ボックスコライダーの設定
        boxCol = this.GetComponent<BoxCollider>();

        //ニックネームを表示(IDを抜き取る)
        NameText.text = PlayerPrefs.GetString("NickName").Substring(0, PlayerPrefs.GetString("NickName").LastIndexOf("("));

        //Transformをキャッシュする
        transformCache = transform;



    }

    // Update is called once per frame
    void Update()
    {

        //自分の画面の自キャラのみ操作できるようにする
        if (offlineflag == false)
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
        //ニックネームを表示
        //NameText.text = PlayerPrefs.GetString("NickName").Substring(0, PlayerPrefs.GetString("NickName").LastIndexOf("("));
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
            rb.AddForce(Vector3.up * jumpPower * trampolineJumpPower, ForceMode.VelocityChange);
            //かかっている重力加速のリセット
            this.rb.velocity = new Vector3(this.rb.velocity.x, 0, this.rb.velocity.z);
            //ジャンプアニメーション
            anim.SetBool("Jump", true);
            jumpFlag = false;
            trampolineJumpPower = 1.0f;
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
            for(int i=0; i<meshboxCol.Length; i++)
            {
                meshboxCol[i].enabled = false;
            }
            boxCol.enabled = true;

        }
        else
        {
            anim.SetBool("Sit", false);
            //コライダーの設定
            for (int i = 0; i < meshboxCol.Length; i++)
            {
                meshboxCol[i].enabled = true;
            }
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
            if (TeppenMenuShopList.JShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 11.0f;
            else if (TeppenMenuShopList.JShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 13.0f;
            else if (TeppenMenuShopList.JShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 15.0f;
            else { jumpPower = 9.0f; }
            //スピード
            if (isGround)
            {
                if (TeppenMenuShopList.DShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 9.5f;
                else if(TeppenMenuShopList.DShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 14.5f;
                else if (TeppenMenuShopList.DShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 19.5f;
                else { runSpeed = 4.5f; }
            }
            else
            {
                if (TeppenMenuShopList.AShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 9.5f;
                else if (TeppenMenuShopList.AShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 14.5f;
                else if (TeppenMenuShopList.AShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 19.5f;
                else { runSpeed = 4.5f; }
            }

        }
        //象
        else if (SelectCharacterUI.animalName == "Elephant")
        {
            //ジャンプ力
            //ジャンプ力
            if (TeppenMenuShopList.JShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 10.5f;
            else if (TeppenMenuShopList.JShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 12.5f;
            else if (TeppenMenuShopList.JShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 14.5f;
            else { jumpPower = 8.5f; }
            //スピード
            if (isGround)
            {
                if (TeppenMenuShopList.DShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 11.5f;
                else if (TeppenMenuShopList.DShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 16.5f;
                else if (TeppenMenuShopList.DShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 21.5f;
                else { runSpeed = 6.5f; }
            }
            else
            {
                if (TeppenMenuShopList.AShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 11.0f;
                else if (TeppenMenuShopList.AShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 16.0f;
                else if (TeppenMenuShopList.AShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 21.0f;
                else { runSpeed = 6.0f; }
            }
        }
        //犬
        else if (SelectCharacterUI.animalName == "Dog")
        {
            //ジャンプ力
            //ジャンプ力
            if (TeppenMenuShopList.JShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 15.0f;
            else if (TeppenMenuShopList.JShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 17.0f;
            else if (TeppenMenuShopList.JShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 19.0f;
            else { jumpPower = 13.0f; }
            //スピード
            if (isGround)
            {
                if (TeppenMenuShopList.DShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 17.0f;
                else if (TeppenMenuShopList.DShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 22.0f;
                else if (TeppenMenuShopList.DShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 27.0f;
                else { runSpeed = 12.0f; }
            }
            else
            {
                if (TeppenMenuShopList.AShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 12.0f;
                else if (TeppenMenuShopList.AShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 17.0f;
                else if (TeppenMenuShopList.AShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 22.0f;
                else { runSpeed = 7.0f; }
            }
        }
        //虎
        else if (SelectCharacterUI.animalName == "Tiger")
        {
            //ジャンプ力
            //ジャンプ力
            if (TeppenMenuShopList.JShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 14.5f;
            else if (TeppenMenuShopList.JShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 16.5f;
            else if (TeppenMenuShopList.JShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 18.5f;
            else { jumpPower = 12.5f; }
            //スピード
            if (isGround)
            {
                if (TeppenMenuShopList.DShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 20.0f;
                else if (TeppenMenuShopList.DShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 25.0f;
                else if (TeppenMenuShopList.DShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 30.0f;
                else { runSpeed = 15.0f; }
            }
            else
            {
                if (TeppenMenuShopList.AShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 13.0f;
                else if (TeppenMenuShopList.AShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 18.0f;
                else if (TeppenMenuShopList.AShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 23.0f;
                else { runSpeed = 8.0f; }
            }
        }
        //猫
        else if (SelectCharacterUI.animalName == "Cat")
        {
            //ジャンプ力
            //ジャンプ力
            if (TeppenMenuShopList.JShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 15.5f;
            else if (TeppenMenuShopList.JShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 17.5f;
            else if (TeppenMenuShopList.JShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 19.5f;
            else { jumpPower = 13.5f; }
            //スピード
            if (isGround)
            {
                if (TeppenMenuShopList.DShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 16.0f;
                else if (TeppenMenuShopList.DShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 21.0f;
                else if (TeppenMenuShopList.DShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 26.0f;
                else { runSpeed = 11.0f; }
            }
            else
            {
                if (TeppenMenuShopList.AShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 13.0f;
                else if (TeppenMenuShopList.AShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 18.0f;
                else if (TeppenMenuShopList.AShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 23.0f;
                else { runSpeed = 8.0f; }
            }
        }
        //ウサギ
        else if (SelectCharacterUI.animalName == "Rabbit")
        {
            //ジャンプ力
            //ジャンプ力
            if (TeppenMenuShopList.JShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 17.0f;
            else if (TeppenMenuShopList.JShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 19.0f;
            else if (TeppenMenuShopList.JShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") jumpPower = 21.0f;
            else { jumpPower = 15.0f; }
            //スピード
            if (isGround)
            {
                
                if (TeppenMenuShopList.DShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 10.0f;
                else if (TeppenMenuShopList.DShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 15.0f;
                else if (TeppenMenuShopList.DShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 20.0f;
                else { runSpeed = 5.0f; }
            }
            else
            {
                if (TeppenMenuShopList.AShoesUseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 11.0f;
                else if (TeppenMenuShopList.AShoes2UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 16.0f;
                else if (TeppenMenuShopList.AShoes3UseFlag == true && SceneManager.GetActiveScene().name == "TeppenBattleScene") runSpeed = 21.0f;
                else { runSpeed = 6.0f; }
            }
        }
    }


    //順位表示処理
    private void OnGUI()
    {
        if (offlineflag == false)
        {
            return;
        }
        //GUI.TextField(new Rect(400, 30, 150, 70), "isGround : " + isGround);

    }
}

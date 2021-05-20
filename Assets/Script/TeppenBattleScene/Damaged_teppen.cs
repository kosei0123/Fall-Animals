using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaged_teppen : MonoBehaviour
{
    //CharacterMainMove_offlineのpublic定数を使う
    CharacterMainMove_offline characterMainMove_offline;
    //BattleScene_teppenManagerのpublic定数を使う
    BattleScene_teppenManager battleScene_teppenManager;
    //EndDialog_teppenの関数等を使う
    EndDialog_teppen endDialog_teppen;

    //オフライン時はレコードtrueにしておく
    public static bool bestTimeRecode_Giraffe = false;
    public static bool bestTimeRecode_Elephant = false;
    public static bool bestTimeRecode_Dog = false;
    public static bool bestTimeRecode_Tiger = false;

    // Start is called before the first frame update
    void Start()
    {
        //CharacterMainMove_offlineのpublic定数を使う
        characterMainMove_offline = this.gameObject.GetComponent<CharacterMainMove_offline>();
        //BattleScene_teppenManagerのpublic定数を使う
        battleScene_teppenManager = GameObject.Find("BattleScene_offlineManager").GetComponent<BattleScene_teppenManager>();
        //EndDialog_teppenの関数等を使う
        endDialog_teppen = GameObject.Find("DialogCanvas").GetComponent<EndDialog_teppen>();

    }

    // Update is called once per frame
    void Update()
    {


        //一定距離落下後にオブジェクトの削除
        if (this.transform.position.y < -50)
        {
            Destroy(this.gameObject);
        }

        //衝突後回転する
        if (battleScene_teppenManager.battleFinishFlag == true)
        {
            this.transform.Rotate(new Vector3(0, 10.0f, 0));
        }

        //フラグがfalseであれば抜ける
        if (characterMainMove_offline.offlineflag == false)
        {
            return;
        }

        //一定距離落下後にゲーム終了
        if (this.transform.position.y < -15)
        {
            GameFinish();
        }

    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {
        //CharacterMainMove_offlineのpublic定数を使う
        characterMainMove_offline = this.gameObject.GetComponent<CharacterMainMove_offline>();

        //フラグがfalseであれば抜ける
        if (characterMainMove_offline.offlineflag == false)
        {
            return;
        }

        //岩や紙飛行機にあたり落下する
        if ((this.gameObject.layer == 10 || this.gameObject.layer == 13) && (other.gameObject.tag == "Obstacle_Rock" || other.gameObject.tag == "Obstacle_Airplane"))
        {
            GameFinish();
        }

        //コインに当たる
        if ((this.gameObject.layer == 10 || this.gameObject.layer == 13) && other.gameObject.tag == "Coin")
        {
            battleScene_teppenManager.getBattleCoin += 5;
        }
    }

    //ゲーム終了関数
    private void GameFinish()
    {
        //動きを止める
        characterMainMove_offline.offlineflag = false;
        characterMainMove_offline.rb.velocity = new Vector3(0, characterMainMove_offline.rb.velocity.y, 0);
        //パーティクルの発生
        Instantiate(Resources.Load("Offline/Particle"), new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);

        //バトル終了フラグをtrueにする
        battleScene_teppenManager.battleFinishFlag = true;
        //ぶつかったり落下した際のフラグをtrueに
        battleScene_teppenManager.damagedFlag = true;

        //アニメーションの設定
        characterMainMove_offline.anim.SetBool("Death", true);


        //プレイ数の追加
        PlayerPrefs.SetInt("PlayCount", PlayerPrefs.GetInt("PlayCount") + 1);


        //レイヤーを変更し、下に落ちていく
        this.gameObject.layer = 9;
        this.gameObject.transform.GetChild(0).gameObject.layer = 9;
        //上方向に力を加える
        characterMainMove_offline.jumpPower = 5.0f;
        characterMainMove_offline.rb.AddForce(Vector3.up * characterMainMove_offline.jumpPower, ForceMode.VelocityChange);

        //終了時のダイアログ表示
        endDialog_teppen.DialogPanelActive(PlayerPrefs.GetInt("TeppenFloor"));
    }

    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(400, 30, 150, 70), "Obstacle : " + test);

    }
}

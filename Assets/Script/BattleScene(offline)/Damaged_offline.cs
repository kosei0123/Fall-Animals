using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaged_offline : MonoBehaviour
{
    //CharacterMainMove_offlineのpublic定数を使う
    CharacterMainMove_offline characterMainMove_offline;
    //Pun2Scriptのpublic定数を使う
    BattleScene_offlineManager battleScene_offlineManager;
    //EndDialogの関数等を使う
    EndDialog_offline endDialog_offline;
    //Timer_offlineの関数等を使う
    Timer_offline timer_offline;

    // Start is called before the first frame update
    void Start()
    {
        //CharacterMainMove_offlineのpublic定数を使う
        characterMainMove_offline = this.gameObject.GetComponent<CharacterMainMove_offline>();
        //Pun2Scriptのpublic定数を使う
        battleScene_offlineManager = GameObject.Find("BattleScene_offlineManager").GetComponent<BattleScene_offlineManager>();
        //EndDialogの関数等を使う
        endDialog_offline = GameObject.Find("DialogCanvas").GetComponent<EndDialog_offline>();
        //Timer_offlineの関数等を使う
        timer_offline = GameObject.Find("TimerCanvas").GetComponent<Timer_offline>();

    }

    // Update is called once per frame
    void Update()
    {

        //一定距離落下後に消去
        if (this.transform.position.y < -50)
        {
            Destroy(this.gameObject);
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
        if ((this.gameObject.layer == 10 || this.gameObject.layer == 13) && other.gameObject.tag == "Obstacle")
        {
            //動きを止める
            characterMainMove_offline.offlineflag = false;
            characterMainMove_offline.rb.velocity = new Vector3(0, characterMainMove_offline.rb.velocity.y, 0);
            //パーティクルの発生
            Instantiate(Resources.Load("Offline/Particle"), new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);

            //バトル終了フラグをtrueにする
            battleScene_offlineManager.battleFinishFlag = true;

            //アニメーションの設定
            characterMainMove_offline.anim.SetBool("Death", true);

            //時間の確定と取得
            battleScene_offlineManager.timeRanking = (int)timer_offline.elapsedTime;
            //ベストスコアに反映
            if (PlayerPrefs.GetInt("BestTime_" + SelectCharacterUI_offline.animalName) < battleScene_offlineManager.timeRanking)
            {
                PlayerPrefs.SetInt("BestTime_" + SelectCharacterUI_offline.animalName, battleScene_offlineManager.timeRanking);
            }

            //レイヤーを変更し、下に落ちていく
            this.gameObject.layer = 9;
            this.gameObject.transform.GetChild(0).gameObject.layer = 9;
            //上方向に力を加える
            characterMainMove_offline.jumpPower = 5.0f;
            characterMainMove_offline.rb.AddForce(Vector3.up * characterMainMove_offline.jumpPower, ForceMode.VelocityChange);

            //終了時のダイアログ表示
            endDialog_offline.DialogPanelActive(battleScene_offlineManager.timeRanking);
        }

        //コインに当たる
        if ((this.gameObject.layer == 10 || this.gameObject.layer == 13) && other.gameObject.tag == "Coin")
        {
            battleScene_offlineManager.getBattleCoin += 5;
        }
    }

    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(400, 30, 150, 70), "Obstacle : " + test);

    }
}

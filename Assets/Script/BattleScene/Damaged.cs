﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Damaged : MonoBehaviour
{
    //CharacterMainMoveのpublic定数を使う
    CharacterMainMove characterMainMove;
    //Pun2Scriptのpublic定数を使う
    Pun2Script pun2Script;
    //EndDialogの関数等を使う
    EndDialog endDialog;
    //Timerのpublic定数を使う
    Timer timer;
    //CameraShakeのスクリプトの関数使用
    CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        //CharacterMainMoveのpublic定数を使う
        characterMainMove = this.gameObject.GetComponent<CharacterMainMove>();
        //Pun2Scriptのpublic定数を使う
        pun2Script = GameObject.Find("Pun2").GetComponent<Pun2Script>();
        //EndDialogの関数等を使う
        endDialog = GameObject.Find("DialogCanvas").GetComponent<EndDialog>();
        //Timerのpublic定数を使う
        timer = GameObject.Find("TimerCanvas").GetComponent<Timer>();
        //CameraShakeのスクリプトの関数使用
        cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        //4秒経っていなければ処理を行わない
        if (timer.elapsedTime <= 4.0f) return;

        //一定距離落下後にオブジェクトの削除
        if (this.transform.position.y < -50)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }

        //衝突後回転する、または、勝利時にアニメーションをする
        if (pun2Script.battleFinishFlag == true && pun2Script.battleRanking > 1)
        {
            this.transform.Rotate(new Vector3(0, 10.0f, 0));
        }
        else if (pun2Script.battleFinishFlag == true && pun2Script.battleRanking <= 1)
        {

        }

        //自分の操作キャラでなければ抜ける
        if (characterMainMove.onlineflag == false)
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
        //CharacterMainMoveのpublic定数を使う
        characterMainMove = this.gameObject.GetComponent<CharacterMainMove>();

        //自分の操作キャラでなければ抜ける
        if (characterMainMove.onlineflag == false)
        {
            return;
        }

        //4秒経っていなければ処理を行わない
        if (timer.elapsedTime <= 4.0f) return;

        //岩や紙飛行機にあたり落下する
        if ((this.gameObject.layer == 10 || this.gameObject.layer == 13) && (other.gameObject.tag == "Obstacle_Rock" || other.gameObject.tag == "Obstacle_Airplane"))
        {
            GameFinish();
        }

        //コインに当たる
        if ((this.gameObject.layer == 10 || this.gameObject.layer == 13) && other.gameObject.tag == "Coin")
        {
            pun2Script.getBattleCoin += 5;
        }
    }

    //ゲーム終了関数
    private void GameFinish()
    {
        //動きを止める
        characterMainMove.onlineflag = false;
        characterMainMove.rb.velocity = new Vector3(0, characterMainMove.rb.velocity.y, 0);
        //パーティクルの発生
        PhotonNetwork.Instantiate("Particle", new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity, 0);
        //画面を揺らす
        cameraShake.Shake(0.25f, 1.0f);

        //バトル終了フラグをtrueにする
        pun2Script.battleFinishFlag = true;

        //アニメーションの設定
        characterMainMove.anim.SetBool("Death", true);

        //プレイ数の追加
        PlayerPrefs.SetInt("PlayCount", PlayerPrefs.GetInt("PlayCount") + 1);

        //順位の確定と取得
        pun2Script.battleRanking = (int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"];
        //ルーム内残り人数を減らす
        var n = PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] is int value ? value : 0;
        PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] = n - 1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

        //レイヤーを変更し、下に落ちていく
        this.gameObject.layer = 9;
        this.gameObject.transform.GetChild(0).gameObject.layer = 9;
        //上方向に力を加える
        characterMainMove.jumpPower = 5.0f;
        characterMainMove.rb.AddForce(Vector3.up * characterMainMove.jumpPower, ForceMode.VelocityChange);

        //終了時のダイアログ表示
        endDialog.DialogPanelActive(pun2Script.battleRanking);
    }

    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(400, 30, 150, 70), "Obstacle : " + test);

    }
}

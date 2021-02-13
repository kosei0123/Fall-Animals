﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Damaged : MonoBehaviour
{
    //CharacterMainMoveのpublic定数を使う
    public GameObject target;
    CharacterMainMove characterMainMove;
    //Pun2Scriptのpublic定数を使う
    Pun2Script pun2Script;
    //EndDialogの関数等を使う
    EndDialog endDialog;

    // Start is called before the first frame update
    void Start()
    {
        //CharacterMainMoveのpublic定数を使う
        characterMainMove = target.GetComponent<CharacterMainMove>();
        //Pun2Scriptのpublic定数を使う
        pun2Script = GameObject.Find("Pun2").GetComponent<Pun2Script>();
        //EndDialogの関数等を使う
        endDialog = GameObject.Find("DialogCanvas").GetComponent<EndDialog>();
    }

    // Update is called once per frame
    void Update()
    {
        //一定距離落下後に消去
        if (characterMainMove.transformCache.position.y < -50)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }


        //自分の操作キャラでなければ抜ける
        if (characterMainMove.onlineflag == false)
        {
            return;
        }
    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {

        //自分の操作キャラでなければ抜ける
        if (characterMainMove.onlineflag == false)
        {
            return;
        }


        //岩にあたり落下する
        if (this.gameObject.layer == 10 && other.gameObject.tag == "Obstacle")
        {
            //動きを止める
            characterMainMove.onlineflag = false;
            characterMainMove.rb.velocity = new Vector3(0, characterMainMove.rb.velocity.y, 0);

            //アニメーションの設定
            characterMainMove.anim.SetBool("Death", true);

            //順位の確定と取得
            pun2Script.battleRanking = (int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"];
            //ルーム内残り人数を減らす
            var n = PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] is int value ? value : 0;
            PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] = n - 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

            //レイヤーを変更し、下に落ちていく
            this.gameObject.layer = 9;
            //上方向に力を加える
            characterMainMove.jumpPower = 5.0f;
            characterMainMove.rb.AddForce(Vector3.up * characterMainMove.jumpPower, ForceMode.VelocityChange);

            //終了時のダイアログ表示
            endDialog.DialogPanelActive(pun2Script.battleRanking);
        }
    }

    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(400, 30, 150, 70), "Obstacle : " + test);

    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Timer : MonoBehaviourPunCallbacks
{
    //残り時刻設定用テキストオブジェクト
    [SerializeField]
    private Text TimerText;

    //Battleの時間を取得する
    [HideInInspector]
    public float battleTime;

    //経過時間を取得する
    [HideInInspector]
    public float elapsedTime = 0;

    //時間無制限フラグ
    [HideInInspector]
    public bool mugenFlag;

    //メッセージの送信に使用される
    new PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        //時間を設定する
        battleTime = 60.0f;
        //無制限フラグをfalseにしておく
        mugenFlag = false;

        //メッセージの送信に使用される
        photonView = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {
        //経過時間を増やしていく
        elapsedTime += Time.deltaTime;

        //今回のタイムをランダムに決める
        //if (elapsedTime >= 2.0f && elapsedTime < 3.0f && PhotonNetwork.IsMasterClient)
        //{
        //    //ランダム値取得(1 ~ 4)
        //    //現在：60秒 or 無限
        //    int randomBattleTime = Random.Range(1, 5);

        //    switch (randomBattleTime)
        //    {
        //        case 1:
        //            battleTime = 15.0f;
        //            mugenFlag = false;
        //            break;
        //        case 2:
        //            battleTime = 30.0f;
        //            mugenFlag = false;
        //            break;
        //        case 3:
        //            battleTime = 60.0f;
        //            mugenFlag = false;
        //            break;
        //        case 4:
        //            mugenFlag = true;
        //            break;
        //        default:
        //            break;
        //    }

        //    //バトル時間を同期する
        //    PhotonView photonView = PhotonView.Get(this);
        //    photonView.RPC("BattleTimeValue", RpcTarget.All, battleTime, mugenFlag);
        //}


        //一定秒経過後に時間を減らしていく(無制限を除く)
        if (elapsedTime >= 4.0f && battleTime >= 0 && mugenFlag == false)
        {
            //5秒ごとに時間の同期を行う
            if(battleTime % 5 <= 1 && PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("BattleTimeValue", RpcTarget.All, battleTime);
                //photonView.RPC("BattleTimeValue", RpcTarget.All, battleTime, mugenFlag);
            }

            battleTime -= Time.deltaTime;
        }

        //残り時間の表示
        if (elapsedTime >= 3.0f && mugenFlag == false)
        {
            TimerText.text = ((int)battleTime).ToString("D2");
        }
        else if(elapsedTime >= 3.0f && mugenFlag == true)
        {
            TimerText.text = "∞";
        }
        
    }

    [PunRPC]
    //バトル時間を共有する
    private void BattleTimeValue(float value)
    {
        //バトル時間を設定する
        battleTime = value;
        //無制限かを同期する
        //mugenFlag = mugen;
    }
}

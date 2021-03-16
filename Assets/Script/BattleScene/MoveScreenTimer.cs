using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MoveScreenTimer : MonoBehaviour
{
    //Pun2Scriptのpublic定数を使う
    Pun2Script pun2Script;

    //TimerText
    [SerializeField]
    private GameObject TimerText;

    //MoveScreenTimerText
    [SerializeField]
    private GameObject MoveScreenTimerTextGameObject;

    //画面遷移までの時間を表示
    [SerializeField]
    private Text MoveScreenTimerText;

    //一定時間操作がなかった時に接続を切る用
    private float disconnectTime;
    public bool moveScreenFlag;

    // Start is called before the first frame update
    void Start()
    {
        //Pun2Scriptのpublic定数を使う
        pun2Script = GameObject.Find("Pun2").GetComponent<Pun2Script>();

        //時間の設定(60秒)
        disconnectTime = 60;
        //シーン移動可能
        moveScreenFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        //バトルが終了していないのであれば、returnを返す
        if(pun2Script.battleFinishFlag == false)
        {
            return;
        }

        //TimerTextの非表示
        TimerText.SetActive(false);
        //MoveScreenTimerTextの表示
        MoveScreenTimerTextGameObject.SetActive(true);

        //一定時間操作がなかった時に退出
        if (disconnectTime > 0 && moveScreenFlag == true)
        {
            disconnectTime -= Time.deltaTime;
        }
        else if(disconnectTime <= 0)
        {
            //マスタークライアントの切断をルーム全体で検知する
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] = true;
                PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
            }

            //画面遷移等(0.5秒後)
            Invoke("MoveScreen_SceneMove", 0.5f);

        }

        //時間の表示
        if(disconnectTime <= 10.0f)
        {
            MoveScreenTimerText.text = ((int)disconnectTime).ToString("D2");
        }
    }

    //再接続時
    private void MoveScreen_SceneMove()
    {
        //画面遷移
        SceneManager.LoadScene("Menu");
    }
}

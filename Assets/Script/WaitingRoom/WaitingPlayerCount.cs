using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class WaitingPlayerCount : MonoBehaviour
{
    //待機人数の表示
    [SerializeField]
    private Text WaitingPlayerCountText;

    // Start is called before the first frame update
    void Start()
    {
        //同じルーム内のWaitingRoomにいるプレイヤーの数を数える
        var n = PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] is int value ? value : 0;
        PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] = n + 1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
    }

    // Update is called once per frame
    void Update()
    {
        //待機人数の表示
        WaitingPlayerCountText.text = "ルーム内待機中プレイヤー : " + PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"]  + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

        //MaxPlayerに達した段階で画面遷移(仮)
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["WaitingRoomPlayerCount"] == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            //入室できないようにする
            LobbyManager.UpdateRoomOptions(false);
            //画面遷移
            SceneManager.LoadScene("BattleScene");
        }
    }
}

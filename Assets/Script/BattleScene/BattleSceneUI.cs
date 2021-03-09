using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BattleSceneUI : MonoBehaviour
{
    //Timerのpublic定数を使う
    //Timer timer;
    //Pun2Scriptのpublic定数を使う
    Pun2Script pun2Script;

    //残りプレイヤー表示
    [SerializeField]
    private Text RemainingPlayerCountText;

    // Start is called before the first frame update
    void Start()
    {
        //Timerのpublic定数を使う
        //timer = GameObject.Find("TimerCanvas").GetComponent<Timer>();
        //Pun2Scriptのpublic定数を使う
        pun2Script = GameObject.Find("Pun2").GetComponent<Pun2Script>();
    }

    // Update is called once per frame
    void Update()
    {
        //勝者が決まるまでの間
        if (pun2Script.battleFinishFlag == false)
        {
            //一定時間後に表示する
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] <= 0)
            {
                RemainingPlayerCountText.text = "のこり　確認中...";
            }
            else
            {
                RemainingPlayerCountText.text = "のこり　" + PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"];
            }
        }
        else
        {
            RemainingPlayerCountText.text = "";
        }
        
        
    }
}

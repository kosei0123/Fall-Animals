using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BattleSceneUI : MonoBehaviour
{
    //Timerのpublic定数を使う
    Timer timer;

    //残りプレイヤー表示
    [SerializeField]
    private Text RemainingPlayerCountText;

    // Start is called before the first frame update
    void Start()
    {
        //Timerのpublic定数を使う
        timer = GameObject.Find("TimerCanvas").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        //一定時間後に表示する
        if (timer.elapsedTime >= 2.0f && timer.elapsedTime < 3.0f)
        {
            RemainingPlayerCountText.text = "のこり　確認中...";
        }
        else if (timer.elapsedTime >= 3.0f)
        {
            RemainingPlayerCountText.text = "のこり　" + PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"];
        }
        
    }
}

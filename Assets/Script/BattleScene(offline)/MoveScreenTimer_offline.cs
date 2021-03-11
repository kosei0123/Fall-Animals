using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveScreenTimer_offline : MonoBehaviour
{
    //Pun2Scriptのpublic定数を使う
    BattleScene_offlineManager battleScene_offlineManager;

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
    private float moveScreenTime;
    public bool moveScreenFlag;

    // Start is called before the first frame update
    void Start()
    {
        //Pun2Scriptのpublic定数を使う
        battleScene_offlineManager = GameObject.Find("BattleScene_offlineManager").GetComponent<BattleScene_offlineManager>();

        //時間の設定(20秒)
        moveScreenTime = 20;
        //シーン移動可能
        moveScreenFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        //バトルが終了していないのであれば、returnを返す
        if (battleScene_offlineManager.battleFinishFlag == false)
        {
            return;
        }

        //TimerTextの非表示
        TimerText.SetActive(false);
        //MoveScreenTimerTextの表示
        MoveScreenTimerTextGameObject.SetActive(true);

        //一定時間操作がなかった時に退出
        if (moveScreenTime > 0 && moveScreenFlag == true)
        {
            moveScreenTime -= Time.deltaTime;
        }
        else if(moveScreenTime <= 0)
        {
            //画面遷移
            SceneManager.LoadScene("Menu");
        }

        //時間の表示
        if (moveScreenTime <= 5.0f)
        {
            MoveScreenTimerText.text = ((int)moveScreenTime).ToString("D2");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_offline : MonoBehaviour
{
    //残り時刻設定用テキストオブジェクト
    [SerializeField]
    private Text TimerText;

    //経過時間を取得する
    [HideInInspector]
    public float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        //初期値は0
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //経過時間を増やしていく
        elapsedTime += Time.deltaTime;

        //残り時間の表示
        if (elapsedTime >= 3.0f)
        {
            TimerText.text = ((int)elapsedTime / 60).ToString("D2") + ":" + ((int)elapsedTime % 60).ToString("D2");
        }
    }
}

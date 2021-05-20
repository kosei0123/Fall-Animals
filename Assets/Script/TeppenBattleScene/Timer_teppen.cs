using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_teppen : MonoBehaviour
{
    //残り時刻設定用テキストオブジェクト
    [SerializeField]
    private Text TimerText;

    //経過時間を取得する
    [HideInInspector]
    public float elapsedTime;
    //残り時間を取得する
    [HideInInspector]
    public float remainingTime;

    // Start is called before the first frame update
    void Start()
    {
        //初期値は0
        elapsedTime = 0;
        //残り時間初期値
        remainingTime = 5.0f + (float)PlayerPrefs.GetInt("TeppenFloor");

    }

    // Update is called once per frame
    void Update()
    {
        //経過時間を増やしていく
        elapsedTime += Time.deltaTime;
        //残り時間を減らしていく
        if(remainingTime > 0) remainingTime -= Time.deltaTime;


        //残り時間の表示
        //if (elapsedTime >= 3.0f)
        //{
        //    TimerText.text = ((int)elapsedTime / 60).ToString("D2") + ":" + ((int)elapsedTime % 60).ToString("D2");
        //}

        //残り時間の表示
        TimerText.text = ((int)remainingTime / 60).ToString("D2") + ":" + ((int)remainingTime % 60).ToString("D2");
    }
}

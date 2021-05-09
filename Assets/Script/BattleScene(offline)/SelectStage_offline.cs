using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStage_offline : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

    //ステージ1
    [SerializeField]
    private GameObject Stage1;
    //ステージ2
    [SerializeField]
    private GameObject Stage2;
    //ステージ3
    [SerializeField]
    private GameObject Stage3;
    //ステージ4
    [SerializeField]
    private GameObject Stage4;
    //ステージ5
    [SerializeField]
    private GameObject Stage5;
    //ステージ6
    [SerializeField]
    private GameObject Stage6;

    //背景1
    [SerializeField]
    private GameObject Background1;
    //背景2
    [SerializeField]
    private GameObject Background2;
    //背景3
    [SerializeField]
    private GameObject Background3;

    //ステージリストの初期化
    private List<int> stageList = new List<int>();
    //バックグラウンド初期化
    private List<int> backgroundList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //選択可能なステージリスト取得
        StageList();
        //選択可能なバックグラウンド取得
        BackgroundList();

        int randomStage = UnityEngine.Random.Range(0, stageList.Count);
        int randomBackground = UnityEngine.Random.Range(0, backgroundList.Count);

        //ステージ
        switch (stageList[randomStage])
        {
            case 1:
                Stage1.SetActive(true);
                break;
            case 2:
                Stage2.SetActive(true);
                break;
            case 3:
                Stage3.SetActive(true);
                break;
            case 4:
                Stage4.SetActive(true);
                break;
            case 5:
                Stage5.SetActive(true);
                break;
            case 6:
                Stage6.SetActive(true);
                break;
            default:
                break;
        }

        //背景
        switch (backgroundList[randomBackground])
        {
            case 1:
                Background1.SetActive(true);
                soundManager.BGMManager("BGM_Battle");
                break;
            case 2:
                Background2.SetActive(true);
                soundManager.BGMManager("BGM_Battle_Cave");
                break;
            case 3:
                Background3.SetActive(true);
                soundManager.BGMManager("BGM_Battle_NightStreet");
                break;
            default:
                break;
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ステージ
    private void StageList()
    {
        //ステージ1~3
        stageList.Add(1);
        stageList.Add(2);
        stageList.Add(3);
        //追加ステージ
        if(PlayerPrefs.GetInt("Unlock_Stage4") == 1) stageList.Add(4);
        if(PlayerPrefs.GetInt("Unlock_Stage5") == 1) stageList.Add(5);
        if(PlayerPrefs.GetInt("Unlock_Stage6") == 1) stageList.Add(6);
    }

    //背景
    private void BackgroundList()
    {
        DateTime now = DateTime.Now;

        backgroundList.Add(1);
        backgroundList.Add(2);
        if((now.Hour >= 0 && now.Hour <=6) || (now.Hour >= 18 && now.Hour <= 24)) backgroundList.Add(3);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //パーティクル
    //背景2のパーティクル
    private GameObject CaveParticle;
    //生成サイクル
    private float Background2_ParticleTime = 5.0f;

    //ステージリストの初期化
    private List<int> stageList = new List<int>();
    //バックグラウンド初期化
    private List<int> backgroundList = new List<int>();

    //ランダム値取得用
    private int randomStage = 0;
    private int randomBackground = 0;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //選択可能なステージリスト取得
        StageList();
        //選択可能なバックグラウンド取得
        BackgroundList();

        randomStage = UnityEngine.Random.Range(0, stageList.Count);
        randomBackground = UnityEngine.Random.Range(0, backgroundList.Count);

        //テッペンモード場合
        if (SceneManager.GetActiveScene().name == "TeppenBattleScene") StageAssignmentNumber();

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
        //背景2パーティクルの発生
        if(Background2.activeSelf == true && Background2_ParticleTime <= 0)
        {
            //パーティクルの発生
            CaveParticle = (GameObject)Instantiate(Resources.Load("Offline/CaveParticle"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            CaveParticle.transform.parent = Background2.transform;
            CaveParticle.transform.localPosition = new Vector3(UnityEngine.Random.Range(-0.2f,0.0f), UnityEngine.Random.Range(-0.6f, -0.4f), UnityEngine.Random.Range(-0.1f, 0.1f));
            CaveParticle.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            CaveParticle.transform.localScale = new Vector3(1,1,1);
            Background2_ParticleTime = UnityEngine.Random.Range(10,30);
        }

        //背景2のパーティクル生成時間を減らす
        Background2_ParticleTime -= Time.deltaTime;
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

        if (now.Hour > 6 && now.Hour < 18) backgroundList.Add(1);
        backgroundList.Add(2);
        if ((now.Hour >= 0 && now.Hour <= 6) || (now.Hour >= 18 && now.Hour <= 24)) backgroundList.Add(3);
    }

    //テッペンモードの場合に呼び出される関数
    private void StageAssignmentNumber()
    {
        if (TeppenShopUI.stageAssignmentNumber == 1) randomStage = 0;
        if (TeppenShopUI.stageAssignmentNumber == 2) randomStage = 1;
        if (TeppenShopUI.stageAssignmentNumber == 3) randomStage = 2;
        if (TeppenShopUI.stageAssignmentNumber == 4) randomStage = 3;
        if (TeppenShopUI.stageAssignmentNumber == 5) randomStage = 4;
        if (TeppenShopUI.stageAssignmentNumber == 6) randomStage = 5;
    }
}

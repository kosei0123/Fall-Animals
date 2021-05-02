using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStage_offline : MonoBehaviour
{
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

    //ステージリストの初期化
    private List<int> stageList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //選択可能なステージリスト取得
        StageList();

        int randomStage = Random.Range(0, stageList.Count);

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
    }

    // Update is called once per frame
    void Update()
    {

    }

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
}

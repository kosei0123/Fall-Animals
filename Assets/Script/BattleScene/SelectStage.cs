using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SelectStage : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //ステージ
        switch (PhotonNetwork.CurrentRoom.CustomProperties["DefinedStage"])
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
        switch (PhotonNetwork.CurrentRoom.CustomProperties["DefinedBackground"])
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
        if (Background2.activeSelf == true && Background2_ParticleTime <= 0)
        {
            //パーティクルの発生
            CaveParticle = PhotonNetwork.Instantiate("CaveParticle", new Vector3(0.0f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            CaveParticle.transform.parent = Background2.transform;
            CaveParticle.transform.localPosition = new Vector3(Random.Range(-0.2f, 0.0f), Random.Range(-0.6f, -0.4f), Random.Range(-0.1f, 0.1f));
            CaveParticle.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            CaveParticle.transform.localScale = new Vector3(1, 1, 1);
            Background2_ParticleTime = Random.Range(10, 30);
        }

        //背景2のパーティクル生成時間を減らす
        Background2_ParticleTime -= Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkins : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

    //None
    //フレーム
    [SerializeField]
    private GameObject NoneFlame;
    private bool NoneFlameFlag = false;

    //Candy
    //フレーム
    [SerializeField]
    private GameObject CandyFlame;
    private bool CandyFlameFlag = false;
    //パネル
    [SerializeField]
    private GameObject CandyPanel;

    //スキンの名前取得
    public static string skinsName;
    //スキンの名前取得(仮)
    [HideInInspector]
    public string skinsName_Temporary;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //スキンが既に指定されている場合は、仮にいれる
        skinsName_Temporary = skinsName;

        //アンロックされたスキンを表示する
        CheckSkinsUnlock();
    }

    // Update is called once per frame
    void Update()
    {
        //None
        NoneFlameFlag = (skinsName_Temporary == null) ? true : false;
        NoneFlame.SetActive(NoneFlameFlag);

        //Candy
        CandyFlameFlag = (skinsName_Temporary == "Candy") ? true : false;
        CandyFlame.SetActive(CandyFlameFlag);
    }

    //アンロックされたスキンを表示する
    private void CheckSkinsUnlock()
    {
        //キリンパネル
        if (PlayerPrefs.GetInt("Unlock_Candy") == 1)
        {
            CandyPanel.SetActive(true);
        }
    }

    //None
    public void OnClick_NoneButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        skinsName_Temporary = null;
    }

    //Candy
    public void OnClick_CandyButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        skinsName_Temporary = "Candy";
    }
}

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
    private bool CandyPanelFlag = false;
    //Crown
    //フレーム
    [SerializeField]
    private GameObject CrownFlame;
    private bool CrownFlameFlag = false;
    //パネル
    [SerializeField]
    private GameObject CrownPanel;
    private bool CrownPanelFlag = false;
    //Cloud
    //フレーム
    [SerializeField]
    private GameObject CloudFlame;
    private bool CloudFlameFlag = false;
    //パネル
    [SerializeField]
    private GameObject CloudPanel;
    private bool CloudPanelFlag = false;
    //Mappin
    //フレーム
    [SerializeField]
    private GameObject MappinFlame;
    private bool MappinFlameFlag = false;
    //パネル
    [SerializeField]
    private GameObject MappinPanel;
    private bool MappinPanelFlag = false;
    //Crystal
    //フレーム
    [SerializeField]
    private GameObject CrystalFlame;
    private bool CrystalFlameFlag = false;
    //パネル
    [SerializeField]
    private GameObject CrystalPanel;
    private bool CrystalPanelFlag = false;

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
        //Crown
        CrownFlameFlag = (skinsName_Temporary == "Crown") ? true : false;
        CrownFlame.SetActive(CrownFlameFlag);
        //Cloud
        CloudFlameFlag = (skinsName_Temporary == "Cloud") ? true : false;
        CloudFlame.SetActive(CloudFlameFlag);
        //Mappin
        MappinFlameFlag = (skinsName_Temporary == "Mappin") ? true : false;
        MappinFlame.SetActive(MappinFlameFlag);
        //Crystal
        CrystalFlameFlag = (skinsName_Temporary == "Crystal") ? true : false;
        CrystalFlame.SetActive(CrystalFlameFlag);
    }

    //アンロックされたスキンを表示する
    private void CheckSkinsUnlock()
    {
        //Candy
        CandyPanelFlag = (PlayerPrefs.GetInt("Unlock_Candy") == 1) ? true : false;
        CandyPanel.SetActive(CandyPanelFlag);
        //Crown
        CrownPanelFlag = (PlayerPrefs.GetInt("Unlock_Crown") == 1) ? true : false;
        CrownPanel.SetActive(CrownPanelFlag);
        //Cloud
        CloudPanelFlag = (PlayerPrefs.GetInt("Unlock_Cloud") == 1) ? true : false;
        CloudPanel.SetActive(CloudPanelFlag);
        //Mappin
        MappinPanelFlag = (PlayerPrefs.GetInt("Unlock_Mappin") == 1) ? true : false;
        MappinPanel.SetActive(MappinPanelFlag);
        //Crystal
        CrystalPanelFlag = (PlayerPrefs.GetInt("Unlock_Crystal") == 1) ? true : false;
        CrystalPanel.SetActive(CrystalPanelFlag);
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

    //Crown
    public void OnClick_CrownButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        skinsName_Temporary = "Crown";
    }

    //Cloud
    public void OnClick_CloudButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        skinsName_Temporary = "Cloud";
    }

    //Mappin
    public void OnClick_MappinButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        skinsName_Temporary = "Mappin";
    }

    //Crystal
    public void OnClick_CrystalButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        skinsName_Temporary = "Crystal";
    }
}

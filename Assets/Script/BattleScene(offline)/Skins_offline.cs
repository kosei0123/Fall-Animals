﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins_offline : MonoBehaviour
{
    //CharacterMainMove_offlineのpublic定数を使う
    CharacterMainMove_offline characterMainMove_Offline;

    //Candy
    [SerializeField]
    private GameObject Candy;
    private bool CandyFlag = false;
    //Crown
    [SerializeField]
    private GameObject Crown;
    private bool CrownFlag = false;

    // Start is called before the first frame update
    void Start()
    {

        //CharacterMainMove_offlineのpublic定数を使う
        characterMainMove_Offline = this.gameObject.GetComponent<CharacterMainMove_offline>();

        //None
        if (SelectSkins.skinsName == null)
        {
            Candy.SetActive(false);
            return;
        }

        //Candy
        CandyFlag = (SelectSkins.skinsName == "Candy") ? true : false;
        Candy.SetActive(CandyFlag);
        //Crown
        CrownFlag = (SelectSkins.skinsName == "Crown") ? true : false;
        Crown.SetActive(CrownFlag);

    }

    // Update is called once per frame
    void Update()
    {
        

        //自分の画面の自キャラのみ操作できるようにする
        if (characterMainMove_Offline.offlineflag == false)
        {
            return;
        }

        //None
        if (SelectSkins.skinsName == null)
        {
            return;
        }

        //向きの反転
        //Candy
        //if (((characterMainMove_Offline.transformCache.localScale.z < 0 && characterMainMove_Offline.moveDirection > 0.1) ||
        //    (characterMainMove_Offline.transformCache.localScale.z > 0 && characterMainMove_Offline.moveDirection < -0.1)) && Candy.activeSelf == true)
        //{
        //    Candy.transform.localScale = new Vector3(Candy.transform.localScale.x, Candy.transform.localScale.y, Candy.transform.localScale.z * (-1));
        //}
        //if (((characterMainMove_Offline.transformCache.localScale.z < 0 && characterMainMove_Offline.moveDirection > 0.1) ||
        //    (characterMainMove_Offline.transformCache.localScale.z > 0 && characterMainMove_Offline.moveDirection < -0.1)) && Candy_Sit.activeSelf == true)
        //{
        //    Candy_Sit.transform.localScale = new Vector3(Candy_Sit.transform.localScale.x, Candy_Sit.transform.localScale.y, Candy_Sit.transform.localScale.z * (-1));
        //}

    }
}
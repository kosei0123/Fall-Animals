using System.Collections;
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
    //Cloud
    [SerializeField]
    private GameObject Cloud;
    private bool CloudFlag = false;

    // Start is called before the first frame update
    void Start()
    {

        //CharacterMainMove_offlineのpublic定数を使う
        characterMainMove_Offline = this.gameObject.GetComponent<CharacterMainMove_offline>();

        //None
        if (SelectSkins.skinsName == null)
        {
            Candy.SetActive(false);
            Crown.SetActive(false);
            Cloud.SetActive(false);
            return;
        }

        //Candy
        CandyFlag = (SelectSkins.skinsName == "Candy") ? true : false;
        Candy.SetActive(CandyFlag);
        //Crown
        CrownFlag = (SelectSkins.skinsName == "Crown") ? true : false;
        Crown.SetActive(CrownFlag);
        //Cloud
        CloudFlag = (SelectSkins.skinsName == "Cloud") ? true : false;
        Cloud.SetActive(CloudFlag);

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

    }
}

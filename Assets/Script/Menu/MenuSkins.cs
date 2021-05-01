using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSkins : MonoBehaviour
{
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
    //Mappin
    [SerializeField]
    private GameObject Mappin;
    private bool MappinFlag = false;
    //Crystal
    [SerializeField]
    private GameObject Crystal;
    private bool CrystalFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //None
        if (SelectSkins.skinsName == null)
        {
            Candy.SetActive(false);
            Crown.SetActive(false);
            Cloud.SetActive(false);
            Mappin.SetActive(false);
            Crystal.SetActive(false);
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
        //Mappin
        MappinFlag = (SelectSkins.skinsName == "Mappin") ? true : false;
        Mappin.SetActive(MappinFlag);
        //Crystal
        CrystalFlag = (SelectSkins.skinsName == "Crystal") ? true : false;
        Crystal.SetActive(CrystalFlag);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

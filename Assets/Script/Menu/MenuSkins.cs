using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSkins : MonoBehaviour
{
    //Candy
    [SerializeField]
    private GameObject Candy;
    private bool CandyFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //None
        if (SelectSkins.skinsName == null)
        {
            Candy.SetActive(false);
            return;
        }

        //Candy
        CandyFlag = (SelectSkins.skinsName == "Candy") ? true : false;
        Candy.SetActive(CandyFlag);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

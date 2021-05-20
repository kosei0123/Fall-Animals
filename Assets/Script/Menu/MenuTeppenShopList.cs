using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTeppenShopList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Dシューズ
        if (!PlayerPrefs.HasKey("DShoesFlag")) PlayerPrefs.SetInt("DShoesFlag", 0);
        if (!PlayerPrefs.HasKey("DShoes2Flag")) PlayerPrefs.SetInt("DShoes2Flag", 0);
        if (!PlayerPrefs.HasKey("DShoes3Flag")) PlayerPrefs.SetInt("DShoes3Flag", 0);

        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

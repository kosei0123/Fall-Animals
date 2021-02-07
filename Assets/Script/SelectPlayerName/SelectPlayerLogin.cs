using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SelectPlayerLogin : MonoBehaviour
{
    //ニックネーム取得用
    private string nickname;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick_SelectNameButton()
    {
        nickname = GameObject.Find("InputField").GetComponent<InputField>().text;

        //ニックネームが空の場合は適当なものを代入する
        if (string.IsNullOrEmpty(nickname))
        {
            nickname = "Player(" + Random.Range(1, 999) + ")";
        }

        PhotonNetwork.LocalPlayer.NickName = nickname;

        //画面遷移
        SceneManager.LoadScene("BattleScene");
    }
}

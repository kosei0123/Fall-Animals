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

    //表示処理
    private void OnGUI()
    {


        GUI.TextField(new Rect(150, 30, 150, 70), "Room名 : " + PhotonNetwork.CurrentRoom.Name);
        //GUI.TextField(new Rect(400, 30, 150, 70), "HP(2P) : " + samplePun2Script.GetPlayer2Information().CustomProperties["HP"].ToString());

        //GUI.TextField(new Rect(650, 30, 150, 70), "HP(3P) : ".ToString());
        //GUI.TextField(new Rect(900, 30, 150, 70), "HP(4P) : ".ToString());
    }
}

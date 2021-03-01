using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class UserAuth : MonoBehaviour
{
    //MenuUIスクリプトの関数使用
    MenuUI menuUI;

    /// <summary>
    /// mobile backendに接続してログイン
    /// </summary>
    public void login(string id)
    {
        NCMBUser.LogInAsync(id, "0", (NCMBException e) =>
        {
            //接続成功したら
            if(e == null)
            {

            }
        });
    }

    /// <summary>
    /// mobile backendに接続して新規登録
    /// </summary>
    public void signUp(string id)
    {
        NCMBUser user = new NCMBUser();
        user.UserName = id;
        user.Password = "0";
        user.SignUpAsync((NCMBException e) =>
        {
            if (e == null)
            {

            }
        });
    }

    /// <summary>
    /// mobile backendに接続してログアウト
    /// </summary>
    public void logOut()
    {
        NCMBUser.LogOutAsync((NCMBException e) =>
        {
            if (e == null)
            {

            }
        });
    }

    /// <summary>
    /// mobile backendに接続して名前とスコアを初期登録する
    /// </summary>
    public void firstSetNameScore()
    {
        NCMBObject obj = new NCMBObject("HighScore");
        obj["Name"] = PlayerPrefs.GetString("NickName");
        obj["Score"] = 0;
        obj.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                //エラー処理
            }
            else
            {
                //成功時の処理
            }
        });
    }

    /// <summary>
    /// サーバにハイスコアを保存
    /// </summary>
    public void save()
    {
        //データスコアの「HighScore」クラスから、Nameをキーにして検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
        query.WhereEqualTo("Name", PlayerPrefs.GetString("NickName"));
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら
            if(e == null)
            {
                objList[0]["Score"] = PlayerPrefs.GetInt("WinCount");
                objList[0].SaveAsync();
            }
        });
    }

    /// <summary>
    /// mobile backendに接続してtop5取得
    /// </summary>
    public void TopRankers()
    {
        //MenuUIスクリプトの関数使用
        menuUI = GameObject.Find("Canvas").GetComponent<MenuUI>();

        //ランキングの配列
        string[] topRankingName = new string[50];
        string[] topRankingNumber = new string[50];

        //データスコアの「HighScore」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
        query.OrderByDescending("Score");
        query.Limit = 30;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null)
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    //ランキング名前の表示
                    topRankingName[i] = (i + 1).ToString("") + "位 : " + objList[i]["Name"] + "\n";
                    menuUI.WinCountRankingNameText.text += topRankingName[i];
                    //ランキング番号の表示
                    topRankingNumber[i] += objList[i]["Score"] + "\n";
                    menuUI.WinCountRankingNumberText.text += topRankingNumber[i];

                }
            }
            
        });
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

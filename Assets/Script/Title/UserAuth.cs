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
    /// mobile backendに接続してベストタイムを初期登録する
    /// </summary>
    public void firstSetBestTime(bool deleteFlag)
    {
        //データスコアの「HighScore」クラスから、Nameをキーにして検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
        query.WhereEqualTo("Name", PlayerPrefs.GetString("NickName"));
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null)
            {
                //キリン
                if (!PlayerPrefs.HasKey("BestTime_Giraffe") || deleteFlag == true)
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Giraffe", 0);
                    PlayerPrefs.SetInt("bestTimeRecode_Giraffe", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Giraffe" + "Time"] = 0;
                    
                }
                //象
                if (!PlayerPrefs.HasKey("BestTime_Elephant") || deleteFlag == true)
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Elephant", 0);
                    PlayerPrefs.SetInt("bestTimeRecode_Elephant", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Elephant" + "Time"] = 0;
                }
                //犬
                if (!PlayerPrefs.HasKey("BestTime_Dog") || deleteFlag == true)
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Dog", 0);
                    PlayerPrefs.SetInt("bestTimeRecode_Dog", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Dog" + "Time"] = 0;
                }
                //虎
                if (!PlayerPrefs.HasKey("BestTime_Tiger") || deleteFlag == true)
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Tiger", 0);
                    PlayerPrefs.SetInt("bestTimeRecode_Tiger", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Tiger" + "Time"] = 0;
                }
                //猫
                if (!PlayerPrefs.HasKey("BestTime_Cat") || deleteFlag == true)
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Cat", 0);
                    PlayerPrefs.SetInt("bestTimeRecode_Cat", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Cat" + "Time"] = 0;
                }
                //ウサギ
                if (!PlayerPrefs.HasKey("BestTime_Rabbit") || deleteFlag == true)
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Rabbit", 0);
                    PlayerPrefs.SetInt("bestTimeRecode_Rabbit", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Rabbit" + "Time"] = 0;
                }
                //総合
                if (!PlayerPrefs.HasKey("BestTime_Total") || deleteFlag == true)
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Total", PlayerPrefs.GetInt("BestTime_Giraffe") + PlayerPrefs.GetInt("BestTime_Elephant") + PlayerPrefs.GetInt("BestTime_Dog") +
                PlayerPrefs.GetInt("BestTime_Tiger") + PlayerPrefs.GetInt("BestTime_Cat") + PlayerPrefs.GetInt("BestTime_Rabbit"));
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Total" + "Time"] = PlayerPrefs.GetInt("BestTime_Total");
                }
                //スコア(データ消去時のみ)
                if (deleteFlag == true)
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("WinCount", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Score"] = 0;
                }
                objList[0].Save();
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
    /// サーバにオフラインハイスコアを保存
    /// </summary>
    public void save_Offline(string animal)
    {
        //データスコアの「HighScore」クラスから、Nameをキーにして検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
        query.WhereEqualTo("Name", PlayerPrefs.GetString("NickName"));
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null)
            {
                objList[0]["Offline" + animal + "Time"] = PlayerPrefs.GetInt("BestTime_" + animal);
                objList[0].SaveAsync();
            }
        });
    }

    /// <summary>
    /// mobile backendに接続してtop30取得
    /// </summary>
    public void TopRankers()
    {
        //ニックネームのID削除用
        string nickName;
        int bkIndex;

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
                //for (int i = 0; i < objList.Count; i++)
                //{
                //    //ランキング名前の表示
                //    topRankingName[i] = (i + 1).ToString("") + "位 : " + objList[i]["Name"];
                //    //ランキング番号の表示
                //    topRankingNumber[i] += objList[i]["Score"] + "ポイント\n";
                //    menuUI.WinCountRankingNameText.text += (topRankingName[i].PadRight(25)) + topRankingNumber[i];

                //}


                for (int i = 0; i < objList.Count; i++)
                {
                    //1位〜3位の文字変更
                    if (i == 0)
                    {
                        topRankingName[i] = "<color=#FFD700>";
                        topRankingNumber[i] = "<color=#FFD700>";
                    }
                    else if (i == 1)
                    {
                        topRankingName[i] = "<color=#f4f4f4>";
                        topRankingNumber[i] = "<color=#f4f4f4>";
                    }
                    else if (i == 2)
                    {
                        topRankingName[i] = "<color=#BA6E40>";
                        topRankingNumber[i] = "<color=#BA6E40>";
                    }

                    //ランキング追跡中の名前からIDを抜き取る
                    nickName = (string)objList[i]["Name"];
                    bkIndex = nickName.LastIndexOf("(");

                    /* 取得成功(名前の後ろに"("有り) */
                    if (bkIndex != -1)
                    {
                        //ランキング名前の表示
                        topRankingName[i] += (i + 1).ToString("") + "位 : " + nickName.Substring(0, bkIndex);
                    }
                    else
                    {
                        //ランキング名前の表示
                        topRankingName[i] += (i + 1).ToString("") + "位 : " + nickName;
                    }
                    //ランキング番号の表示
                    topRankingNumber[i] += objList[i]["Score"] + "ポイント";

                    //1位〜3位の文字変更
                    if (i <= 2)
                    {
                        topRankingName[i] += "</color>";
                        topRankingNumber[i] += "</color>";
                    }

                    //ランキング名前と番号の取得
                    menuUI.SetOnlineRankingInfo(topRankingName[i], topRankingNumber[i]);
                }
            }
            
        });
    }

    /// <summary>
    /// mobile backendに接続してオフラインtop15取得
    /// </summary>
    public void TopOfflineRankers(string animal)
    {

        //ニックネームのID削除用
        string nickName;
        int bkIndex;

        //MenuUIスクリプトの関数使用
        menuUI = GameObject.Find("Canvas").GetComponent<MenuUI>();

        //ランキングの配列
        string[] topRankingName = new string[50];
        string[] topRankingNumber = new string[50];

        //データスコアの「HighScore」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
        query.OrderByDescending("Offline" + animal + "Time");
        query.Limit = 15;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null)
            {

                for (int i = 0; i < objList.Count; i++)
                {
                    //1位〜3位の文字変更
                    if (i == 0)
                    {
                        topRankingName[i] = "<color=#FFD700>";
                        topRankingNumber[i] = "<color=#FFD700>";
                    }
                    else if (i == 1)
                    {
                        topRankingName[i] = "<color=#f4f4f4>";
                        topRankingNumber[i] = "<color=#f4f4f4>";
                    }
                    else if (i == 2)
                    {
                        topRankingName[i] = "<color=#BA6E40>";
                        topRankingNumber[i] = "<color=#BA6E40>";
                    }

                    //ランキング追跡中の名前からIDを抜き取る
                    nickName = (string)objList[i]["Name"];
                    bkIndex = nickName.LastIndexOf("(");

                    /* 取得成功(名前の後ろに"("有り) */
                    if (bkIndex != -1)
                    {
                        //ランキング名前変換
                        topRankingName[i] += (i + 1).ToString("") + "位 : " + nickName.Substring(0, bkIndex);
                    }
                    else
                    {
                        //ランキング名前そのまま
                        topRankingName[i] += (i + 1).ToString("") + "位 : " + nickName;
                    }

                    //ランキング番号
                    topRankingNumber[i] += objList[i]["Offline" + animal + "Time"] + "秒";

                    //1位〜3位の文字変更
                    if(i <= 2)
                    {
                        topRankingName[i] += "</color>";
                        topRankingNumber[i] += "</color>";
                    }

                    //ランキング名前とベストタイムの取得
                    menuUI.SetOfflineRankingInfo(animal, topRankingName[i], topRankingNumber[i]);
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

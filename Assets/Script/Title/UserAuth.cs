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
    public void firstSetBestTime()
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
                if (!PlayerPrefs.HasKey("BestTime_Giraffe"))
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Giraffe", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Giraffe" + "Time"] = 0;
                    
                }
                //象
                if (!PlayerPrefs.HasKey("BestTime_Elephant"))
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Elephant", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Elephant" + "Time"] = 0;
                }
                //犬
                if (!PlayerPrefs.HasKey("BestTime_Dog"))
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Dog", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Dog" + "Time"] = 1;
                }
                //虎
                if (!PlayerPrefs.HasKey("BestTime_Tiger"))
                {
                    //端末内にデータ保存
                    PlayerPrefs.SetInt("BestTime_Tiger", 0);
                    //mobile backendサーバにデータ保存
                    objList[0]["Offline" + "Tiger" + "Time"] = 0;
                }
                objList[0].SaveAsync();
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
    public void save_Offline()
    {
        //データスコアの「HighScore」クラスから、Nameをキーにして検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
        query.WhereEqualTo("Name", PlayerPrefs.GetString("NickName"));
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null)
            {
                objList[0]["Offline" + SelectCharacterUI.animalName + "Time"] = PlayerPrefs.GetInt("BestTime_" + SelectCharacterUI.animalName);
                objList[0].SaveAsync();
            }
        });
    }

    /// <summary>
    /// mobile backendに接続してtop30取得
    /// </summary>
    public void TopRankers()
    {
        //村上追加分
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

                /* 村上追加分 */
                for (int i = 0; i < objList.Count; i++)
                {
                    //ランキング追跡中の名前からIDを抜き取る
                    nickName = (string)objList[i]["Name"];
                    bkIndex = nickName.LastIndexOf("(");

                    /* 取得成功(名前の後ろに"("有り) */
                    if (bkIndex != -1)
                    {
                        //ランキング名前の表示
                        topRankingName[i] = (i + 1).ToString("") + "位 : " + nickName.Substring(0, bkIndex);
                    }
                    else
                    {
                        //ランキング名前の表示
                        topRankingName[i] = (i + 1).ToString("") + "位 : " + nickName;
                    }
                    //ランキング番号の表示
                    topRankingNumber[i] += objList[i]["Score"] + "ポイント\n";
                    menuUI.WinCountRankingNameText.text += (topRankingName[i].PadRight(25)) + topRankingNumber[i];

                }
                /* ここまで村上追加分 */
            }
            
        });
    }

    /// <summary>
    /// mobile backendに接続してオフラインtop15取得
    /// </summary>
    public void TopOfflineRankers(string animal)
    {
        //村上追加分
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
                //for (int i = 0; i < objList.Count; i++)
                //{
                //    //ランキング名前の表示
                //    topRankingName[i] = (i + 1).ToString("") + "位 : " + objList[i]["Name"];
                    
                //    //ランキング番号の表示
                //    topRankingNumber[i] += objList[i]["Offline" + animal + "Time"] + "秒\n";
                    

                //    //ランキング名前とベストタイムの表示
                //    switch (animal)
                //    {
                //        case "Giraffe":
                //            menuUI.OfflineRankingGiraffeNameText.text += (topRankingName[i].PadRight(25)) + topRankingNumber[i];
                //            break;
                //        case "Elephant":
                //            menuUI.OfflineRankingElephantNameText.text += (topRankingName[i].PadRight(25)) + topRankingNumber[i];
                //            break;
                //        case "Dog":
                //            menuUI.OfflineRankingDogNameText.text += (topRankingName[i].PadRight(25)) + topRankingNumber[i];
                //            break;
                //        case "Tiger":
                //            menuUI.OfflineRankingTigerNameText.text += (topRankingName[i].PadRight(25)) + topRankingNumber[i];
                //            break;
                //    }

                //}

                /* 村上追加分 */
                for (int i = 0; i < objList.Count; i++)
                {
                    //ランキング追跡中の名前からIDを抜き取る
                    nickName = (string)objList[i]["Name"];
                    bkIndex = nickName.LastIndexOf("(");

                    /* 取得成功(名前の後ろに"("有り) */
                    if (bkIndex != -1)
                    {
                        //ランキング名前の表示
                        topRankingName[i] = (i + 1).ToString("") + "位 : " + nickName.Substring(0, bkIndex);
                    }
                    else
                    {
                        //ランキング名前の表示
                        topRankingName[i] = (i + 1).ToString("") + "位 : " + nickName;
                    }

                    //ランキング番号の表示
                    topRankingNumber[i] += objList[i]["Score"] + "ポイント\n";

                    //ランキング名前とベストタイムの表示
                    switch (animal)
                    {
                        case "Giraffe":
                            menuUI.OfflineRankingGiraffeNameText.text += (topRankingName[i].PadRight(25)) + topRankingNumber[i];
                            break;
                        case "Elephant":
                            menuUI.OfflineRankingElephantNameText.text += (topRankingName[i].PadRight(25)) + topRankingNumber[i];
                            break;
                        case "Dog":
                            menuUI.OfflineRankingDogNameText.text += (topRankingName[i].PadRight(25)) + topRankingNumber[i];
                            break;
                        case "Tiger":
                            menuUI.OfflineRankingTigerNameText.text += (topRankingName[i].PadRight(25)) + topRankingNumber[i];
                            break;
                    }
                }
                /* ここまで村上追加分 */
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

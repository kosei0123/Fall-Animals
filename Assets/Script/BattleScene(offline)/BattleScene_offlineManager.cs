using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScene_offlineManager : MonoBehaviour
{
    //CharacterMainMove_offlineのスクリプトを使う
    CharacterMainMove_offline characterMainMove_offline;
    //TitleTapのScriptを使う
    ScreenTouch_offline screenTouch_offline;
    //EndDialogの関数等を使う
    EndDialog endDialog;
    //Timer_offlineのpublic定数を使う
    Timer_offline timer_offline;
    //UserAuthのスクリプトの関数使用
    UserAuth userAuth;

    //プレイヤーのオブジェクト
    public GameObject animal;
    //紙飛行機のオブジェクト
    private GameObject airplane;
    //岩のオブジェクト
    private GameObject rock;
    //コインオブジェクト
    private GameObject coin;


    //紙飛行機の生成時間(初期値設定)
    private float airplaneCreateTime = 3;
    //岩の生成時間(初期値設定)
    private float rockCreateTime = 3;
    //岩の生成時間(コイン設定)
    private float coinCreateTime = 3;

    //バトル中に取得したコイン
    public int getBattleCoin = 0;

    //rankingを表示する
    public int timeRanking = 0;

    //バトルを終了したフラグ
    public bool battleFinishFlag = false;

    // Start is called before the first frame update
    void Start()
    {

        //TitleTapのScriptを使う
        screenTouch_offline = GameObject.Find("ScreenTouch").GetComponent<ScreenTouch_offline>();
        //EndDialogの関数等を使う
        //endDialog = GameObject.Find("DialogCanvas").GetComponent<EndDialog>();
        //Timer_offlineのpublic定数を使う
        timer_offline = GameObject.Find("TimerCanvas").GetComponent<Timer_offline>();
        //UserAuthのスクリプトの関数使用
        //userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();

        //ルームに入室後の設定
        CreateCharacter();

    }

    // Update is called once per frame
    void Update()
    {
        //自分の画面の自キャラのみ操作できるようにする
        if (characterMainMove_offline.offlineflag == false)
        {
            return;
        }

        //紙飛行機を作る
        if (airplaneCreateTime <= 0)
        {
            AirplaneCreated();
            airplaneCreateTime = Random.Range(1, 5);
        }
        //岩を作る
        if (rockCreateTime <= 0)
        {
            RockCreated();
            rockCreateTime = Random.Range(1, 5);
        }
        //コインを作る
        if (coinCreateTime <= 0)
        {
            CoinCreated();
            coinCreateTime = Random.Range(6, 10);
        }

        //生成時間を減らす
        //紙飛行機
        airplaneCreateTime -= Time.deltaTime;
        //岩
        rockCreateTime -= Time.deltaTime;
        //コイン
        coinCreateTime -= Time.deltaTime;


    }

    //Joined Room
    private void CreateCharacter()
    {
        //プレイキャラのオブジェクトを生成
        animal = (GameObject)Instantiate(Resources.Load("Offline/" + SelectCharacterUI_offline.animalName), new Vector3(-4.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
        animal.name = "animal1";

        //Scriptを設定し、フラグを指定する。
        characterMainMove_offline = animal.GetComponent<CharacterMainMove_offline>();
        //ScreenTouch
        //Scriptを設定し、オブジェクトを取得する。
        screenTouch_offline.GetComponent<ScreenTouch_offline>().target = animal;
    }

    //紙飛行機のインスタンス化
    private void AirplaneCreated()
    {
        //ランダム値取得(0 ~ 999)
        int randomAirplane = Random.Range(0, 1000);

        //右に出現
        //ゆっくり
        if (randomAirplane >= 0 && randomAirplane < 100)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Offline/Airplane"), new Vector3(15.0f, 3.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane0";
        }
        //普通
        else if (randomAirplane >= 100 && randomAirplane < 200)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Offline/Airplane"), new Vector3(15.0f, 3.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane1";
        }
        ////高速
        else if (randomAirplane >= 200 && randomAirplane < 300 && timer_offline.elapsedTime > 100)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Offline/Airplane"), new Vector3(15.0f, 3.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane2";
        }
        //左に出現
        //ゆっくり
        else if (randomAirplane >= 300 && randomAirplane < 400)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Offline/Airplane"), new Vector3(-15.0f, 3.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane3";
        }
        //普通
        else if (randomAirplane >= 400 && randomAirplane < 500)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Offline/Airplane"), new Vector3(-15.0f, 3.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane4";
        }
        ////高速
        else if (randomAirplane >= 500 && randomAirplane < 600 && timer_offline.elapsedTime > 200)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Offline/Airplane"), new Vector3(-15.0f, 3.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane5";
        }
    }

    //岩のインスタンス化
    private void RockCreated()
    {
        //ランダム値取得(0 ~ 999)
        int randomrock = Random.Range(0, 1000);

        //右に出現
        //ゆっくり
        if (randomrock >= 0 && randomrock < 100)
        {
            rock = (GameObject)Instantiate(Resources.Load("Offline/Rock"), new Vector3(15.0f, 1.5f, 0), Quaternion.identity);
            rock.name = "Rock0";
        }
        ////普通
        else if (randomrock >= 100 && randomrock < 200 && timer_offline.elapsedTime > 300)
        {
            rock = (GameObject)Instantiate(Resources.Load("Offline/Rock"), new Vector3(15.0f, 1.5f, 0), Quaternion.identity);
            rock.name = "Rock1";
        }
        //高速
        else if (randomrock >= 200 && randomrock < 300)
        {
            rock = (GameObject)Instantiate(Resources.Load("Offline/Rock"), new Vector3(15.0f, 2.5f, 0), Quaternion.identity);
            rock.name = "Rock2";
        }
        //左に出現
        //ゆっくり
        else if (randomrock >= 300 && randomrock < 400)
        {
            rock = (GameObject)Instantiate(Resources.Load("Offline/Rock"), new Vector3(-15.0f, 1.5f, 0), Quaternion.identity);
            rock.name = "Rock3";
        }
        ////普通
        else if (randomrock >= 400 && randomrock < 500 && timer_offline.elapsedTime > 400)
        {
            rock = (GameObject)Instantiate(Resources.Load("Offline/Rock"), new Vector3(-15.0f, 1.5f, 0), Quaternion.identity);
            rock.name = "Rock4";
        }
        //高速
        else if (randomrock >= 500 && randomrock < 600)
        {
            rock = (GameObject)Instantiate(Resources.Load("Offline/Rock"), new Vector3(-15.0f, 2.5f, 0), Quaternion.identity);
            rock.name = "Rock5";
        }
    }

    //コインのインスタンス化
    private void CoinCreated()
    {
        float randomCoin = Random.Range(-8.0f, 8.0f);
        coin = (GameObject)Instantiate(Resources.Load("Offline/Coin"), new Vector3(randomCoin, 10.0f, 0), Quaternion.identity);
    }

    //勝敗のチェック
    private void Check()
    {

        //終了時のダイアログ表示
        endDialog.DialogPanelActive(1);
    }

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
    }


    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(150, 30, 150, 70), "残り人数 : " + (int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"]);
    }
}

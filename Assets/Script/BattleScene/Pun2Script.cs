using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Pun2Script : MonoBehaviourPunCallbacks
{
    //CharacterMainMoveのスクリプトを使う
    CharacterMainMove characterMainMove;
    //TitleTapのScriptを使う
    ScreenTouch screenTouch;
    //EndDialogの関数等を使う
    EndDialog endDialog;
    //Timerのpublic定数を使う
    Timer timer;
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

    //情報取得に使う
    private Player[] animalInformation = { null, null, null, null };

    //紙飛行機の生成時間(初期値設定)
    private float airplaneCreateTime = 3;
    //岩の生成時間(初期値設定)
    private float rockCreateTime = 3;
    //岩の生成時間(コイン設定)
    private float coinCreateTime = 3;

    //バトル中に取得したコイン
    public int getBattleCoin = 0;

    //rankingを表示する
    public int battleRanking = 0;

    //マスタークライアントの委譲時間
    public float NoMasterCliantTime = 0;
    //マスタークライアントの委譲フラグ
    private bool NoMasterCliantTimeStartFlag = false;
    private bool NoMasterCliantTimeFinishFlag = false;

    //生成されたプレイヤーの数を取得し終えたかを確認
    private bool createdPlayerStartFlag = false;
    private int createdPlayerStartCount = 0;

    //接続が切れていないか確認フラグ
    private bool[] animalInformationDisconnectCheck = { false, false, false, false };

    //バトルを終了したフラグ
    public bool battleFinishFlag = false;

    //メッセージの送信に使用される
    new PhotonView photonView;


    // Start is called before the first frame update
    void Start()
    {

        //イベントを受け取るように設定
        PhotonNetwork.IsMessageQueueRunning = true;
        //ルーム内のクライアントがMasterClientと同じシーンをロードしないように設定
        PhotonNetwork.AutomaticallySyncScene = false;

        //TitleTapのScriptを使う
        screenTouch = GameObject.Find("ScreenTouch").GetComponent<ScreenTouch>();
        //EndDialogの関数等を使う
        endDialog = GameObject.Find("DialogCanvas").GetComponent<EndDialog>();
        //Timerのpublic定数を使う
        timer = GameObject.Find("TimerCanvas").GetComponent<Timer>();
        //UserAuthのスクリプトの関数使用
        userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();

        //メッセージの送信に使用される
        photonView = PhotonView.Get(this);

        //ルームに入室後の設定
        JoinedRoom();

        

        //次回もキック確認されるように設定する
        //var prps = PhotonNetwork.LocalPlayer.CustomProperties;
        //prps["NoKick"] = "false";
        //PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

        
    }

    // Update is called once per frame
    void Update()
    {
        //マスタークライアントの委譲時間を取得する
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] == true && NoMasterCliantTimeStartFlag == false)
        {
            NoMasterCliantTime = 0.1f;
            NoMasterCliantTimeStartFlag = true;
            NoMasterCliantTimeFinishFlag = false;
        }

        //マスタークライアントのみで処理(マスタークライアントのonlineflagがfalseでも岩を作る)
        if (PhotonNetwork.IsMasterClient)
        {
            //マスタークライアントの委譲が完了して、フラグを元に戻す
            if (NoMasterCliantTime >= 0)
            {
                NoMasterCliantTime -= Time.deltaTime;
            }
            else if (NoMasterCliantTime < 0 && NoMasterCliantTimeFinishFlag == false)
            {
                PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] = false;
                PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
                //フラグの共有
                photonView.RPC("NoMasterCliantTimeFlagValue", RpcTarget.All, false, true);
                
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
                coinCreateTime = Random.Range(5, 10);
            }

            //生成時間を減らす
            //紙飛行機
            airplaneCreateTime -= Time.deltaTime;
            //岩
            rockCreateTime -= Time.deltaTime;
            //コイン
            coinCreateTime -= Time.deltaTime;
        }

        //自分の画面の自キャラのみ操作できるようにする
        if (characterMainMove.onlineflag == false)
        {
            return;
        }

        //プレイヤーリストの情報を取得
        GetPlayerInformation();

        //マスタークライアントのみで処理
        if (PhotonNetwork.IsMasterClient)
        {
            //切断確認
            GetDisconnect();

            if (timer.elapsedTime >= 3.0f)
            {
                //生成プレイヤーの数を取得
                GetCreatedPlayerCount();
            }
        }


        //バトル終了
        //mugenFlag==false：時間がくるか1人になった段階で終了
        //mugenFlag==true：1人になった段階で終了
        if ((timer.battleTime <= 0 && timer.mugenFlag == false) ||
            (timer.elapsedTime >= 4.0f && (int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] == 1))
        {
            Check();
        }

    }

    //animalの情報を返す
    public Player GetAnimalInformation()
    {
        return animalInformation[0];
    }
    //animal2の情報を返す
    public Player GetAnimal2Information()
    {
        return animalInformation[1];
    }
    //animal3の情報を返す
    public Player GetAnimal3Information()
    {
        return animalInformation[2];
    }
    //animal4の情報を返す
    public Player GetAnimal4Information()
    {
        return animalInformation[3];
    }

    //Joined Room
    public void JoinedRoom()
    {
        Debug.Log("Room");

        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerCreatedNumber"] == 1)
        {
            //プレイキャラのオブジェクトを生成
            animal = PhotonNetwork.Instantiate(SelectCharacterUI.animalName, new Vector3(-4.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            animal.name = "animal1";
            //自分の名前や接続状況を設定する
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            prps["NAME"] = "Ani1";
            prps["DISCONNECT"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

            //Pun2Script
            //Scriptを設定し、フラグを指定する。
            characterMainMove = animal.GetComponent<CharacterMainMove>();
            //CharacterMainMove
            //Scriptを設定し、フラグを指定する。
            animal.GetComponent<CharacterMainMove>().SetFlag(true);
            animal.GetComponent<CharacterMainMove>().animal1NickNameFlag = true;
            //ScreenTouch
            //Scriptを設定し、オブジェクトを取得する。
            screenTouch.GetComponent<ScreenTouch>().target = animal;
            //Damaged
            //Scriptを設定し、オブジェクトを取得する。
            //animal.GetComponent<Damaged>().target = animal;
            //GroundCheck
            //animalの子オブジェクトのGroundCheckのtargetにオブジェクトを設定する
            //animal.transform.GetChild(2).gameObject.GetComponent<GroundCheck>().target = animal;


        }
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerCreatedNumber"] == 2)
        {
            //プレイキャラのオブジェクトを生成
            animal = PhotonNetwork.Instantiate(SelectCharacterUI.animalName, new Vector3(-1.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            animal.name = "animal2";
            //自分の名前や接続状況を設定する
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            prps["NAME"] = "Ani2";
            prps["DISCONNECT"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

            //Pun2Script
            //Scriptを設定し、フラグを指定する。
            characterMainMove = animal.GetComponent<CharacterMainMove>();
            //CharacterMainMove
            //Scriptを設定し、フラグを指定する。
            animal.GetComponent<CharacterMainMove>().SetFlag(true);
            animal.GetComponent<CharacterMainMove>().animal2NickNameFlag = true;
            //ScreenTouch
            //Scriptを設定し、オブジェクトを取得する。
            screenTouch.GetComponent<ScreenTouch>().target = animal;

        }
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerCreatedNumber"] == 3)
        {
            //プレイキャラのオブジェクトを生成
            animal = PhotonNetwork.Instantiate(SelectCharacterUI.animalName, new Vector3(1.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            animal.name = "animal3";
            //自分の名前や接続状況を設定する
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            prps["NAME"] = "Ani3";
            prps["DISCONNECT"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

            //Pun2Script
            //Scriptを設定し、フラグを指定する。
            characterMainMove = animal.GetComponent<CharacterMainMove>();
            //CharacterMainMove
            //Scriptを設定し、フラグを指定する。
            animal.GetComponent<CharacterMainMove>().SetFlag(true);
            animal.GetComponent<CharacterMainMove>().animal3NickNameFlag = true;
            //ScreenTouch
            //Scriptを設定し、オブジェクトを取得する。
            screenTouch.GetComponent<ScreenTouch>().target = animal;

        }
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerCreatedNumber"] == 4)
        {
            //プレイキャラのオブジェクトを生成
            animal = PhotonNetwork.Instantiate(SelectCharacterUI.animalName, new Vector3(4.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            animal.name = "animal4";
            //自分の名前や接続状況を設定する
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            prps["NAME"] = "Ani4";
            prps["DISCONNECT"] = false;
            PhotonNetwork.LocalPlayer.SetCustomProperties(prps);

            //Pun2Script
            //Scriptを設定し、フラグを指定する。
            characterMainMove = animal.GetComponent<CharacterMainMove>();
            //CharacterMainMove
            //Scriptを設定し、フラグを指定する。
            animal.GetComponent<CharacterMainMove>().SetFlag(true);
            animal.GetComponent<CharacterMainMove>().animal4NickNameFlag = true;
            //ScreenTouch
            //Scriptを設定し、オブジェクトを取得する。
            screenTouch.GetComponent<ScreenTouch>().target = animal;

        }
    }

    //プレイヤーの情報を取得する
    private void GetPlayerInformation()
    {
        foreach(var p in PhotonNetwork.PlayerList)
        {
            if ((string)p.CustomProperties["NAME"] == "Ani1")
            {
                animalInformation[0] = p;
            }
            if ((string)p.CustomProperties["NAME"] == "Ani2")
            {
                animalInformation[1] = p;
            }
            if ((string)p.CustomProperties["NAME"] == "Ani3")
            {
                animalInformation[2] = p;
            }
            if ((string)p.CustomProperties["NAME"] == "Ani4")
            {
                animalInformation[3] = p;
            }
        }
        
    }

    //生成されたプレイヤーの数を取得する
    private void GetCreatedPlayerCount()
    {
        if (createdPlayerStartFlag == false)
        {
            for (int i = 0; i < 4; i++)
            {
                if (animalInformation[i] != null)
                {
                    createdPlayerStartCount++;
                }
            }

            //ルーム内カスタムプロパティにて残り人数に追加
            var n = PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] is int value ? value : 0;
            PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] = n + createdPlayerStartCount;
            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
        }

        //1度のみ実行するようにする
        photonView.RPC("createdPlayerStartFlagValue", RpcTarget.All, true);
    }

    //切断を確認する
    private void GetDisconnect()
    {
        for (int i = 0; i < 4; i++)
        {
            if (animalInformation[i] != null && animalInformationDisconnectCheck[i] == false)
            {
                if ((bool)animalInformation[i].CustomProperties["DISCONNECT"] == true)
                {
                    //同じルーム内のWaitingRoomにいるプレイヤーの数を減らす
                    var n = PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] is int value ? value : 0;
                    PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] = n - 1;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
                    //1度のみ実行
                    photonView.RPC("animalInformationDisconnectCheckValue", RpcTarget.All, true, i);
                }
            }
        }
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
            airplane = PhotonNetwork.Instantiate("Airplane", new Vector3(30.0f, 4.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            airplane.name = "Airplane0";
        }
        //普通
        else if (randomAirplane >= 100 && randomAirplane < 200)
        {
            airplane = PhotonNetwork.Instantiate("Airplane", new Vector3(30.0f, 4.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            airplane.name = "Airplane1";
        }
        ////高速
        //else if (randomAirplane >= 200 && randomAirplane < 300)
        //{
        //    airplane = PhotonNetwork.Instantiate("Airplane", new Vector3(30.0f, 5.0f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
        //    airplane.name = "Airplane2";
        //}
        //左に出現
        //ゆっくり
        else if (randomAirplane >= 300 && randomAirplane < 400)
        {
            airplane = PhotonNetwork.Instantiate("Airplane", new Vector3(-30.0f, 4.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            airplane.name = "Airplane3";
        }
        //普通
        else if (randomAirplane >= 400 && randomAirplane < 500)
        {
            airplane = PhotonNetwork.Instantiate("Airplane", new Vector3(-30.0f, 4.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            airplane.name = "Airplane4";
        }
        ////高速
        //else if (randomAirplane >= 500 && randomAirplane < 600)
        //{
        //    airplane = PhotonNetwork.Instantiate("Airplane", new Vector3(-30.0f, 5.0f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
        //    airplane.name = "Airplane5";
        //}
    }

    //岩のインスタンス化
    private void RockCreated()
    {
        //ランダム値取得(0 ~ 999)
        int randomrock = Random.Range(0,1000);

        //右に出現
        //ゆっくり
        if (randomrock >= 0 && randomrock < 100)
        {
            rock = PhotonNetwork.Instantiate("Rock", new Vector3(25.0f, 2.5f, 0), Quaternion.identity, 0);
            rock.name = "Rock0";
        }
        ////普通
        //else if (randomrock >= 100 && randomrock < 200)
        //{
        //    rock = PhotonNetwork.Instantiate("Rock", new Vector3(25.0f, 3.0f, 0), Quaternion.identity, 0);
        //    rock.name = "Rock1";
        //}
        //高速
        else if (randomrock >= 200 && randomrock < 300)
        {
            rock = PhotonNetwork.Instantiate("Rock", new Vector3(25.0f, 3.0f, 0), Quaternion.identity, 0);
            rock.name = "Rock2";
        }
        //左に出現
        //ゆっくり
        else if (randomrock >= 300 && randomrock < 400)
        {
            rock = PhotonNetwork.Instantiate("Rock", new Vector3(-25.0f, 2.5f, 0), Quaternion.identity, 0);
            rock.name = "Rock3";
        }
        ////普通
        //else if (randomrock >= 400 && randomrock < 500)
        //{
        //    rock = PhotonNetwork.Instantiate("Rock", new Vector3(-25.0f, 3.0f, 0), Quaternion.identity, 0);
        //    rock.name = "Rock4";
        //}
        //高速
        else if (randomrock >= 500 && randomrock < 600)
        {
            rock = PhotonNetwork.Instantiate("Rock", new Vector3(-25.0f, 3.0f, 0), Quaternion.identity, 0);
            rock.name = "Rock5";
        }
    }

    //コインのインスタンス化
    private void CoinCreated()
    {
        float randomCoin = Random.Range(-8.0f, 8.0f);

        coin = PhotonNetwork.Instantiate("Coin", new Vector3(randomCoin, 20.0f, 0), Quaternion.identity, 0);
    }

    //勝敗のチェック
    private void Check()
    {
        //動きを止める
        characterMainMove.onlineflag = false;
        characterMainMove.rb.velocity = new Vector3(0, characterMainMove.rb.velocity.y, 0);
        //バトル終了フラグをtrueにする
        battleFinishFlag = true;
        //順位の確定と取得
        battleRanking = (int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"];

        //1位だった場合
        if (battleRanking == 1)
        {
            PlayerPrefs.SetInt("WinCount", PlayerPrefs.GetInt("WinCount") + 1);
            PlayerPrefs.Save();
            //mobile backendに接続してデータの保存
            userAuth.save();
        }

        //終了時のダイアログ表示
        endDialog.DialogPanelActive(battleRanking);
    }

    [PunRPC]
    //マスタークライアントの委譲が完了して、フラグを元に戻す
    private void NoMasterCliantTimeFlagValue(bool value, bool value2)
    {
        NoMasterCliantTimeStartFlag = value;
        NoMasterCliantTimeFinishFlag = value2;
    }

    [PunRPC]
    //生成されたプレイヤーの数を取得する
    private void createdPlayerStartFlagValue(bool value)
    {
        //生成されたプレイヤーの数を1度のみ取得する
        createdPlayerStartFlag = value;
    }

    [PunRPC]
    //切断を確認する
    private void animalInformationDisconnectCheckValue(bool value, int i)
    {
        //1度のみ実行される
        animalInformationDisconnectCheck[i] = value;
    }

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //切断フラグを設定する
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            prps["DISCONNECT"] = true;
            PhotonNetwork.LocalPlayer.SetCustomProperties(prps);
            //マスタークライアントの切断をルーム全体で検知する
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] = true;
                PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
            }

            //画面遷移等(0.5秒後)
            Invoke("Pun2Script_PhotonOff", 0.5f);
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
        //切断フラグを設定する
        var prps = PhotonNetwork.LocalPlayer.CustomProperties;
        prps["DISCONNECT"] = true;
        PhotonNetwork.LocalPlayer.SetCustomProperties(prps);
        //マスタークライアントの切断をルーム全体で検知する
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] = true;
            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);
        }

        //画面遷移等(0.5秒後)
        Invoke("Pun2Script_PhotonOff", 0.5f);
    }

    //Photon接続解除や画面の遷移
    private void Pun2Script_PhotonOff()
    {
        //画面遷移
        SceneManager.LoadScene("Menu");
    }


    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(150, 30, 150, 70), "残り人数 : " + (int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //プレイヤーのオブジェクト
    public GameObject animal;
    //岩のオブジェクト
    private GameObject rock;
    

    //情報取得に使う
    private Player animalInformation;
    private Player animal2Information;
    private Player animal3Information;
    private Player animal4Information;

    //岩の生成時間
    private float rockCreateTime = 0;
    //バトル開始時間
    private float battleStartTime;

    //rankingを表示する
    public int battleRanking = 0;

    // Start is called before the first frame update
    void Start()
    {
        //TitleTapのScriptを使う
        screenTouch = GameObject.Find("ScreenTouch").GetComponent<ScreenTouch>();
        //EndDialogの関数等を使う
        endDialog = GameObject.Find("DialogCanvas").GetComponent<EndDialog>();

        //ルーム内カスタムプロパティにて残り人数に追加
        var n = PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] is int value ? value : 0;
        PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] = n + PhotonNetwork.CurrentRoom.MaxPlayers;
        PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

        //ルームに入室後の設定
        JoinedRoom();

        //バトル開始時間の設定
        battleStartTime = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //マスタークライアントのみで処理
        if (PhotonNetwork.IsMasterClient)
        {
            //岩を作る
            if (rockCreateTime <= 0)
            {
                RockCreated();
                rockCreateTime = Random.Range(1, 5);
            }

            //岩の生成時間を減らす
            rockCreateTime -= Time.deltaTime;
        }

        //自分の画面の自キャラのみ操作できるようにする
        if (characterMainMove.onlineflag == false)
        {
            return;
        }

        //プレイヤーリストの情報を取得
        GetPlayerInformation();

        //バトル開始時間
        if (battleStartTime >= 0)
        {
            battleStartTime -= Time.deltaTime;
        }
        else
        {
            Check();
        }

    }

    //animalの情報を返す
    public Player GetAnimalInformation()
    {
        return animalInformation;
    }
    //animal2の情報を返す
    public Player GetAnimal2Information()
    {
        return animal2Information;
    }
    //animal3の情報を返す
    public Player GetAnimal3Information()
    {
        return animal3Information;
    }
    //animal4の情報を返す
    public Player GetAnimal4Information()
    {
        return animal4Information;
    }

    //Joined Room
    public void JoinedRoom()
    {
        Debug.Log("Room");

        if (WaitingPlayerCount.playerCreatedNumber == 1)
        {
            //プレイキャラのオブジェクトを生成
            animal = PhotonNetwork.Instantiate("animal1", new Vector3(-4.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            animal.name = "animal1";
            //自分の名前を設定する
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            prps["NAME"] = "animal1";
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
            animal.GetComponent<Damaged>().target = animal;
            //GroundCheck
            //animalの子オブジェクトのGroundCheckのtargetにオブジェクトを設定する
            animal.transform.GetChild(6).gameObject.GetComponent<GroundCheck>().target = animal;


        }
        if (WaitingPlayerCount.playerCreatedNumber == 2)
        {
            //プレイキャラのオブジェクトを生成
            animal = PhotonNetwork.Instantiate("animal1", new Vector3(-1.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            animal.name = "animal2";
            //自分の名前を設定する
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            prps["NAME"] = "animal2";
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
            //Damaged
            //Scriptを設定し、オブジェクトを取得する。
            animal.GetComponent<Damaged>().target = animal;
            //GroundCheck
            //animalの子オブジェクトのGroundCheckのtargetにオブジェクトを設定する
            animal.transform.GetChild(6).gameObject.GetComponent<GroundCheck>().target = animal;

        }
        if (WaitingPlayerCount.playerCreatedNumber == 3)
        {
            //プレイキャラのオブジェクトを生成
            animal = PhotonNetwork.Instantiate("animal1", new Vector3(1.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            animal.name = "animal3";
            //自分の名前を設定する
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            prps["NAME"] = "animal3";
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
            //Damaged
            //Scriptを設定し、オブジェクトを取得する。
            animal.GetComponent<Damaged>().target = animal;
            //GroundCheck
            //animalの子オブジェクトのGroundCheckのtargetにオブジェクトを設定する
            animal.transform.GetChild(6).gameObject.GetComponent<GroundCheck>().target = animal;

        }
        if (WaitingPlayerCount.playerCreatedNumber == 4)
        {
            //プレイキャラのオブジェクトを生成
            animal = PhotonNetwork.Instantiate("animal1", new Vector3(4.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            animal.name = "animal4";
            //自分の名前を設定する
            var prps = PhotonNetwork.LocalPlayer.CustomProperties;
            prps["NAME"] = "animal4";
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
            //Damaged
            //Scriptを設定し、オブジェクトを取得する。
            animal.GetComponent<Damaged>().target = animal;
            //GroundCheck
            //animalの子オブジェクトのGroundCheckのtargetにオブジェクトを設定する
            animal.transform.GetChild(6).gameObject.GetComponent<GroundCheck>().target = animal;

        }
    }

    //プレイヤーの情報を取得する
    private void GetPlayerInformation()
    {
        foreach(var p in PhotonNetwork.PlayerList)
        {
            if ((string)p.CustomProperties["NAME"] == "animal1")
            {
                animalInformation = p;
            }
            if ((string)p.CustomProperties["NAME"] == "animal2")
            {
                animal2Information = p;
            }
            if ((string)p.CustomProperties["NAME"] == "animal3")
            {
                animal3Information = p;
            }
            if ((string)p.CustomProperties["NAME"] == "animal4")
            {
                animal4Information = p;
            }
        }
        
    }
    

    //岩のインスタンス化
    private void RockCreated()
    {
        //ランダム値取得
        int randomrock = Random.Range(0,999);

        //右に出現
        //ゆっくり
        if (randomrock >= 0 && randomrock < 100)
        {
            rock = PhotonNetwork.Instantiate("Rock", new Vector3(15.0f, 1.1f, 0), Quaternion.identity, 0);
            rock.name = "Rock0";
        }
        //普通
        else if (randomrock >= 100 && randomrock < 200)
        {
            rock = PhotonNetwork.Instantiate("Rock", new Vector3(15.0f, 1.1f, 0), Quaternion.identity, 0);
            rock.name = "Rock1";
        }
        //高速
        else if (randomrock >= 200 && randomrock < 300)
        {
            rock = PhotonNetwork.Instantiate("Rock", new Vector3(15.0f, 1.1f, 0), Quaternion.identity, 0);
            rock.name = "Rock2";
        }
        //左に出現
        //ゆっくり
        else if (randomrock >= 300 && randomrock < 400)
        {
            rock = PhotonNetwork.Instantiate("Rock", new Vector3(-15.0f, 1.1f, 0), Quaternion.identity, 0);
            rock.name = "Rock3";
        }
        //普通
        else if (randomrock >= 400 && randomrock < 500)
        {
            rock = PhotonNetwork.Instantiate("Rock", new Vector3(-15.0f, 1.1f, 0), Quaternion.identity, 0);
            rock.name = "Rock4";
        }
        //高速
        else if (randomrock >= 500 && randomrock < 600)
        {
            rock = PhotonNetwork.Instantiate("Rock", new Vector3(-15.0f, 1.1f, 0), Quaternion.identity, 0);
            rock.name = "Rock5";
        }
    }

    //勝敗のチェック
    private void Check()
    {
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] <= 1)
        {
            //動きを止める
            characterMainMove.onlineflag = false;
            characterMainMove.rb.velocity = new Vector3(0, characterMainMove.rb.velocity.y, 0);
            //順位の確定と取得
            battleRanking = (int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"];

            //終了時のダイアログ表示
            endDialog.DialogPanelActive(battleRanking);
        }
    }

    //順位表示処理
    private void OnGUI()
    {
        GUI.TextField(new Rect(150, 30, 150, 70), "残り人数 : " + (int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"]);

    }

}

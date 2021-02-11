using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Pun2Script : MonoBehaviourPunCallbacks
{
    //TitleTapのScriptを使う
    ScreenTouch screenTouch;

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

    // Start is called before the first frame update
    void Start()
    {
        //TitleTapのScriptを使う
        screenTouch = GameObject.Find("ScreenTouch").GetComponent<ScreenTouch>();

        JoinedRoom();

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

        //プレイヤーリストの情報を取得
        GetPlayerInformation();
        
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

            //ルーム内カスタムプロパティにて残り人数に追加
            var n = PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] is int value ? value : 0;
            PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] = n + 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

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

            //ルーム内カスタムプロパティにて残り人数に追加
            var n = PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] is int value ? value : 0;
            PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] = n + 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

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

            //ルーム内カスタムプロパティにて残り人数に追加
            var n = PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] is int value ? value : 0;
            PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] = n + 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

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

            //ルーム内カスタムプロパティにて残り人数に追加
            var n = PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] is int value ? value : 0;
            PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] = n + 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

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

}

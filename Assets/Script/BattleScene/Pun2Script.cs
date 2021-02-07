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
        
    }

    //// Update is called once per frame
    //public override void OnConnectedToMaster()
    //{
    //    //ロビーに入室する
    //    PhotonNetwork.JoinLobby();

    //}

    ////ロビーに入った際の処理
    //public override void OnJoinedLobby()
    //{
    //    //RoomOptionsの用意
    //    var opt = new RoomOptions();

    //    //ルームの作成・入室
    //    PhotonNetwork.JoinOrCreateRoom("Game Room", opt, TypedLobby.Default);
    //}

    ////Joined Room
    //public override void OnJoinedRoom()
    //{

    //}

    //Joined Room
    public void JoinedRoom()
    {
        Debug.Log("Room");
        //Room myroom = PhotonNetwork.CurrentRoom;
        //Debug.Log("ルーム名：" + myroom.Name);
        //Debug.Log("PlayerNo：" + PhotonNetwork.LocalPlayer.ActorNumber);

        if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            //プレイキャラのオブジェクトを生成
            animal = PhotonNetwork.Instantiate("animal1", new Vector3(-4.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            animal.name = "animal1";

            //UnityChanControlScriptWithRgidBody
            //Scriptを設定し、フラグを指定する。
            animal.GetComponent<CharacterMainMove>().SetFlag(true);
            //ScreenTouch
            //Scriptを設定し、オブジェクトを取得する。
            screenTouch.GetComponent<ScreenTouch>().target = animal;
            //GroundCheck
            //animalの子オブジェクトのGroundCheckのtargetにオブジェクトを設定する
            animal.transform.GetChild(6).gameObject.GetComponent<GroundCheck>().target = animal;

            ////敵情報を取得する
            //foreach (var p in PhotonNetwork.PlayerList)
            //{
            //    //敵分
            //    if (PhotonNetwork.LocalPlayer.UserId != p.UserId)
            //    {
            //        player2Information = p;
            //        break;
            //    }
            //}
            ////自分情報を取得する
            //foreach (var p in PhotonNetwork.PlayerList)
            //{
            //    if (PhotonNetwork.LocalPlayer.UserId == p.UserId)
            //    {
            //        playerInformation = p;
            //        break;
            //    }
            //}

        }
        if (PhotonNetwork.LocalPlayer.ActorNumber == 2)
        {
            //プレイキャラのオブジェクトを生成
            animal = PhotonNetwork.Instantiate("animal1", new Vector3(-1.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
            animal.name = "human2";

            //UnityChanControlScriptWithRgidBody
            //Scriptを設定し、フラグを指定する。
            animal.GetComponent<CharacterMainMove>().SetFlag(true);
            //ScreenTouch
            //Scriptを設定し、オブジェクトを取得する。
            screenTouch.GetComponent<ScreenTouch>().target = animal;
            //GroundCheck
            //animalの子オブジェクトのGroundCheckのtargetにオブジェクトを設定する
            animal.transform.GetChild(6).gameObject.GetComponent<GroundCheck>().target = animal;

            ////敵情報を取得する
            //foreach (var p in PhotonNetwork.PlayerList)
            //{
            //    if (PhotonNetwork.LocalPlayer.UserId != p.UserId)
            //    {
            //        playerInformation = p;
            //        break;
            //    }
            //}
            ////自分情報を取得する
            //foreach (var p in PhotonNetwork.PlayerList)
            //{
            //    if (PhotonNetwork.LocalPlayer.UserId == p.UserId)
            //    {
            //        player2Information = p;
            //        break;
            //    }
            //}
        }
        //if (PhotonNetwork.LocalPlayer.ActorNumber == 3)
        //{
        //    //プレイキャラのオブジェクトを生成
        //    animal = PhotonNetwork.Instantiate("animal1", new Vector3(1.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
        //    animal.name = "animal3";

        //    //敵情報を取得する
        //    foreach (var p in PhotonNetwork.PlayerList)
        //    {
        //        if (PhotonNetwork.LocalPlayer.UserId != p.UserId)
        //        {
        //            playerInformation = p;
        //            break;
        //        }
        //    }
        //    //自分情報を取得する
        //    foreach (var p in PhotonNetwork.PlayerList)
        //    {
        //        if (PhotonNetwork.LocalPlayer.UserId == p.UserId)
        //        {
        //            player2Information = p;
        //            break;
        //        }
        //    }
        //}
        //if (PhotonNetwork.LocalPlayer.ActorNumber >= 4)
        //{
        //    //プレイキャラのオブジェクトを生成
        //    animal = PhotonNetwork.Instantiate("animal1", new Vector3(4.5f, 1.1f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f), 0);
        //    animal.name = "animal4";

        //    //敵情報を取得する
        //    foreach (var p in PhotonNetwork.PlayerList)
        //    {
        //        if (PhotonNetwork.LocalPlayer.UserId != p.UserId)
        //        {
        //            playerInformation = p;
        //            break;
        //        }
        //    }
        //    //自分情報を取得する
        //    foreach (var p in PhotonNetwork.PlayerList)
        //    {
        //        if (PhotonNetwork.LocalPlayer.UserId == p.UserId)
        //        {
        //            player2Information = p;
        //            break;
        //        }
        //    }
        //}
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Skins : MonoBehaviourPunCallbacks,IPunObservable
{
    //CharacterMainMoveのpublic定数を使う
    CharacterMainMove characterMainMove;

    //Candy
    [SerializeField]
    private GameObject Candy;
    private bool CandyFlag = false;
    //Crown
    [SerializeField]
    private GameObject Crown;
    private bool CrownFlag = false;
    //Cloud
    [SerializeField]
    private GameObject Cloud;
    private bool CloudFlag = false;
    //Mappin
    [SerializeField]
    private GameObject Mappin;
    private bool MappinFlag = false;
    //Crystal
    [SerializeField]
    private GameObject Crystal;
    private bool CrystalFlag = false;

    // Start is called before the first frame update
    void Start()
    {

        //CharacterMainMoveのpublic定数を使う
        characterMainMove = this.gameObject.GetComponent<CharacterMainMove>();

        //自分の画面の自キャラのみ操作できるようにする
        if (characterMainMove.onlineflag == false)
        {
            return;
        }

        //None
        if (SelectSkins.skinsName == null)
        {
            Candy.SetActive(false);
            Crown.SetActive(false);
            Cloud.SetActive(false);
            Mappin.SetActive(false);
            Crystal.SetActive(false);
            return;
        }

        //Candy
        CandyFlag = (SelectSkins.skinsName == "Candy") ? true : false;
        Candy.SetActive(CandyFlag);
        //Crown
        CrownFlag = (SelectSkins.skinsName == "Crown") ? true : false;
        Crown.SetActive(CrownFlag);
        //Cloud
        CloudFlag = (SelectSkins.skinsName == "Cloud") ? true : false;
        Cloud.SetActive(CloudFlag);
        //Mappin
        MappinFlag = (SelectSkins.skinsName == "Mappin") ? true : false;
        Mappin.SetActive(MappinFlag);
        //Crystal
        CrystalFlag = (SelectSkins.skinsName == "Crystal") ? true : false;
        Crystal.SetActive(CrystalFlag);
    }

    // Update is called once per frame
    void Update()
    {
        

        //自分の画面の自キャラのみ操作できるようにする
        if (characterMainMove.onlineflag == false)
        {
            return;
        }

        //None
        if (SelectSkins.skinsName == null)
        {
            return;
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        

        if (stream.IsWriting)
        {

            //データの送信
            //スキン
            //キリン
            if (SelectCharacterUI.animalName == "Giraffe")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(4).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Candy,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Crown
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
            }
            //象
            else if (SelectCharacterUI.animalName == "Elephant")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Candy,Crown,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
            }
            //犬
            else if (SelectCharacterUI.animalName == "Dog")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Candy,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Crown
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
            }
            //虎
            else if (SelectCharacterUI.animalName == "Tiger")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Candy,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Crown
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
            }
            //猫
            else if (SelectCharacterUI.animalName == "Cat")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Candy,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Crown
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
            }
            //ウサギ
            else if (SelectCharacterUI.animalName == "Rabbit")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Candy,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                //Crown
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
            }

        }
        else
        {
            //データの受信
            //スキン
            //キリン
            if (gameObject.transform.GetChild(0).gameObject.name == "Giraffe")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(4).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Candy,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Crown
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
            }
            //象
            else if (gameObject.transform.GetChild(0).gameObject.name == "Elephant")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Candy,Crown,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
            }
            //犬
            else if (gameObject.transform.GetChild(0).gameObject.name == "Dog")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Candy,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Crown
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
            }
            //虎
            else if (gameObject.transform.GetChild(0).gameObject.name == "Tiger")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Candy,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Crown
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
            }
            //猫
            else if (gameObject.transform.GetChild(0).gameObject.name == "Cat")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Candy,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Crown
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
            }
            //ウサギ
            else if (gameObject.transform.GetChild(0).gameObject.name == "Rabbit")
            {
                //Cloud
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Candy,Mappin,Crystal
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                //Crown
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
            }
        }
    }
}

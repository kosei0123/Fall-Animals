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
            return;
        }

        //Candy
        CandyFlag = (SelectSkins.skinsName == "Candy") ? true : false;
        Candy.SetActive(CandyFlag);
        //Crown
        CrownFlag = (SelectSkins.skinsName == "Crown") ? true : false;
        Crown.SetActive(CrownFlag);

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

        //向きの反転
        //Candy
        //if (((characterMainMove.transformCache.localScale.z < 0 && characterMainMove.moveDirection > 0.1) ||
        //    (characterMainMove.transformCache.localScale.z > 0 && characterMainMove.moveDirection < -0.1)) && Candy.activeSelf == true)
        //{
        //    Candy.transform.localScale = new Vector3(Candy.transform.localScale.x, Candy.transform.localScale.y, Candy.transform.localScale.z * (-1));
        //}
        //if (((characterMainMove.transformCache.localScale.z < 0 && characterMainMove.moveDirection > 0.1) ||
        //    (characterMainMove.transformCache.localScale.z > 0 && characterMainMove.moveDirection < -0.1)) && Candy_Sit.activeSelf == true)
        //{
        //    Candy_Sit.transform.localScale = new Vector3(Candy_Sit.transform.localScale.x, Candy_Sit.transform.localScale.y, Candy_Sit.transform.localScale.z * (-1));
        //}

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
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
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
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
            }
            //犬
            else if (SelectCharacterUI.animalName == "Dog")
            {
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
            }
            //虎
            else if (SelectCharacterUI.animalName == "Tiger")
            {
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    stream.SendNext(childTransform.gameObject.activeSelf);
                }
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
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
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
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
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(4).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
            }
            //犬
            else if (gameObject.transform.GetChild(0).gameObject.name == "Dog")
            {
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
            }
            //虎
            else if (gameObject.transform.GetChild(0).gameObject.name == "Tiger")
            {
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
                foreach (Transform childTransform in this.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.
                    transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform)
                {
                    childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
                }
            }
        }
    }
}

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
    [SerializeField]
    private GameObject Candy_Sit;
    private bool Candy_SitFlag = false;

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

        //Candy
        CandyFlag = ((SelectSkins.skinsName == "Candy") && (characterMainMove.sitFlag == false)) ? true : false;
        Candy.SetActive(CandyFlag);
        Candy_SitFlag = ((SelectSkins.skinsName == "Candy") && (characterMainMove.sitFlag == true)) ? true : false;
        Candy_Sit.SetActive(Candy_SitFlag);

        //向きの反転
        //Candy
        if (((characterMainMove.transformCache.localScale.z < 0 && characterMainMove.moveDirection > 0.1) ||
            (characterMainMove.transformCache.localScale.z > 0 && characterMainMove.moveDirection < -0.1)) && Candy.activeSelf == true)
        {
            Candy.transform.localScale = new Vector3(Candy.transform.localScale.x, Candy.transform.localScale.y, Candy.transform.localScale.z * (-1));
        }
        if (((characterMainMove.transformCache.localScale.z < 0 && characterMainMove.moveDirection > 0.1) ||
            (characterMainMove.transformCache.localScale.z > 0 && characterMainMove.moveDirection < -0.1)) && Candy_Sit.activeSelf == true)
        {
            Candy_Sit.transform.localScale = new Vector3(Candy_Sit.transform.localScale.x, Candy_Sit.transform.localScale.y, Candy_Sit.transform.localScale.z * (-1));
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        

        if (stream.IsWriting)
        {
            
            //データの送信
            //スキン
            foreach (Transform childTransform in this.gameObject.transform.GetChild(4).gameObject.transform)
            {
                //Debug.Log("B");
                stream.SendNext(childTransform.gameObject.activeSelf);
            }
        }
        else
        {
            //データの受信
            //スキン
            foreach (Transform childTransform in this.gameObject.transform.GetChild(4).gameObject.transform)
            {
                //Debug.Log("C");
                childTransform.gameObject.SetActive((bool)stream.ReceiveNext());
            }
        }
    }
}

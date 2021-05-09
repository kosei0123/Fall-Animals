using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimalColors_Cat : MonoBehaviourPunCallbacks, IPunObservable
{
    //猫
    //マテリアル
    [SerializeField]
    private Material CatMaterial_Standard;
    [SerializeField]
    private Material CatMaterial_White;
    [SerializeField]
    private Material CatMaterial_Gray;

    //オンライン時の識別
    private int materialColorNumber = 1;

    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //猫
            case "Cat(N)":
                this.GetComponent<Renderer>().material = CatMaterial_Standard;
                materialColorNumber = 1;
                break;
            case "Cat(W)":
                this.GetComponent<Renderer>().material = CatMaterial_White;
                materialColorNumber = 2;
                break;
            case "Cat(G)":
                this.GetComponent<Renderer>().material = CatMaterial_Gray;
                materialColorNumber = 3;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //同期
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(materialColorNumber);
        }
        else
        {
            CatMaterialColor((int)stream.ReceiveNext());
        }
    }

    public void CatMaterialColor(int materialColorNumber)
    {
        //キャラクターカラーを変更する
        switch (materialColorNumber)
        {
            case 1:
                this.gameObject.GetComponent<Renderer>().material = CatMaterial_Standard;
                break;
            case 2:
                this.gameObject.GetComponent<Renderer>().material = CatMaterial_White;
                break;
            case 3:
                this.gameObject.GetComponent<Renderer>().material = CatMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

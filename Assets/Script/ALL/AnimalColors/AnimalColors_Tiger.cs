using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimalColors_Tiger : MonoBehaviourPunCallbacks, IPunObservable
{
    //虎
    //マテリアル
    [SerializeField]
    private Material TigerMaterial_Standard;
    [SerializeField]
    private Material TigerMaterial_White;
    [SerializeField]
    private Material TigerMaterial_Gray;

    //オンライン時の識別
    private int materialColorNumber = 1;

    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //虎
            case "Tiger(N)":
                this.GetComponent<Renderer>().material = TigerMaterial_Standard;
                materialColorNumber = 1;
                break;
            case "Tiger(W)":
                this.GetComponent<Renderer>().material = TigerMaterial_White;
                materialColorNumber = 2;
                break;
            case "Tiger(G)":
                this.GetComponent<Renderer>().material = TigerMaterial_Gray;
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
            TigerMaterialColor((int)stream.ReceiveNext());
        }
    }

    public void TigerMaterialColor(int materialColorNumber)
    {
        //キャラクターカラーを変更する
        switch (materialColorNumber)
        {
            case 1:
                this.gameObject.GetComponent<Renderer>().material = TigerMaterial_Standard;
                break;
            case 2:
                this.gameObject.GetComponent<Renderer>().material = TigerMaterial_White;
                break;
            case 3:
                this.gameObject.GetComponent<Renderer>().material = TigerMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

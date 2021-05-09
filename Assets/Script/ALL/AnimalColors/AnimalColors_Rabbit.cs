using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimalColors_Rabbit : MonoBehaviourPunCallbacks, IPunObservable
{
    //ウサギ
    //マテリアル
    [SerializeField]
    private Material RabbitMaterial_Standard;
    [SerializeField]
    private Material RabbitMaterial_White;
    [SerializeField]
    private Material RabbitMaterial_Gray;

    //オンライン時の識別
    private int materialColorNumber = 1;

    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //ウサギ
            case "Rabbit(N)":
                this.GetComponent<Renderer>().material = RabbitMaterial_Standard;
                materialColorNumber = 1;
                break;
            case "Rabbit(W)":
                this.GetComponent<Renderer>().material = RabbitMaterial_White;
                materialColorNumber = 2;
                break;
            case "Rabbit(G)":
                this.GetComponent<Renderer>().material = RabbitMaterial_Gray;
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
            RabbitMaterialColor((int)stream.ReceiveNext());
        }
    }

    public void RabbitMaterialColor(int materialColorNumber)
    {
        //キャラクターカラーを変更する
        switch (materialColorNumber)
        {
            case 1:
                this.gameObject.GetComponent<Renderer>().material = RabbitMaterial_Standard;
                break;
            case 2:
                this.gameObject.GetComponent<Renderer>().material = RabbitMaterial_White;
                break;
            case 3:
                this.gameObject.GetComponent<Renderer>().material = RabbitMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

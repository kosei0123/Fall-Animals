using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimalColors_Dog : MonoBehaviourPunCallbacks, IPunObservable
{
    //犬
    //マテリアル
    [SerializeField]
    private Material DogMaterial_Standard;
    [SerializeField]
    private Material DogMaterial_White;
    [SerializeField]
    private Material DogMaterial_Gray;

    //オンライン時の識別
    private int materialColorNumber = 1;

    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //犬
            case "Dog(N)":
                this.GetComponent<Renderer>().material = DogMaterial_Standard;
                materialColorNumber = 1;
                break;
            case "Dog(W)":
                this.GetComponent<Renderer>().material = DogMaterial_White;
                materialColorNumber = 2;
                break;
            case "Dog(G)":
                this.GetComponent<Renderer>().material = DogMaterial_Gray;
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
            DogMaterialColor((int)stream.ReceiveNext());
        }
    }

    public void DogMaterialColor(int materialColorNumber)
    {
        //キャラクターカラーを変更する
        switch (materialColorNumber)
        {
            case 1:
                this.gameObject.GetComponent<Renderer>().material = DogMaterial_Standard;
                break;
            case 2:
                this.gameObject.GetComponent<Renderer>().material = DogMaterial_White;
                break;
            case 3:
                this.gameObject.GetComponent<Renderer>().material = DogMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimalColors_Elephant : MonoBehaviourPunCallbacks, IPunObservable
{
    //象
    //マテリアル
    [SerializeField]
    private Material ElephantMaterial_Standard;
    [SerializeField]
    private Material ElephantMaterial_White;
    [SerializeField]
    private Material ElephantMaterial_Gray;

    //オンライン時の識別
    private int materialColorNumber = 1;

    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //象
            case "Elephant(N)":
                this.GetComponent<Renderer>().material = ElephantMaterial_Standard;
                materialColorNumber = 1;
                break;
            case "Elephant(W)":
                this.GetComponent<Renderer>().material = ElephantMaterial_White;
                materialColorNumber = 2;
                break;
            case "Elephant(G)":
                this.GetComponent<Renderer>().material = ElephantMaterial_Gray;
                materialColorNumber = 3;
                break;
            default:
                break;
        }
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
            ElephantMaterialColor((int)stream.ReceiveNext());
        }
    }

    public void ElephantMaterialColor(int materialColorNumber)
    {
        //キャラクターカラーを変更する
        switch (materialColorNumber)
        {
            case 1:
                this.gameObject.GetComponent<Renderer>().material = ElephantMaterial_Standard;
                break;
            case 2:
                this.gameObject.GetComponent<Renderer>().material = ElephantMaterial_White;
                break;
            case 3:
                this.gameObject.GetComponent<Renderer>().material = ElephantMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

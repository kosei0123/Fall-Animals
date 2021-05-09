using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimalColors_Giraffe : MonoBehaviourPunCallbacks, IPunObservable
{
    //キリン
    //マテリアル
    [SerializeField]
    private Material GiraffeMaterial_Standard;
    [SerializeField]
    private Material GiraffeMaterial_White;
    [SerializeField]
    private Material GiraffeMaterial_Gray;

    //オンライン時の識別
    private int materialColorNumber = 1;

    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //キリン
            case "Giraffe(N)":
                this.GetComponent<Renderer>().material = GiraffeMaterial_Standard;
                materialColorNumber = 1;
                break;
            case "Giraffe(W)":
                this.GetComponent<Renderer>().material = GiraffeMaterial_White;
                materialColorNumber = 2;
                break;
            case "Giraffe(G)":
                this.GetComponent<Renderer>().material = GiraffeMaterial_Gray;
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
            GiraffeMaterialColor((int)stream.ReceiveNext());
        }
    }

    public void GiraffeMaterialColor(int materialColorNumber)
    {
        //キャラクターカラーを変更する
        switch (materialColorNumber)
        {
            case 1:
                this.gameObject.GetComponent<Renderer>().material = GiraffeMaterial_Standard;
                break;
            case 2:
                this.gameObject.GetComponent<Renderer>().material = GiraffeMaterial_White;
                break;
            case 3:
                this.gameObject.GetComponent<Renderer>().material = GiraffeMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

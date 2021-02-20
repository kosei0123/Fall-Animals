using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RockMove : MonoBehaviourPunCallbacks,IPunObservable
{
    //岩にかかる重力や摩擦
    private Rigidbody rbRock;
    //時間計測
    private float rockTime;


    // Start is called before the first frame update
    void Start()
    {
        //重力や摩擦
        rbRock = this.GetComponent<Rigidbody>();

        //岩の移動方向に力をかける
        switch (this.name)
        {
            case "Rock0":
                rbRock.velocity = new Vector3(-3.0f, 0, 0);
                break;
            case "Rock1":
                rbRock.velocity = new Vector3(-6.0f, 0, 0);
                break;
            case "Rock2":
                rbRock.velocity = new Vector3(-9.0f, 0, 0);
                break;
            case "Rock3":
                rbRock.velocity = new Vector3(3.0f, 0, 0);
                break;
            case "Rock4":
                rbRock.velocity = new Vector3(6.0f, 0, 0);
                break;
            case "Rock5":
                rbRock.velocity = new Vector3(9.0f, 0, 0);
                break;
            default:
                break;
        }
        
        //岩が作られてからの時間計測
        rockTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //一定距離画面から離れたら消去する
        if (this.transform.position.x >= 20.0f || this.transform.position.x <= -20.0f || rockTime >= 15.0f)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }

        //岩が作られてからの時間計測
        rockTime += Time.deltaTime;
    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Obstacle")
        {
            return;
        }

        //ぶつかって止まってしまった際は消去する
        if (rbRock.velocity.x >= -0.3f && rbRock.velocity.x <= 0.3f)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    //同期
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //データの送信
            //位置と加速度
            stream.SendNext(rbRock.position);
            stream.SendNext(rbRock.velocity);

        }
        else
        {
            //データの受信
            //位置と加速度
            GetComponent<Rigidbody>().position = (Vector3)stream.ReceiveNext();
            GetComponent<Rigidbody>().velocity = (Vector3)stream.ReceiveNext();

        }
    }
}

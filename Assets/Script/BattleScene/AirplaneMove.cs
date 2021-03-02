using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AirplaneMove : MonoBehaviourPunCallbacks, IPunObservable
{
    //紙飛行機にかかる重力や摩擦
    private Rigidbody rbAirplane;
    //時間計測
    private float airplaneTime;

    

    // Start is called before the first frame update
    void Start()
    {
        //重力や摩擦
        rbAirplane = this.GetComponent<Rigidbody>();

        //オーナーの所有権を別オーナーに移譲するようにする
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView rockPhotonView = PhotonView.Get(this);
            rockPhotonView.RequestOwnership();
        }

        //紙飛行機の移動方向に力をかける
        switch (this.name)
        {
            case "Airplane0":
                rbAirplane.velocity = new Vector3(-3.0f, 0, 0);
                break;
            case "Airplane1":
                rbAirplane.velocity = new Vector3(-6.0f, 0, 0);
                break;
            case "Airplane2":
                rbAirplane.velocity = new Vector3(-9.0f, 0, 0);
                break;
            case "Airplane3":
                rbAirplane.velocity = new Vector3(3.0f, 0, 0);
                break;
            case "Airplane4":
                rbAirplane.velocity = new Vector3(6.0f, 0, 0);
                break;
            case "Airplane5":
                rbAirplane.velocity = new Vector3(9.0f, 0, 0);
                break;
            default:
                break;
        }

        //紙飛行機が作られてからの時間計測
        airplaneTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //移動方向により向きを変える
        if ((this.transform.localScale.z < 0 && rbAirplane.velocity.x > 0.1) || (this.transform.localScale.z > 0 && rbAirplane.velocity.x < -0.1))
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z * (-1));
        }

        //一定距離画面から離れたら消去する
        if (this.transform.position.x >= 20.0f || this.transform.position.x <= -20.0f || airplaneTime >= 15.0f)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }

        //紙飛行機が作られてからの時間計測
        airplaneTime += Time.deltaTime;
    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Obstacle")
        {
            return;
        }

        //ぶつかって止まってしまった際は消去する
        if (rbAirplane.velocity.x >= -0.3f && rbAirplane.velocity.x <= 0.3f)
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
            //加速度
            stream.SendNext(rbAirplane.velocity);

        }
        else
        {
            //データの受信
            //加速度
            GetComponent<Rigidbody>().velocity = (Vector3)stream.ReceiveNext();

        }
    }
}

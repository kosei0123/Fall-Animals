using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RockMove : MonoBehaviourPunCallbacks,IPunObservable
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //Timerのpublic定数を使う
    Timer timer;

    //岩にかかる重力や摩擦
    private Rigidbody rbRock;
    //時間計測
    private float rockTime;

    //メッセージの送信に使用される
    PhotonView rockPhotonView;

    //移譲を1度のみにする
    private bool NoMasterCliantFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //Timerのpublic定数を使う
        timer = GameObject.Find("TimerCanvas").GetComponent<Timer>();

        //重力や摩擦
        rbRock = this.GetComponent<Rigidbody>();

        //メッセージの送信に使用される
        rockPhotonView = PhotonView.Get(this);

        //オーナーの所有権を別オーナーに移譲するようにする
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] == true && NoMasterCliantFlag == true)
        {
            rockPhotonView.RequestOwnership();
            //1度のみ実行
            NoMasterCliantFlag = false;
        }
        else if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] == false)
        {
            NoMasterCliantFlag = true;
        }

        //岩の移動方向に力をかける
        switch (this.name)
        {
            case "Rock0":
                rbRock.velocity = new Vector3(-3.0f - (timer.elapsedTime / 5), 0, 0);
                break;
            case "Rock1":
                rbRock.velocity = new Vector3(-6.0f - (timer.elapsedTime / 5), 0, 0);
                break;
            case "Rock2":
                rbRock.velocity = new Vector3(-9.0f - (timer.elapsedTime / 5), 0, 0);
                break;
            case "Rock3":
                rbRock.velocity = new Vector3(3.0f + (timer.elapsedTime / 5), 0, 0);
                break;
            case "Rock4":
                rbRock.velocity = new Vector3(6.0f + (timer.elapsedTime / 5), 0, 0);
                break;
            case "Rock5":
                rbRock.velocity = new Vector3(9.0f + (timer.elapsedTime / 5), 0, 0);
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

        //オーナーの所有権を別オーナーに移譲するようにする
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] == true && NoMasterCliantFlag == true)
        {
            //メッセージの送信に使用される
            rockPhotonView.RequestOwnership();
            //1度のみ実行
            NoMasterCliantFlag = false;
        }
        else if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] == false)
        {
            NoMasterCliantFlag = true;
        }

        //一定距離画面から離れたら消去する
        if (this.transform.position.x >= 27.0f || this.transform.position.x <= -27.0f || rockTime >= 15.0f)
        {
            //マスタークライアントが削除する
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

        //岩が作られてからの時間計測
        rockTime += Time.deltaTime;
    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "SlidingPlayer")
        {
            //SEの使用
            soundManager.SEManager("Rock_sound1");
        }

        if (other.gameObject.tag != "Obstacle_Rock")
        {
            return;
        }

        //ぶつかって止まってしまった際は消去する
        if (rbRock.velocity.x >= -0.3f && rbRock.velocity.x <= 0.3f)
        {
            //マスタークライアントが削除する
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    //同期
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //データの送信
            //加速度
            stream.SendNext(rbRock.velocity);

        }
        else
        {
            //データの受信
            //加速度
            GetComponent<Rigidbody>().velocity = (Vector3)stream.ReceiveNext();

        }
    }
}

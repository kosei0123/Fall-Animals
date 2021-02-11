using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RockMove : MonoBehaviourPunCallbacks
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
        if (this.transform.position.x >= 20.0f || this.transform.position.x <= -20.0f || rockTime >= 15.0f ||
            (rbRock.velocity.x >= -0.3f && rbRock.velocity.x <= 0.3f))
        {
            PhotonNetwork.Destroy(this.gameObject);
        }

        //岩が作られてからの時間計測
        rockTime += Time.deltaTime;
    }
}

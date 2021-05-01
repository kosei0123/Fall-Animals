using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove_offline : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //Timer_offlineのpublic定数を使う
    Timer_offline timer_offline;

    //岩にかかる重力や摩擦
    private Rigidbody rbRock;
    //時間計測
    private float rockTime;

    //速度を保持する
    private float rbRockVelocityX_Retention;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //Timer_offlineのpublic定数を使う
        timer_offline = GameObject.Find("TimerCanvas").GetComponent<Timer_offline>();

        //重力や摩擦
        rbRock = this.GetComponent<Rigidbody>();

        //Y軸方向の力をランダムに加える
        float randomRockVelocity_Y = Random.Range(0, 2);

        //岩の移動方向に力をかける
        switch (this.name)
        {
            case "Rock0":
                rbRock.velocity = new Vector3(-3.0f - (timer_offline.elapsedTime / 10), randomRockVelocity_Y, 0);
                break;
            case "Rock1":
                rbRock.velocity = new Vector3(-6.0f - (timer_offline.elapsedTime / 10), randomRockVelocity_Y, 0);
                break;
            case "Rock2":
                rbRock.velocity = new Vector3(-9.0f - (timer_offline.elapsedTime / 10), randomRockVelocity_Y, 0);
                break;
            case "Rock3":
                rbRock.velocity = new Vector3(3.0f + (timer_offline.elapsedTime / 20), randomRockVelocity_Y, 0);
                break;
            case "Rock4":
                rbRock.velocity = new Vector3(6.0f + (timer_offline.elapsedTime / 20), randomRockVelocity_Y, 0);
                break;
            case "Rock5":
                rbRock.velocity = new Vector3(9.0f + (timer_offline.elapsedTime / 20), randomRockVelocity_Y, 0);
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
        if (this.transform.position.x >= 30.0f || this.transform.position.x <= -30.0f ||
            this.transform.position.y < -50.0f || rockTime >= 15.0f)
        {
            Destroy(this.gameObject);
        }

        //岩の速度を保持する
        if (rbRock.velocity.x != 0) rbRockVelocityX_Retention = rbRock.velocity.x;

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

        if (other.gameObject.tag == "Ground_Reflection")
        {
            //x方向のベクトルを反転させる
            rbRock.velocity = new Vector3(rbRockVelocityX_Retention * (-1.0f), rbRock.velocity.y, 0);
        }

        if (other.gameObject.tag != "Obstacle_Rock")
        {
            return;
        }

        //ぶつかって止まってしまった際は消去する
        if (rbRock.velocity.x >= -0.3f && rbRock.velocity.x <= 0.3f)
        {
            Destroy(this.gameObject);
        }
    }
}

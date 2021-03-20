using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneMove_offline : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //Timer_offlineのpublic定数を使う
    Timer_offline timer_offline;

    //紙飛行機にかかる重力や摩擦
    private Rigidbody rbAirplane;
    //時間計測
    private float airplaneTime;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //Timer_offlineのpublic定数を使う
        timer_offline = GameObject.Find("TimerCanvas").GetComponent<Timer_offline>();

        //重力や摩擦
        rbAirplane = this.GetComponent<Rigidbody>();

        //Y軸方向の力をランダムに加える
        float randomAirplaneVelocity_Y = Random.Range(-2, 2);

        //紙飛行機の移動方向に力をかける
        switch (this.name)
        {
            

            case "Airplane0":
                rbAirplane.velocity = new Vector3(-3.0f - (timer_offline.elapsedTime / 40), randomAirplaneVelocity_Y, 0);
                break;
            case "Airplane1":
                rbAirplane.velocity = new Vector3(-6.0f - (timer_offline.elapsedTime / 40), randomAirplaneVelocity_Y, 0);
                break;
            case "Airplane2":
                rbAirplane.velocity = new Vector3(-9.0f - (timer_offline.elapsedTime / 40), randomAirplaneVelocity_Y, 0);
                break;
            case "Airplane3":
                rbAirplane.velocity = new Vector3(3.0f + (timer_offline.elapsedTime / 40), randomAirplaneVelocity_Y, 0);
                break;
            case "Airplane4":
                rbAirplane.velocity = new Vector3(6.0f + (timer_offline.elapsedTime / 40), randomAirplaneVelocity_Y, 0);
                break;
            case "Airplane5":
                rbAirplane.velocity = new Vector3(9.0f + (timer_offline.elapsedTime / 40), randomAirplaneVelocity_Y, 0);
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
        if (this.transform.position.x >= 35.0f || this.transform.position.x <= -35.0f || airplaneTime >= 15.0f)
        {
            Destroy(this.gameObject);
        }

        //紙飛行機が作られてからの時間計測
        airplaneTime += Time.deltaTime;
    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "SlidingPlayer")
        {
            //SEの使用
            soundManager.SEManager("Airplane_sound1");
        }

        if (other.gameObject.tag != "Obstacle")
        {
            return;
        }

        //ぶつかって止まってしまった際は消去する
        if (rbAirplane.velocity.x >= -0.3f && rbAirplane.velocity.x <= 0.3f)
        {
            Destroy(this.gameObject);
        }
    }

}

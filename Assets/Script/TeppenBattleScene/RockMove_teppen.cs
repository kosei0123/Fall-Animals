using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove_teppen : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //Timer_teppenのpublic定数を使う
    Timer_teppen timer_teppen;
    //BattleScene_teppenManagerの定数を使う
    BattleScene_teppenManager battleScene_teppenManager;
    //Replayの関数を使う
    Replay.ReplayManager replayManager;

    //岩にかかる重力や摩擦
    private Rigidbody rbRock;
    //時間計測
    private float rockTime;

    //プレイ開始から岩が消えるまでの時間
    private float rockElapsedTime;
    //リプレイ押下時にまだアクティプ状態の岩を非表示にするためのもの
    private bool battleFinishReplayFlag = false;

    //速度を保持する
    private float rbRockVelocityX_Retention;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //Timer_teppenのpublic定数を使う
        timer_teppen = GameObject.Find("TimerCanvas").GetComponent<Timer_teppen>();
        //BattleScene_teppenManagerの定数を使う
        battleScene_teppenManager = GameObject.Find("BattleScene_offlineManager").GetComponent<BattleScene_teppenManager>();
        //Replayの関数を使う
        replayManager = GameObject.Find("REPLAY").GetComponent<Replay.ReplayManager>();

        //重力や摩擦
        rbRock = this.GetComponent<Rigidbody>();

        //Y軸方向の力をランダムに加える
        float randomRockVelocity_Y = Random.Range(0, 2);

        //岩の移動方向に力をかける
        switch (this.name)
        {
            case "Rock0":
                rbRock.velocity = new Vector3(-3.0f - (float)PlayerPrefs.GetInt("TeppenFloor"), randomRockVelocity_Y, 0);
                break;
            case "Rock1":
                rbRock.velocity = new Vector3(-6.0f - (float)PlayerPrefs.GetInt("TeppenFloor"), randomRockVelocity_Y, 0);
                break;
            case "Rock2":
                rbRock.velocity = new Vector3(-9.0f - (float)PlayerPrefs.GetInt("TeppenFloor"), randomRockVelocity_Y, 0);
                break;
            case "Rock3":
                rbRock.velocity = new Vector3(3.0f + (float)PlayerPrefs.GetInt("TeppenFloor"), randomRockVelocity_Y, 0);
                break;
            case "Rock4":
                rbRock.velocity = new Vector3(6.0f + (float)PlayerPrefs.GetInt("TeppenFloor"), randomRockVelocity_Y, 0);
                break;
            case "Rock5":
                rbRock.velocity = new Vector3(9.0f + (float)PlayerPrefs.GetInt("TeppenFloor"), randomRockVelocity_Y, 0);
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
        if (((this.transform.position.x >= 30.0f || this.transform.position.x <= -30.0f ||
            this.transform.position.y < -50.0f || rockTime >= 15.0f) && battleScene_teppenManager.battleFinishFlag == false) ||
            (battleScene_teppenManager.battleFinishFlag == true && battleFinishReplayFlag == false))
        {
            rockElapsedTime = timer_teppen.elapsedTime;
            battleFinishReplayFlag = true;
            this.gameObject.SetActive(false);
        }
        else if ((this.transform.position.x >= 30.0f || this.transform.position.x <= -30.0f ||
            this.transform.position.y < -50.0f || replayManager._slide.value >= rockElapsedTime) && battleScene_teppenManager.battleFinishFlag == true)
        {
            this.gameObject.SetActive(false);
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
        if ((rbRock.velocity.x >= -0.3f && rbRock.velocity.x <= 0.3f) && battleScene_teppenManager.battleFinishFlag == false)
        {
            rockElapsedTime = timer_teppen.elapsedTime;
            battleFinishReplayFlag = true;
            this.gameObject.SetActive(false);
        }
    }
}

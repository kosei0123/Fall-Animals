using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang_teppen : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //Timer_teppenのpublic定数を使う
    Timer_teppen timer_teppen;
    //BattleScene_teppenManagerの定数を使う
    BattleScene_teppenManager battleScene_teppenManager;
    //Replayの関数を使う
    Replay.ReplayManager replayManager;

    //ブーメランにかかる重力や摩擦
    private Rigidbody rbBoomerang;
    //時間計測
    private float boomerangTime;
    //ブーメランの向き
    private float boomerangDirection;

    //プレイ開始からブーメランが消えるまでの時間
    private float boomerangElapsedTime;
    //リプレイ押下時にまだアクティプ状態のブーメランを非表示にするためのもの
    private bool battleFinishReplayFlag = false;

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
        rbBoomerang = this.GetComponent<Rigidbody>();

        //Y軸方向の力をランダムに加える
        float randomBoomerangVelocity_Y = Random.Range(-3, 3);

        //紙飛行機の移動方向に力をかける
        switch (this.name)
        {


            case "Boomerang0":
                rbBoomerang.velocity = new Vector3(-3.0f - ((float)PlayerPrefs.GetInt("TeppenFloor") / 2.0f), randomBoomerangVelocity_Y, 0);
                boomerangDirection = 4.0f;
                break;
            case "Boomerang1":
                rbBoomerang.velocity = new Vector3(-6.0f - ((float)PlayerPrefs.GetInt("TeppenFloor") / 2.0f), randomBoomerangVelocity_Y, 0);
                boomerangDirection = 4.0f;
                break;
            case "Boomerang2":
                rbBoomerang.velocity = new Vector3(-9.0f - ((float)PlayerPrefs.GetInt("TeppenFloor") / 2.0f), randomBoomerangVelocity_Y, 0);
                boomerangDirection = 4.0f;
                break;
            case "Boomerang3":
                rbBoomerang.velocity = new Vector3(3.0f + ((float)PlayerPrefs.GetInt("TeppenFloor") / 2.0f), randomBoomerangVelocity_Y, 0);
                boomerangDirection = -4.0f;
                break;
            case "Boomerang4":
                rbBoomerang.velocity = new Vector3(6.0f + ((float)PlayerPrefs.GetInt("TeppenFloor") / 2.0f), randomBoomerangVelocity_Y, 0);
                boomerangDirection = -4.0f;
                break;
            case "Boomerang5":
                rbBoomerang.velocity = new Vector3(9.0f + ((float)PlayerPrefs.GetInt("TeppenFloor") / 2.0f), randomBoomerangVelocity_Y, 0);
                boomerangDirection = -4.0f;
                break;
            default:
                break;
        }

        //ブーメランが作られてからの時間計測
        boomerangTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //回転させる
        this.transform.Rotate(new Vector3(0, 30.0f, 0));
        //徐々に向きを変える
        rbBoomerang.velocity = new Vector3(rbBoomerang.velocity.x + (boomerangDirection * Time.deltaTime), rbBoomerang.velocity.y, 0);

        //一定距離画面から離れたら消去する
        if (((this.transform.position.x >= 35.0f || this.transform.position.x <= -35.0f ||
            boomerangTime >= 15.0f) && battleScene_teppenManager.battleFinishFlag == false) ||
            (battleScene_teppenManager.battleFinishFlag == true && battleFinishReplayFlag == false))
        {
            boomerangElapsedTime = timer_teppen.elapsedTime;
            battleFinishReplayFlag = true;
            this.gameObject.SetActive(false);
        }
        else if ((this.transform.position.x >= 35.0f || this.transform.position.x <= -35.0f ||
            replayManager._slide.value >= boomerangElapsedTime) && battleScene_teppenManager.battleFinishFlag == true)
        {
            this.gameObject.SetActive(false);
        }


        //ブーメランが作られてからの時間計測
        boomerangTime += Time.deltaTime;
    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "SlidingPlayer")
        {
            //SEの使用
            //soundManager.SEManager("Airplane_sound1");
        }

        if (other.gameObject.tag != "Obstacle_Airplane")
        {
            return;
        }

        //ぶつかって止まってしまった際は消去する
        if ((rbBoomerang.velocity.x >= -0.3f && rbBoomerang.velocity.x <= 0.3f) && battleScene_teppenManager.battleFinishFlag == false)
        {
            //Destroy(this.gameObject);
            boomerangElapsedTime = timer_teppen.elapsedTime;
            battleFinishReplayFlag = true;
            this.gameObject.SetActive(false);
        }
    }
}

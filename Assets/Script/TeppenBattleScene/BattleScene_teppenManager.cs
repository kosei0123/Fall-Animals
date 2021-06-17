using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene_teppenManager : MonoBehaviour
{
    //CharacterMainMove_offlineのスクリプトを使う
    CharacterMainMove_offline characterMainMove_offline;
    //TitleTapのScriptを使う
    ScreenTouch_offline screenTouch_offline;
    //EndDialog_teppenの関数等を使う
    EndDialog_teppen endDialog_teppen;
    //Timer_teppenのpublic定数を使う
    Timer_teppen timer_teppen;
    //UserAuthのスクリプトの関数使用
    UserAuth userAuth;

    //プレイヤーのオブジェクト
    [HideInInspector]
    public GameObject animal;
    //紙飛行機のオブジェクト
    private GameObject airplane;
    //ブーメランのオブジェクト
    private GameObject boomerang;
    //岩のオブジェクト
    private GameObject rock;
    //コインオブジェクト
    private GameObject coin;
    //トランポリンオブジェクト
    private GameObject trampoline;
    private GameObject trampoline2;

    //紙飛行機の親オブジェクト
    [SerializeField]
    private GameObject AirplaneParent;
    //ブーメランの親オブジェクト
    [SerializeField]
    private GameObject BoomerangParent;
    //岩の親オブジェクト
    [SerializeField]
    private GameObject RockParent;
    //アニマルの親オブジェクト
    [SerializeField]
    public GameObject AnimalParent;
    //トランポリンの親オブジェクト
    [SerializeField]
    public GameObject TrampolineParent;

    //紙飛行機の生成時間(初期値設定)
    private float airplaneCreateTime = 3.0f;
    //ブーメランの生成時間(初期値設定)
    private float boomerangCreateTime = 3.0f;
    //岩の生成時間(初期値設定)
    private float rockCreateTime = 3.0f;
    //コインの生成時間(初期設定)
    private float coinCreateTime = 3.0f;

    //バトル中に取得したコイン
    [HideInInspector]
    public int getBattleCoin = 0;

    //バトルを終了したフラグ
    [HideInInspector]
    public bool battleFinishFlag = false;
    //ぶつかったり落下した際のフラグ
    [HideInInspector]
    public bool damagedFlag = false;

    //キャラクターリストの番号
    [HideInInspector]
    public int characterListNumber = 0;
    //前回キャラのポジション
    [HideInInspector]
    public Vector3 characterChangePosition = new Vector3(-4.5f,10.0f,0);
    //ボタンインターバル
    private float characterChangeInterval;
    //キャラチェンジボタンオブジェクト
    [SerializeField]
    private GameObject CharacterChangeButtonGameObject;



    // Start is called before the first frame update
    void Start()
    {


        //TitleTapのScriptを使う
        screenTouch_offline = GameObject.Find("ScreenTouch").GetComponent<ScreenTouch_offline>();
        //EndDialogの関数等を使う
        endDialog_teppen = GameObject.Find("DialogCanvas").GetComponent<EndDialog_teppen>();
        //Timer_teppenのpublic定数を使う
        timer_teppen = GameObject.Find("TimerCanvas").GetComponent<Timer_teppen>();
        //UserAuthのスクリプトの関数使用
        //userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();

        //ルームに入室後の設定
        CreateCharacter();

        Physics.gravity = new Vector3(0, -11.0f, 0);

        //トランポリンを作る
        if(TeppenShopUI.TrampolineRealFlag == true) CreateTrampoline();

    }

    // Update is called once per frame
    void Update()
    {
        //自分の画面の自キャラのみ操作できるようにする
        if (characterMainMove_offline.offlineflag == false)
        {
            return;
        }

        //紙飛行機を作る
        if (airplaneCreateTime <= 0)
        {
            AirplaneCreated();
            airplaneCreateTime = Random.Range(1, 5);
        }
        //ブーメランを作る
        if (boomerangCreateTime <= 0)
        {
            BoomerangCreated();
            boomerangCreateTime = Random.Range(1, 5);
        }
        //岩を作る
        if (rockCreateTime <= 0)
        {
            RockCreated();
            rockCreateTime = Random.Range(1, 5);
        }
        //コインを作る
        if (coinCreateTime <= 0)
        {
            CoinCreated();
            coinCreateTime = Random.Range(6, 10);
        }
        

        //生成時間を減らす
        //紙飛行機
        airplaneCreateTime -= Time.deltaTime;
        //ブーメラン
        boomerangCreateTime -= Time.deltaTime;
        //岩
        rockCreateTime -= Time.deltaTime;
        //コイン
        coinCreateTime -= Time.deltaTime;

        //キャラクター変更ボタンのインターバル
        characterChangeInterval -= Time.deltaTime;
        //キャラクター変更ボタンの表示/非表示
        if (TeppenShopUI.characterList.Count > 1) CharacterChangeButtonGameObject.SetActive(true);
        else { CharacterChangeButtonGameObject.SetActive(false); }

        //残り時間が0になったら終了
        if (timer_teppen.remainingTime <= 0) Check();

    }

    //Joined Room
    public void CreateCharacter()
    {
        //プレイキャラのオブジェクトを生成
        animal = (GameObject)Instantiate(Resources.Load("Teppen/" + SelectCharacterUI.animalName), new Vector3(characterChangePosition.x, characterChangePosition.y, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
        animal.name = "animal1";
        animal.transform.parent = AnimalParent.transform;

        //Scriptを設定し、フラグを指定する。
        characterMainMove_offline = animal.GetComponent<CharacterMainMove_offline>();
        //ScreenTouch
        //Scriptを設定し、オブジェクトを取得する。
        screenTouch_offline.GetComponent<ScreenTouch_offline>().target = animal;
    }

    //紙飛行機のインスタンス化
    private void AirplaneCreated()
    {
        //ランダム値取得(0 ~ 999)
        int randomAirplane = Random.Range(0, 1000);

        //右に出現
        //ゆっくり
        if (randomAirplane >= 0 && randomAirplane < 100)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Teppen/Airplane"), new Vector3(30.0f, 4.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane0";
            //子オブジェクトに指定する
            airplane.transform.parent = AirplaneParent.transform;
        }
        //普通
        else if (randomAirplane >= 100 && randomAirplane < 200)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Teppen/Airplane"), new Vector3(30.0f, 4.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane1";
            airplane.transform.parent = AirplaneParent.transform;
        }
        ////高速
        else if (randomAirplane >= 200 && randomAirplane < 300 && PlayerPrefs.GetInt("TeppenFloor") >= 10)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Teppen/Airplane"), new Vector3(30.0f, 5.0f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane2";
            airplane.transform.parent = AirplaneParent.transform;
        }
        //左に出現
        //ゆっくり
        else if (randomAirplane >= 300 && randomAirplane < 400)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Teppen/Airplane"), new Vector3(-30.0f, 4.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane3";
            airplane.transform.parent = AirplaneParent.transform;
        }
        //普通
        else if (randomAirplane >= 400 && randomAirplane < 500)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Teppen/Airplane"), new Vector3(-30.0f, 4.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane4";
            airplane.transform.parent = AirplaneParent.transform;
        }
        ////高速
        else if (randomAirplane >= 500 && randomAirplane < 600 && PlayerPrefs.GetInt("TeppenFloor") >= 10)
        {
            airplane = (GameObject)Instantiate(Resources.Load("Teppen/Airplane"), new Vector3(-30.0f, 5.0f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
            airplane.name = "Airplane5";
            airplane.transform.parent = AirplaneParent.transform;
        }
    }

    //ブーメランのインスタンス化
    private void BoomerangCreated()
    {
        //ランダム値取得(0 ~ 999)
        int randomBoomerang = Random.Range(0, 1000);

        //右に出現
        //ゆっくり
        if (randomBoomerang >= 0 && randomBoomerang < 100 && PlayerPrefs.GetInt("TeppenFloor") >= 20)
        {
            boomerang = (GameObject)Instantiate(Resources.Load("Teppen/Boomerang"), new Vector3(30.0f, 6.0f, 0), Quaternion.Euler(-30.0f, 0.0f, 0.0f));
            boomerang.name = "Boomerang0";
            //子オブジェクトに指定する
            boomerang.transform.parent = BoomerangParent.transform;
        }
        //普通
        //else if (randomBoomerang >= 100 && randomBoomerang < 200)
        //{
        //    boomerang = (GameObject)Instantiate(Resources.Load("Teppen/Boomerang"), new Vector3(30.0f, 6.0f, 0), Quaternion.Euler(-30.0f, 0.0f, 0.0f));
        //    boomerang.name = "Boomerang1";
        //    boomerang.transform.parent = BoomerangParent.transform;
        //}
        //高速
        else if (randomBoomerang >= 200 && randomBoomerang < 300 && PlayerPrefs.GetInt("TeppenFloor") >= 25)
        {
            boomerang = (GameObject)Instantiate(Resources.Load("Teppen/Boomerang"), new Vector3(30.0f, 6.0f, 0), Quaternion.Euler(-30.0f, 0.0f, 0.0f));
            boomerang.name = "Boomerang2";
            boomerang.transform.parent = BoomerangParent.transform;
        }
        //左に出現
        //ゆっくり
        else if (randomBoomerang >= 300 && randomBoomerang < 400 && PlayerPrefs.GetInt("TeppenFloor") >= 20)
        {
            boomerang = (GameObject)Instantiate(Resources.Load("Teppen/Boomerang"), new Vector3(-30.0f, 6.0f, 0), Quaternion.Euler(-30.0f, 0.0f, 0.0f));
            boomerang.name = "Boomerang3";
            boomerang.transform.parent = BoomerangParent.transform;
        }
        //普通
        //else if (randomBoomerang >= 400 && randomBoomerang < 500)
        //{
        //    boomerang = (GameObject)Instantiate(Resources.Load("Teppen/Boomerang"), new Vector3(-30.0f, 6.0f, 0), Quaternion.Euler(-30.0f, 0.0f, 0.0f));
        //    boomerang.name = "Boomerang4";
        //    boomerang.transform.parent = BoomerangParent.transform;
        //}
        //高速
        else if (randomBoomerang >= 500 && randomBoomerang < 600 && PlayerPrefs.GetInt("TeppenFloor") >= 25)
        {
            boomerang = (GameObject)Instantiate(Resources.Load("Teppen/Boomerang"), new Vector3(-30.0f, 6.0f, 0), Quaternion.Euler(-30.0f, 0.0f, 0.0f));
            boomerang.name = "Boomerang5";
            boomerang.transform.parent = BoomerangParent.transform;
        }
    }

    //岩のインスタンス化
    private void RockCreated()
    {
        //ランダム値取得(0 ~ 999)
        int randomrock = Random.Range(0, 1000);

        //右に出現
        //ゆっくり
        if (randomrock >= 0 && randomrock < 100)
        {
            rock = (GameObject)Instantiate(Resources.Load("Teppen/Rock"), new Vector3(25.0f, 2.5f, 0), Quaternion.identity);
            rock.name = "Rock0";
            //子オブジェクトに指定する
            rock.transform.parent = RockParent.transform;
        }
        ////普通
        else if (randomrock >= 100 && randomrock < 200 && PlayerPrefs.GetInt("TeppenFloor") >= 30)
        {
            rock = (GameObject)Instantiate(Resources.Load("Teppen/Rock"), new Vector3(25.0f, 3.0f, 0), Quaternion.identity);
            rock.name = "Rock1";
            rock.transform.parent = RockParent.transform;
        }
        //高速
        else if (randomrock >= 200 && randomrock < 300)
        {
            rock = (GameObject)Instantiate(Resources.Load("Teppen/Rock"), new Vector3(25.0f, 3.0f, 0), Quaternion.identity);
            rock.name = "Rock2";
            rock.transform.parent = RockParent.transform;
        }
        //左に出現
        //ゆっくり
        else if (randomrock >= 300 && randomrock < 400)
        {
            rock = (GameObject)Instantiate(Resources.Load("Teppen/Rock"), new Vector3(-25.0f, 2.5f, 0), Quaternion.identity);
            rock.name = "Rock3";
            rock.transform.parent = RockParent.transform;
        }
        ////普通
        else if (randomrock >= 400 && randomrock < 500 && PlayerPrefs.GetInt("TeppenFloor") >= 30)
        {
            rock = (GameObject)Instantiate(Resources.Load("Teppen/Rock"), new Vector3(-25.0f, 3.0f, 0), Quaternion.identity);
            rock.name = "Rock4";
            rock.transform.parent = RockParent.transform;
        }
        //高速
        else if (randomrock >= 500 && randomrock < 600)
        {
            rock = (GameObject)Instantiate(Resources.Load("Teppen/Rock"), new Vector3(-25.0f, 3.0f, 0), Quaternion.identity);
            rock.name = "Rock5";
            rock.transform.parent = RockParent.transform;
        }
    }

    //コインのインスタンス化
    private void CoinCreated()
    {
        float randomCoin = Random.Range(-8.0f, 8.0f);
        coin = (GameObject)Instantiate(Resources.Load("Offline/Coin"), new Vector3(randomCoin, 20.0f, 0), Quaternion.identity);
    }

    //トランポリンのインスタンス化
    private void CreateTrampoline()
    {
        //トランポリンのオブジェクトを生成
        trampoline = (GameObject)Instantiate(Resources.Load("Teppen/Trampoline"), new Vector3(-20.0f, 10.0f, 0), Quaternion.identity);
        trampoline.transform.parent = TrampolineParent.transform;
        trampoline2 = (GameObject)Instantiate(Resources.Load("Teppen/Trampoline"), new Vector3(20.0f, 10.0f, 0), Quaternion.identity);
        trampoline2.transform.parent = TrampolineParent.transform;
    }

    //勝敗のチェック
    private void Check()
    {
        //動きを止める
        characterMainMove_offline.offlineflag = false;
        characterMainMove_offline.rb.velocity = new Vector3(0, characterMainMove_offline.rb.velocity.y, 0);
        battleFinishFlag = true;
        
        //現在回数を追加する
        PlayerPrefs.SetInt("TeppenFloor", PlayerPrefs.GetInt("TeppenFloor") + 1);
        

        //終了時のダイアログ表示
        endDialog_teppen.DialogPanelActive(PlayerPrefs.GetInt("TeppenFloor"));
    }

    //キャラクター変更ボタン押下時
    public void OnClick_CharacterChangeButton()
    {
        if (characterChangeInterval >= 0)
        {
            return;
        }

        //ボタン押下のインターバル
        characterChangeInterval = 0.5f;
        //削除前ポジション取得
        characterChangePosition = characterMainMove_offline.gameObject.transform.position;
        //アニマルの削除
        foreach (Transform childTransform in AnimalParent.transform) Destroy(childTransform.gameObject);
        //キャラクター変更
        if (characterListNumber < TeppenShopUI.characterList.Count - 1) characterListNumber++;
        else { characterListNumber = 0; }
        SelectCharacterUI.animalName = TeppenShopUI.characterList[characterListNumber];
        //キャラクターカラー
        SelectCharacterUI.animalName_Color = SelectCharacterUI.animalName + PlayerPrefs.GetString("TeppenAnimalColor").Substring(PlayerPrefs.GetString("TeppenAnimalColor").IndexOf("("));
        //キャラクター作成
        CreateCharacter();
    }

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
        }
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
    }


    //順位表示処理
    private void OnGUI()
    {
        //GUI.TextField(new Rect(150, 30, 150, 70), "残り人数 : " + (int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"]);
    }
}

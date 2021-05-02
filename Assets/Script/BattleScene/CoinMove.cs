using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CoinMove : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;

    //コインの破壊
    private bool coinDestroyFlag = false;
    private float coinDestroyTime = 0;

    //コインのゲームオブジェクト
    [SerializeField]
    private GameObject Coin;
    //コインテキストのゲームオブジェクト
    [SerializeField]
    private GameObject CoinTextGameobject;
    [SerializeField]
    private Text CoinText;

    //メッセージの送信に使用される
    PhotonView coinPhotonView;

    //移譲を1度のみにする
    private bool NoMasterCliantFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();

        //メッセージの送信に使用される
        coinPhotonView = PhotonView.Get(this);

        //オーナーの所有権を別オーナーに移譲するようにする
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] == true && NoMasterCliantFlag == true)
        {
            coinPhotonView.RequestOwnership();
            //1度のみ実行
            NoMasterCliantFlag = false;
        }
        else if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] == false)
        {
            NoMasterCliantFlag = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //オーナーの所有権を別オーナーに移譲するようにする
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] == true && NoMasterCliantFlag == true)
        {
            coinPhotonView.RequestOwnership();
            //1度のみ実行
            NoMasterCliantFlag = false;
        }
        else if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["NoMasterCliant"] == false)
        {
            NoMasterCliantFlag = true;
        }

        //テキストの位置
        CoinText.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);

        //一定距離画面から離れたら消去する
        if (this.transform.position.y < -50.0f)
        {
            //マスタークライアントが削除する
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

        //削除まで時間をおく
        if (coinDestroyFlag == true)
        {
            coinDestroyTime += Time.deltaTime;
        }

        //時間経過後マスタークライアントが削除する
        if (coinDestroyTime >= 1.0f)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "SlidingPlayer")
        {
            //コイン破壊のフラグをtrueにする
            coinDestroyFlag = true;
            //コインを見えなくする
            Coin.SetActive(false);
            //コインテキストを表示する
            CoinTextGameobject.SetActive(true);
            //SEの使用
            soundManager.SEManager("CoinGet_sound1");
        }

        
    }
}

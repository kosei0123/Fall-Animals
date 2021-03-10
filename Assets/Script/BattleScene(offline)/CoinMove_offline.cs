using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinMove_offline : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //テキストの位置
        CoinText.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);

        if (coinDestroyFlag == true)
        {
            coinDestroyTime += Time.deltaTime;
        }

        //時間経過後マスタークライアントが削除する
        if (coinDestroyTime >= 1.0f)
        {
            Destroy(this.gameObject);
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

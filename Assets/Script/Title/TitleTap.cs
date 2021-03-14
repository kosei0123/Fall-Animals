using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class TitleTap : MonoBehaviour
{
    //TitleManagerのpublic定数を取得
    TitleManager titleManager;
    //AdMobTitleAdvertinsingのpublic定数を取得
    AdMobTitleAdvertising adMobTitleAdvertinsing;
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;
    //UserAuthのスクリプトの関数使用
    UserAuth userAuth;

    //TapTextオブジェクトを指定する
    [SerializeField]
    private Text TapText;

    // Start is called before the first frame update
    void Start()
    {
        //TitleManagerのpublic定数を取得
        titleManager = GameObject.Find("TitleManager").GetComponent<TitleManager>();
        //AdMobTitleAdvertinsingのpublic定数を取得
        adMobTitleAdvertinsing = GameObject.Find("TitleAdvertising").GetComponent<AdMobTitleAdvertising>();
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //UserAuthのスクリプトの関数使用
        userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();

        //mobile backendに接続し、ログアウトする
        userAuth.logOut();


    }

    // Update is called once per frame
    void Update()
    {
        //テキストの点滅表示を行う
        TapText.color = GetAlphaColor(TapText.color);

        //画面のどこかをタップした際の動作
        if (Input.GetMouseButtonUp(0))
        {
            //オブジェクトの消去
            //Destroy(titleManager.titleAnimal);
            //Destroy(titleManager.titleRock);
            adMobTitleAdvertinsing.bannerView.Hide();
            adMobTitleAdvertinsing.bannerView.Destroy();
            //SEの使用
            soundManager.SEManager("Title_sound1");

            //PlayerPrefs.DeleteKey("NickName");
            //デバイスにニックネームが保持されているか確認
            if (!PlayerPrefs.HasKey("NickName"))
            {
                //ニックネームが登録されていない場合
                //画面遷移
                SceneManager.LoadScene("SelectPlayerName");
            }
            else
            {
                //ニックネームが登録されている場合
                //ログイン
                FindObjectOfType<UserAuth>().login(PlayerPrefs.GetString("NickName"));
                //画面遷移
                SceneManager.LoadScene("Menu");
            }
        }
    }

    //テキストの点滅表示を行う関数
    private Color GetAlphaColor(Color color)
    {
        //α値を0~1で取得する
        color.a = (Mathf.Sin(Time.time * 2) / 2) + 0.5f;

        return color;
    }
}

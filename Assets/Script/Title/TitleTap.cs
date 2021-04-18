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
    

    //NewsPanelを表示する
    [SerializeField]
    private GameObject NewsPanel;
    //TermsOfServicePanelを表示する
    [SerializeField]
    private GameObject TermsOfServicePanel;
    //CreditPanelを表示する
    [SerializeField]
    private GameObject CreditPanel;
    //DeleteDataPanelを表示する
    [SerializeField]
    private GameObject DeleteDataPanel;

    //ボタンを押した後のインターバル
    private float TitleNextInterval = 0;
    //タイトル画面のオンライン状態確認
    private bool titleOnlineFlag = false;

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

        //デバイスにニックネームが保持されているか確認しログインする
        if (PlayerPrefs.HasKey("NickName")) FindObjectOfType<UserAuth>().login(PlayerPrefs.GetString("NickName"));

        //オンラインかの確認
        if (Application.internetReachability != NetworkReachability.NotReachable) titleOnlineFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        //テキストの点滅表示を行う
        TapText.color = GetAlphaColor(TapText.color);
        //ボタン押下後設定したインターバルを減らしていく
        TitleNextInterval -= Time.deltaTime;

        //画面のどこかをタップした際の動作
        //if (Input.GetMouseButtonUp(0))
        //{
        //    //Rayを発射
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit2D hit2D = Physics2D.Raycast((Vector2)Input.mousePosition, (Vector2)ray.direction);

        //    //ボタン押下でなければ、スクリーンタッチ時のイベント
        //    if (!hit2D && TitleNextInterval <= 0)
        //    {
        //        ScreenEvent();
        //    }
        //}
    }

    //テキストの点滅表示を行う関数
    private Color GetAlphaColor(Color color)
    {
        //α値を0~1で取得する
        color.a = (Mathf.Sin(Time.time * 2) / 2) + 0.5f;

        return color;
    }

    //スクリーンボタン時の挙動
    public void OnClick_ScreenEventButton()
    {
        //オブジェクトの消去
        //Destroy(titleManager.titleAnimal);
        //Destroy(titleManager.titleRock);

        //広告解除していない場合(オンライン)
        if (PlayerPrefs.GetInt("Unlock_TitleAdvertising") == 0 && adMobTitleAdvertinsing.bannerView != null)
        {
            adMobTitleAdvertinsing.bannerView.Hide();
            adMobTitleAdvertinsing.bannerView.Destroy();
        }


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
            //画面遷移
            SceneManager.LoadScene("Menu");
        }
    }

    //NewsButton押下時
    public void OnClick_NewsButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //NewsPanelを表示する
        NewsPanel.SetActive(true);
    }

    //NewsCloseButton押下時
    public void OnClick_NewsCloseButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //NewsPanelを非表示にする
        NewsPanel.SetActive(false);
        //すぐに次のシーンに行かないようにする
        TitleNextInterval = 0.3f;
    }

    //TermsOfServiceButton押下時
    public void OnClick_PrivacyPolicyButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //TermsOfServicePanelを表示する
        TermsOfServicePanel.SetActive(true);
    }

    //TermsOfServiceCloseButton押下時
    public void OnClick_PrivacyPolicyCloseButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //TermsOfServicePanelを非表示にする
        TermsOfServicePanel.SetActive(false);
        //すぐに次のシーンに行かないようにする
        TitleNextInterval = 0.3f;
    }

    //CreditButton押下時
    public void OnClick_CreditButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //CreditPanelを表示する
        CreditPanel.SetActive(true);
    }

    //CreditCloseButton押下時
    public void OnClick_CreditCloseButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //CreditPanelを非表示にする
        CreditPanel.SetActive(false);
        //すぐに次のシーンに行かないようにする
        TitleNextInterval = 0.3f;
    }

    //DeleteDataButton押下時
    public void OnClick_DeleteDataButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //DeleteDataPanelを非表示にする
        DeleteDataPanel.SetActive(true);
    }

    //DeleteDataCloseButton押下時
    public void OnClick_DeleteDataCloseButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //DeleteDataPanelを非表示にする
        DeleteDataPanel.SetActive(false);
    }

}

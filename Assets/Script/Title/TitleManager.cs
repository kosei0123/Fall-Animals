using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    //ドキュメントの表示
    [SerializeField]
    private Text newsText = default;
    [SerializeField]
    private TextAsset newsDocument = default;
    [SerializeField]
    private Text policyText = default;
    [SerializeField]
    private TextAsset policyDocument = default;
    [SerializeField]
    private Text creditText = default;
    [SerializeField]
    private TextAsset creditDocument = default;

    //ボタン
    [SerializeField]
    private Button DeleteDataButton;


    // Start is called before the first frame update
    void Start()
    {
        //FPSを60に設定
        Application.targetFrameRate = 60;

        newsText.text = newsDocument.text;
        policyText.text = policyDocument.text;
        creditText.text = creditDocument.text;

        //ニックネーム登録がまだであればボタン非表示
        if (!PlayerPrefs.HasKey("NickName")) DeleteDataButton.interactable = false;
        else { DeleteDataButton.interactable = true; }
    }

    // Update is called once per frame
    void Update()
    {
        //スプラッシュ画面が閉じた後での動作(岩オブジェクトの生成)
        if (UnityEngine.Rendering.SplashScreen.isFinished)
        {
            //画面の回転可能にする
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToLandscapeLeft = true;
        }
    }
}

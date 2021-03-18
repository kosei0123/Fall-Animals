using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCharacterUI : MonoBehaviour
{
    //SoundManagerスクリプトの関数使用
    SoundManager soundManager;
    //UserAuthのスクリプトの関数使用
    UserAuth userAuth;

    //Buttonのコンポーネントを取得
    [SerializeField]
    private Button SelectCharacterOKButton;
    //選択キャラの名前表示
    [SerializeField]
    private Text SelectCharacterText;
    //ベストタイムスコア表示
    [SerializeField]
    private Text SelectCharacterBestTimeText;

    //キリンパネルの表示
    [SerializeField]
    private GameObject GiraffePanel;
    //象パネルの表示
    [SerializeField]
    private GameObject ElephantPanel;
    //虎パネルの表示
    [SerializeField]
    private GameObject TigerPanel;

    //プレイキャラの名前取得
    public static string animalName;
    //プレイキャラの名前取得(仮)
    private static string animalName_Temporary = "Dog";

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //UserAuthのスクリプトの関数使用
        userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //キャラ選択できていない場合はOKボタンを押せないようにする
        if (animalName == null)
        {
            //ボタン選択不可
            SelectCharacterOKButton.interactable = false;
        }
        else
        {
            //ボタン選択可
            SelectCharacterOKButton.interactable = true;
            //選択キャラの名前表示
            SelectCharacterText.text = animalName_Temporary;
            //ベストタイムスコアを表示
            SelectCharacterBestTimeText.text = "ベストタイム : " + PlayerPrefs.GetInt("BestTime_" + animalName_Temporary).ToString("") + " 秒";
        }


        //アンロックされたキャラクターを表示する
        CheckUnlock();

    }

    

    //アンロックされたキャラクターを表示する
    private void CheckUnlock()
    {
        //キリンパネル
        if (PlayerPrefs.GetInt("Unlock_Giraffe") == 1)
        {
            GiraffePanel.SetActive(true);
        }
        //象パネル
        if (PlayerPrefs.GetInt("Unlock_Elephant") == 1)
        {
            ElephantPanel.SetActive(true);
        }
        //虎パネル
        if (PlayerPrefs.GetInt("Unlock_Tiger") == 1)
        {
            TigerPanel.SetActive(true);
        }
    }

    //Giraffeボタン押下した際の処理
    public void OnClick_GiraffeButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //プレイキャラの名前取得
        animalName_Temporary = "Giraffe";
    }

    //Elephantボタン押下した際の処理
    public void OnClick_ElephantButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //プレイキャラの名前取得
        animalName_Temporary = "Elephant";
    }

    //Dogボタン押下した際の処理
    public void OnClick_DogButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //プレイキャラの名前取得
        animalName_Temporary = "Dog";
    }

    //Tigerボタン押下した際の処理
    public void OnClick_TigerButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //プレイキャラの名前取得
        animalName_Temporary = "Tiger";
    }

    //Unityちゃんボタン押下した際の処理
    //public void OnClick_UnityChanButton()
    //{
    //    //SEの使用
    //    soundManager.SEManager("CharacterSelect_sound1");
    //    //プレイキャラの名前取得
    //    animalName = "animal1";
    //}

    //キャラ選択後に画面遷移を行う
    public void OnClick_SelectCharacterOKButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //仮で入れていた動物の名前を正式に入力する
        animalName = animalName_Temporary;
        //画面遷移
        SceneManager.LoadScene("Menu");
    }

    //メニューボタン押下した際の挙動
    public void OnClick_MenuButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        SceneManager.LoadScene("Menu");
    }

    //アプリケーション一時停止時
    private void OnApplicationPause(bool pause)
    {
    }

    //アプリケーション終了時
    private void OnApplicationQuit()
    {
    }

}

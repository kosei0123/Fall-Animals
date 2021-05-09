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
    //SelectSkinsのスクリプトの関数使用
    SelectSkins selectSkins;

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
    //猫パネルの表示
    [SerializeField]
    private GameObject CatPanel;
    //ウサギのパネル表示
    [SerializeField]
    private GameObject RabbitPanel;

    //プレイキャラの名前取得
    public static string animalName;
    //プレイキャラの名前取得(仮)
    private static string animalName_Temporary = "Dog";
    //プレイキャラのカラー取得
    public static string animalName_Color;
    //プレイキャラのカラー取得(仮)
    public static string animalName_Color_Temporary = "Dog(ノーマル)";

    // Start is called before the first frame update
    void Start()
    {
        //SoundManagerのスクリプトの関数使用
        soundManager = GameObject.Find("Sound").GetComponent<SoundManager>();
        //UserAuthのスクリプトの関数使用
        userAuth = GameObject.Find("NCMBSettings").GetComponent<UserAuth>();
        //SelectSkinsのスクリプトの関数使用
        selectSkins = this.gameObject.GetComponent<SelectSkins>();

        //仮の名前を初期化
        animalName_Temporary = animalName;
        animalName_Color_Temporary = animalName_Color;

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
            SelectCharacterText.text = animalName_Color_Temporary;
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
        //猫パネル
        if (PlayerPrefs.GetInt("Unlock_Cat") == 1)
        {
            CatPanel.SetActive(true);
        }
        //猫パネル
        if (PlayerPrefs.GetInt("Unlock_Rabbit") == 1)
        {
            RabbitPanel.SetActive(true);
        }
    }

    //キャラクターカラーの表示
    private void SelectColors_Display(string animalName, bool normal)
    {
        //初期値
        if (normal == true)
        {
            animalName_Color_Temporary = animalName + "(N)";
            return;
        }
        //カラーを変更する
        if (animalName_Color_Temporary == animalName + "(N)")
        {
            animalName_Color_Temporary = animalName + "(W)";
        }
        else if (animalName_Color_Temporary == animalName + "(W)")
        {
            animalName_Color_Temporary = animalName + "(G)";
        }
        else if (animalName_Color_Temporary == animalName + "(G)")
        {
            animalName_Color_Temporary = animalName + "(N)";
        }
    }

    //Giraffeボタン押下した際の処理
    public void OnClick_GiraffeButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //カラー変更
        if(animalName_Temporary != "Giraffe") SelectColors_Display("Giraffe", true);
        else { SelectColors_Display("Giraffe", false); }
        //プレイキャラの名前取得
        animalName_Temporary = "Giraffe";
        
    }

    //Elephantボタン押下した際の処理
    public void OnClick_ElephantButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //カラー変更
        if (animalName_Temporary != "Elephant") SelectColors_Display("Elephant", true);
        else { SelectColors_Display("Elephant", false); }
        //プレイキャラの名前取得
        animalName_Temporary = "Elephant";
    }

    //Dogボタン押下した際の処理
    public void OnClick_DogButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //カラー変更
        if (animalName_Temporary != "Dog") SelectColors_Display("Dog", true);
        else { SelectColors_Display("Dog", false); }
        //プレイキャラの名前取得
        animalName_Temporary = "Dog";
    }

    //Tigerボタン押下した際の処理
    public void OnClick_TigerButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //カラー変更
        if (animalName_Temporary != "Tiger") SelectColors_Display("Tiger", true);
        else { SelectColors_Display("Tiger", false); }
        //プレイキャラの名前取得
        animalName_Temporary = "Tiger";
    }

    //Catボタン押下した際の処理
    public void OnClick_CatButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //カラー変更
        if (animalName_Temporary != "Cat") SelectColors_Display("Cat", true);
        else { SelectColors_Display("Cat", false); }
        //プレイキャラの名前取得
        animalName_Temporary = "Cat";
    }

    //Rabbitボタン押下した際の処理
    public void OnClick_RabbitButton()
    {
        //SEの使用
        soundManager.SEManager("CharacterSelect_sound1");
        //カラー変更
        if (animalName_Temporary != "Rabbit") SelectColors_Display("Rabbit", true);
        else { SelectColors_Display("Rabbit", false); }
        //プレイキャラの名前取得
        animalName_Temporary = "Rabbit";
    }

    //キャラ選択後に画面遷移を行う
    public void OnClick_SelectCharacterOKButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //仮で入れていた動物の名前を正式に入力する
        animalName = animalName_Temporary;
        //仮で入れていたスキンを正式に入力する
        SelectSkins.skinsName = selectSkins.skinsName_Temporary;
        //仮で入れていた動物のカラーを正式に入力する
        animalName_Color = animalName_Color_Temporary;
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

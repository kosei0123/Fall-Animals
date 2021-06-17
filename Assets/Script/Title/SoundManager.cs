using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    //シングルトンにて1度のみ作成
    private static SoundManager soundManager_instance;

    //BGM
    //タイトルBGMのAudioSource
    [SerializeField]
    private AudioSource BGM_Title;
    //メニューBGMのAudioSource
    [SerializeField]
    private AudioSource BGM_Menu;
    //バトルBGMのAudioSource
    [SerializeField]
    private AudioSource BGM_Battle;
    //バトルBGM(洞窟)のAudioSource
    [SerializeField]
    private AudioSource BGM_Battle_Cave;
    //バトルBGM(夜道)のAudioSource
    [SerializeField]
    private AudioSource BGM_Battle_NightStreet;
    //テッペンメニューのAudioSource
    [SerializeField]
    private AudioSource BGM_TeppenMenu;
    //テッペンショップのAudioSource
    [SerializeField]
    private AudioSource BGM_TeppenShop;

    //SE使用
    AudioSource audioSource;
    [SerializeField]
    private AudioClip Title_sound1;
    [SerializeField]
    private AudioClip Button_sound1;
    [SerializeField]
    private AudioClip CharacterSelect_sound1;
    [SerializeField]
    private AudioClip CoinGet_sound1;
    [SerializeField]
    private AudioClip Rock_sound1;
    [SerializeField]
    private AudioClip Airplane_sound1;
    [SerializeField]
    private AudioClip CatNakigoe_sound1;

    //1つ前のシーン
    private string beforeScene = "Title";

    private void Awake()
    {
        //SE使用
        audioSource = this.GetComponent<AudioSource>();

        //オブジェクトが作成され、一度のみ永久に破壊されない
        if (soundManager_instance == null)
        {
            soundManager_instance = this;
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(BGM_Title);
            DontDestroyOnLoad(BGM_Menu);
            DontDestroyOnLoad(BGM_Battle);
            DontDestroyOnLoad(BGM_Battle_Cave);
            DontDestroyOnLoad(BGM_TeppenMenu);
            DontDestroyOnLoad(BGM_TeppenShop);
        }
        else
        {
            Destroy(this.gameObject);
            Destroy(BGM_Title);
            Destroy(BGM_Menu);
            Destroy(BGM_Battle);
            Destroy(BGM_Battle_Cave);
            Destroy(BGM_TeppenMenu);
            Destroy(BGM_TeppenShop);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //シーンが切り替わったときに呼ばれるメソッドを登録
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    //シーンが切り替わったときに呼ばれるメソッド
    private void OnActiveSceneChanged(Scene prevScene, Scene currentScene)
    {
        //タイトルからメニューまたはプレイヤーネーム選択画面への遷移
        if (beforeScene == "Title" && (currentScene.name == "Menu" || currentScene.name == "SelectPlayerName"))
        {
            BGM_Title.Stop();
            BGM_Menu.Play();
        }

        //バトルシーンへの遷移
        if ((beforeScene == "WaitingRoom" && currentScene.name == "BattleScene")
            || (beforeScene == "WaitingRoom(offline)" && currentScene.name == "BattleScene(offline)"))
        {
            BGM_Menu.Stop();
        }

        //メニューからテッペンメニューへの遷移
        if (beforeScene == "Menu" && currentScene.name == "TeppenMenu")
        {
            BGM_Menu.Stop();
            BGM_TeppenMenu.Play();
        }

        //テッペンメニューからテッペンバトルシーンへの遷移
        if (beforeScene == "TeppenMenu" && currentScene.name == "TeppenBattleScene")
        {
            BGM_TeppenShop.Stop();
        }

        //テッペンバトルシーンからテッペンメニューへの遷移
        if (beforeScene == "TeppenBattleScene" && currentScene.name == "TeppenMenu")
        {
            BGM_Battle.Stop();
            BGM_Battle_Cave.Stop();
            BGM_Battle_NightStreet.Stop();
            BGM_TeppenMenu.Play();
        }

        //テッペンメニューからメニューへの遷移
        if ((beforeScene == "TeppenMenu" || beforeScene == "TeppenBattleScene") && currentScene.name == "Menu")
        {
            BGM_TeppenMenu.Stop();
            BGM_TeppenShop.Stop();
            BGM_Menu.Play();
        }

        //バトルシーンからメニューへの遷移
        if ((beforeScene == "BattleScene" || beforeScene == "BattleScene(offline)") && currentScene.name == "Menu")
        {
            BGM_Battle.Stop();
            BGM_Battle_Cave.Stop();
            BGM_Battle_NightStreet.Stop();
            BGM_Menu.Play();
        }



        //WaitingRoomへの遷移
        if (beforeScene == "BattleScene(offline)" && currentScene.name == "WaitingRoom")
        {
            BGM_Battle.Stop();
            BGM_Battle_Cave.Stop();
            BGM_Battle_NightStreet.Stop();
            BGM_Menu.Play();
        }
        else if((beforeScene == "TeppenMenu" || beforeScene == "TeppenBattleScene") && currentScene.name == "WaitingRoom")
        {

            BGM_TeppenMenu.Stop();
            BGM_TeppenShop.Stop();
            BGM_Battle.Stop();
            BGM_Battle_Cave.Stop();
            BGM_Battle_NightStreet.Stop();
            BGM_Menu.Play();
        }

        //遷移後の現在のシーンを一つ前のシーンとして保持
        beforeScene = currentScene.name;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SEManager(string i)
    {
        switch (i)
        {
            case "Title_sound1":
                audioSource.PlayOneShot(Title_sound1);
                break;
            case "Button_sound1":
                audioSource.PlayOneShot(Button_sound1);
                break;
            case "CharacterSelect_sound1":
                audioSource.PlayOneShot(CharacterSelect_sound1);
                break;
            case "CoinGet_sound1":
                audioSource.PlayOneShot(CoinGet_sound1);
                break;
            case "Rock_sound1":
                audioSource.PlayOneShot(Rock_sound1);
                break;
            case "Airplane_sound1":
                audioSource.PlayOneShot(Airplane_sound1);
                break;
            case "CatNakigoe_sound1":
                audioSource.PlayOneShot(CatNakigoe_sound1);
                break;
            default:
                break;
        }
    }

    public void BGMManager(string i)
    {
        switch (i)
        {
            case "BGM_TeppenShop":
                BGM_TeppenMenu.Stop();
                BGM_TeppenShop.Play();
                break;
            case "BGM_Battle":
                BGM_Battle.Play();
                break;
            case "BGM_Battle_Cave":
                BGM_Battle_Cave.Play();
                break;
            case "BGM_Battle_NightStreet":
                BGM_Battle_NightStreet.Play();
                break;
            default:
                break;
        }
    }
}

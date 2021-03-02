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

    //SE使用
    AudioSource audioSource;
    [SerializeField]
    private AudioClip Title_sound1;
    [SerializeField]
    private AudioClip Button_sound1;
    [SerializeField]
    private AudioClip CharacterSelect_sound1;

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
        }
        else
        {
            Destroy(this.gameObject);
            Destroy(BGM_Title);
            Destroy(BGM_Menu);
            Destroy(BGM_Battle);
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

        //WaitingRoomからバトルシーンへの遷移
        if (beforeScene == "WaitingRoom" && currentScene.name == "BattleScene")
        {
            BGM_Menu.Stop();
            BGM_Battle.Play();
        }

        //バトルシーンからメニューへの遷移
        if (beforeScene == "BattleScene" && currentScene.name == "Menu")
        {
            BGM_Battle.Stop();
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
            default:
                break;
        }
    }
}

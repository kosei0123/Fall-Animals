using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //シングルトンにて1度のみ作成
    private static SoundManager soundManager_instance;

    //SE使用
    AudioSource audioSource;
    [SerializeField]
    private AudioClip Title_sound1;
    [SerializeField]
    private AudioClip Button_sound1;
    [SerializeField]
    private AudioClip CharacterSelect_sound1;

    private void Awake()
    {
        //SE使用
        audioSource = this.GetComponent<AudioSource>();

        if(soundManager_instance == null)
        {
            soundManager_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
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

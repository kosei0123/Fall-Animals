using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteData : MonoBehaviour
{
    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;
    //UserAuthのスクリプトの関数使用
    UserAuth userAuth;

    //DeleteDataCheckPanel表示
    [SerializeField]
    private GameObject DeleteDataCheckPanel;

    //
    private string deleteName;

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
        
    }

    //DeleteUnlockButton押下時
    public void OnClick_DeleteUnlockButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //DeleteDataCheckPanelを表示する
        DeleteDataCheckPanel.SetActive(true);
        //アンロックを指定
        deleteName = "DeleteUnlock";
    }

    //DeleteCoinButton押下時
    public void OnClick_DeleteCoinButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //DeleteDataCheckPanelを表示する
        DeleteDataCheckPanel.SetActive(true);
        //コインを指定
        deleteName = "DeleteCoin";
    }

    //DeleteBestTimeButton押下時
    public void OnClick_DeleteBestTimeButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //DeleteDataCheckPanelを表示する
        DeleteDataCheckPanel.SetActive(true);
        //ベストタイムを指定
        deleteName = "DeleteBestTime";
    }

    //DeleteDataCheckYesButton押下時
    public void OnClick_DeleteDataCheckYesButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //DeleteDataCheckPanelを非表示する
        DeleteDataCheckPanel.SetActive(false);

        switch (deleteName)
        {
            case "DeleteUnlock":
                //アニマル
                PlayerPrefs.SetInt("Unlock_Giraffe", 0);
                PlayerPrefs.SetInt("Unlock_Elephant", 0);
                PlayerPrefs.SetInt("Unlock_Tiger", 0);
                //スキン
                PlayerPrefs.SetInt("Unlock_Candy", 0);
                PlayerPrefs.SetInt("Unlock_Crown", 0);
                //広告
                PlayerPrefs.SetInt("Unlock_TitleAdvertising", 0);
                PlayerPrefs.SetInt("Unlock_WaitingRoomAdvertising", 0);
                PlayerPrefs.SetInt("Unlock_WaitingRoomAdvertising_offline", 0);
                break;
            case "DeleteCoin":
                PlayerPrefs.SetInt("myCoin", 0);
                break;
            case "DeleteBestTime":
                userAuth.firstSetBestTime(true);
                break;
            default:
                break;
        }
    }

    //DeleteDataCheckNoButton押下時
    public void OnClick_DeleteDataCheckNoButton()
    {
        //SEの使用
        soundManager.SEManager("Button_sound1");
        //DeleteDataCheckPanelを非表示する
        DeleteDataCheckPanel.SetActive(false);
    }
}

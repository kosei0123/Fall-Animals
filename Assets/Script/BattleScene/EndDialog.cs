using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class EndDialog : MonoBehaviour
{
    //バトル終了時のダイアログ
    [SerializeField]
    private GameObject DialogPanel;

    //順位テキスト表示
    [SerializeField]
    private Text RankingText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //バトル終了時のダイアログ表示
    public void DialogPanelActive(int ranking)
    {
        //バトル終了時ダイアログ表示
        DialogPanel.SetActive(true);

        //順位を表示する
        RankingText.text = ranking.ToString() + " 位 ";

    }

    //ダイアログの「再接続」選択
    public void OnClick_AgainButton()
    {
        //Photonに接続を解除する
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.Disconnect();
        }

        //画面遷移
        SceneManager.LoadScene("Title");
    }

    //ダイアログの「終了」選択
    public void OnClick_EndButton()
    {
        Application.Quit();
    }
}

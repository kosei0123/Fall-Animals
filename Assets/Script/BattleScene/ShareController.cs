using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ShareController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ShareButtonボタンを押した際の挙動
    public void OnClick_ShareButton()
    {
        StartCoroutine(_Share());
    }

    public IEnumerator _Share()
    {
        string imgPath = Application.persistentDataPath + "/image.png";

        //前回のデータを削除
        File.Delete(imgPath);
        //削除が完了するまで待機
        while (true)
        {
            if (!File.Exists(imgPath))
            {
                break;
            }
            yield return null;
        }

        //スクリーンショットを撮影
        ScreenCapture.CaptureScreenshot("image.png");

        //撮影画像の保存が完了するまで待機
        while (true)
        {
            if (File.Exists(imgPath))
            {
                break;
            }

            yield return null;
        }

        //撮影画像の保存処理のため、1フレーム待機
        yield return new WaitForEndOfFrame();

        //投稿する
        string tweetText = "Fall Animalsで遊んでみよう！";

        string tweetURL = "";

        SocialConnector.PostMessage(SocialConnector.ServiceType.Twitter, tweetText, tweetURL, imgPath);
    }
}

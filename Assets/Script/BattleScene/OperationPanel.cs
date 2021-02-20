using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationPanel : MonoBehaviour
{
    //ScreenTouchのpublic定数を使用
    ScreenTouch screenTouch;
    //Timerのpublic定数を使う
    Timer timer;

    //パネルを取得
    [SerializeField]
    private GameObject UpPanel;
    [SerializeField]
    private GameObject DownPanel;
    [SerializeField]
    private GameObject RightPanel;
    [SerializeField]
    private GameObject LeftPanel;

    //パネルを表示する座標を入れ込む
    private RectTransform rectTransform_UpPanel;
    private RectTransform rectTransform_DownPanel;
    private RectTransform rectTransform_RightPanel;
    private RectTransform rectTransform_LeftPanel;

    // Start is called before the first frame update
    void Start()
    {
        //ScreenTouchのpublic定数を使用
        screenTouch = GameObject.Find("ScreenTouch").GetComponent<ScreenTouch>();
        //Timerのpublic定数を使う
        timer = GameObject.Find("TimerCanvas").GetComponent<Timer>();

        //パネルを表示する
        UpPanel.SetActive(true);
        DownPanel.SetActive(true);
        RightPanel.SetActive(true);
        LeftPanel.SetActive(true);

        //パネルを表示する座標を入れ込む
        rectTransform_UpPanel = UpPanel.GetComponent<RectTransform>();
        rectTransform_DownPanel = DownPanel.GetComponent<RectTransform>();
        rectTransform_RightPanel = RightPanel.GetComponent<RectTransform>();
        rectTransform_LeftPanel = LeftPanel.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //UpPanel
        //(left,bottom)
        rectTransform_UpPanel.offsetMin = new Vector2(0, screenTouch.screenUp);
        //(-right,-top)
        rectTransform_UpPanel.offsetMax = new Vector2(0, 0);

        //DownPanel
        //(left,bottom)
        rectTransform_DownPanel.offsetMin = new Vector2(0, 0);
        //(-right,-top)
        rectTransform_DownPanel.offsetMax = new Vector2(0, (-1) * screenTouch.screenUp);

        //RightPanel
        //(left,bottom)
        rectTransform_RightPanel.offsetMin = new Vector2(screenTouch.screenMiddle, screenTouch.screenDown);
        //(-right,-top)
        rectTransform_RightPanel.offsetMax = new Vector2(0, (-1) * screenTouch.screenDown);

        //LeftPanel
        //(left,bottom)
        rectTransform_LeftPanel.offsetMin = new Vector2(0, screenTouch.screenDown);
        //(-right,-top)
        rectTransform_LeftPanel.offsetMax = new Vector2((-1) * screenTouch.screenMiddle, (-1) * screenTouch.screenDown);

        //一定時間後に非表示にする
        if (timer.elapsedTime >= 2.0f)
        {
            UpPanel.SetActive(false);
            DownPanel.SetActive(false);
            RightPanel.SetActive(false);
            LeftPanel.SetActive(false);
        }
    }
}

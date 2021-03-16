using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalCanvasManager : MonoBehaviour
{
    //シングルトンにて1度のみ作成
    private static VerticalCanvasManager verticalCanvasManager_instance;

    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトが作成され、一度のみ永久に破壊されない
        if (verticalCanvasManager_instance == null)
        {
            verticalCanvasManager_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

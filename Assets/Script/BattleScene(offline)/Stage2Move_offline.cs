using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Move_offline: MonoBehaviour
{
    //すり抜け床のゲームオブジェクト
    [SerializeField]
    private GameObject Stage2_scaffold;

    //すり抜け床の上方向移動フラグ
    private bool moveUpFlag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //1より小さければ上に上がり、6より大きければ下に下がる
        if (Stage2_scaffold.transform.position.y < 1.0f) moveUpFlag = true;
        else if (Stage2_scaffold.transform.position.y > 6.0f) moveUpFlag = false;

        switch (moveUpFlag)
        {
            case true:
                Stage2_scaffold.transform.position = new Vector3(Stage2_scaffold.transform.position.x, Stage2_scaffold.transform.position.y + 0.01f, Stage2_scaffold.transform.position.z);
                break;
            case false:
                Stage2_scaffold.transform.position = new Vector3(Stage2_scaffold.transform.position.x, Stage2_scaffold.transform.position.y - 0.01f, Stage2_scaffold.transform.position.z);
                break;
        }

    }
}

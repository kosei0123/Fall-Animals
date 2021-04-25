using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Move_offline : MonoBehaviour
{
    //真ん中床のゲームオブジェクト
    [SerializeField]
    private GameObject Stage4;
    //横の伸びるキューブのゲームオブジェクト
    [SerializeField]
    private GameObject Stage4Cube;
    [SerializeField]
    private GameObject Stage4Cube2;

    //すり抜け床の上方向移動フラグ
    private bool moveUpFlag = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //1より小さければ上に上がり、6より大きければ下に下がる
        if (Stage4.transform.position.y < -10.0f) moveUpFlag = true;
        else if (Stage4.transform.position.y > 10.0f) moveUpFlag = false;

        switch (moveUpFlag)
        {
            case true:
                //真ん中
                Stage4.transform.position = new Vector3(Stage4.transform.position.x, Stage4.transform.position.y + 0.02f, Stage4.transform.position.z);
                //横キューブ
                Stage4Cube.transform.position = new Vector3(Stage4Cube.transform.position.x, Stage4Cube.transform.position.y + 0.01f, Stage4Cube.transform.position.z);
                Stage4Cube.transform.localScale = new Vector3(Stage4Cube.transform.localScale.x, Stage4Cube.transform.localScale.y + 0.01f, Stage4Cube.transform.localScale.z);
                Stage4Cube2.transform.position = new Vector3(Stage4Cube2.transform.position.x, Stage4Cube2.transform.position.y + 0.01f, Stage4Cube2.transform.position.z);
                Stage4Cube2.transform.localScale = new Vector3(Stage4Cube2.transform.localScale.x, Stage4Cube2.transform.localScale.y + 0.01f, Stage4Cube2.transform.localScale.z);
                break;
            case false:
                //真ん中
                Stage4.transform.position = new Vector3(Stage4.transform.position.x, Stage4.transform.position.y - 0.02f, Stage4.transform.position.z);
                //横キューブ
                Stage4Cube.transform.position = new Vector3(Stage4Cube.transform.position.x, Stage4Cube.transform.position.y - 0.01f, Stage4Cube.transform.position.z);
                Stage4Cube.transform.localScale = new Vector3(Stage4Cube.transform.localScale.x, Stage4Cube.transform.localScale.y - 0.01f, Stage4Cube.transform.localScale.z);
                Stage4Cube2.transform.position = new Vector3(Stage4Cube2.transform.position.x, Stage4Cube2.transform.position.y - 0.01f, Stage4Cube2.transform.position.z);
                Stage4Cube2.transform.localScale = new Vector3(Stage4Cube2.transform.localScale.x, Stage4Cube2.transform.localScale.y - 0.01f, Stage4Cube2.transform.localScale.z);
                break;
        }

    }
}

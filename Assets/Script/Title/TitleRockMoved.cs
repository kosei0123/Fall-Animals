using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleRockMoved : MonoBehaviour
{
    //岩にかかる重力や摩擦
    private Rigidbody rbRock;

    // Start is called before the first frame update
    void Start()
    {
        //岩にかかる重力や摩擦
        rbRock = this.GetComponent<Rigidbody>();

        //加速度
        rbRock.velocity = new Vector3(-3.0f, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        //初期位置に戻ってきた際に止める
        if (rbRock.transform.position.x >= 2.6f)
        {
            rbRock.velocity = new Vector3(0, 0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimalDamaged : MonoBehaviour
{
    //かかる重力や摩擦
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //かかる重力や摩擦
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {
        //岩にあたり落下する
        if (this.gameObject.layer == 10 && other.gameObject.tag == "Obstacle")
        {

            //アニメーションの設定
            //characterMainMove.anim.SetBool("Death", true);

            //レイヤーを変更し、下に落ちていく
            this.gameObject.layer = 9;
            this.gameObject.transform.GetChild(0).gameObject.layer = 9;
            //上方向に力を加える
            rb.AddForce(Vector3.up * 5.0f, ForceMode.VelocityChange);
        }
    }
}

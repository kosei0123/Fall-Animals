using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline_teppen : MonoBehaviour
{

    //SoundManagerのスクリプトの関数使用
    SoundManager soundManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "SlidingPlayer")
        {
            //SEの使用
            //soundManager.SEManager("Rock_sound1");
        }
        else
        {
            return;
        }
    }
}

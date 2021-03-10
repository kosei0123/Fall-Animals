using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck_offline : MonoBehaviour
{
    //CharacterMainMoveのpublic定数を使用
    CharacterMainMove_offline characterMainMove_offline;

    //Groundタグへの参照
    private string groundTag = "Ground";

    // Start is called before the first frame update
    void Start()
    {
        //CharacterMainMoveのpublic定数を使用
        characterMainMove_offline = this.transform.parent.gameObject.GetComponent<CharacterMainMove_offline>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //接地判定
    //入る
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == groundTag)
        {
            //地面にいる判定
            characterMainMove_offline.isGround = true;
        }
    }
    //入り続けている
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == groundTag)
        {
            //地面にいる判定
            characterMainMove_offline.isGround = true;
        }
    }
    //抜ける
    private void OnTriggerExit(Collider other)
    {
        //地面にいない判定
        characterMainMove_offline.isGround = false;
    }
}

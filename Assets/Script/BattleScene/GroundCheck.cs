using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //CharacterMainMoveのpublic定数を使用
    CharacterMainMove characterMainMove;

    //Groundタグへの参照
    private string groundTag = "Ground";

    // Start is called before the first frame update
    void Start()
    {
        //CharacterMainMoveのpublic定数を使用
        characterMainMove = this.transform.parent.gameObject.GetComponent<CharacterMainMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //接地判定
    //入る
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == groundTag)
        {
            //地面にいる判定
            characterMainMove.isGround = true;
        }
    }
    //入り続けている
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == groundTag)
        {
            //地面にいる判定
            characterMainMove.isGround = true;
        }
    }
    //抜ける
    private void OnTriggerExit(Collider other)
    {
        //地面にいない判定
        characterMainMove.isGround = false;
    }
}

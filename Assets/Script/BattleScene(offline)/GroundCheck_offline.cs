using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck_offline : MonoBehaviour
{
    //CharacterMainMoveのpublic定数を使用
    CharacterMainMove_offline characterMainMove_offline;

    //Groundタグへの参照
    private string groundTag = "Ground";
    //GroundSlidingタグへの参照
    private string groundSlidingTag = "GroundSliding";
    //Playerタグへの参照
    private string playerTag = "Player";

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
        if (other.tag == groundTag || other.tag == playerTag)
        {
            //地面にいる判定
            characterMainMove_offline.isGround = true;
            //すり抜け床でない地面 or プレイヤーの上にいる
            characterMainMove_offline.GroundSlidingFlag = false;
        }
        if (other.tag == groundSlidingTag)
        {
            //地面にいる判定
            characterMainMove_offline.isGround = true;
            //すり抜け床である
            characterMainMove_offline.GroundSlidingFlag = true;
        }
    }
    //入り続けている
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == groundTag || other.tag == playerTag)
        {
            //地面にいる判定
            characterMainMove_offline.isGround = true;
            //すり抜け床でない地面 or プレイヤーの上にいる
            characterMainMove_offline.GroundSlidingFlag = false;
        }
        if (other.tag == groundSlidingTag)
        {
            //地面にいる判定
            characterMainMove_offline.isGround = true;
            //すり抜け床である
            characterMainMove_offline.GroundSlidingFlag = true;
        }
    }
    //抜ける
    private void OnTriggerExit(Collider other)
    {
        //地面にいない判定
        characterMainMove_offline.isGround = false;
        //空中では見ないようにするためtrueをいれる
        characterMainMove_offline.GroundSlidingFlag = true;
    }
}

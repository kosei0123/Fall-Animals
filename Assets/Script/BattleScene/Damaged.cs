using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Damaged : MonoBehaviour
{
    //CharacterMainMoveのpublic定数を使う
    public GameObject target;
    CharacterMainMove characterMainMove;

    //順位の確定
    private int battleRanking;

    // Start is called before the first frame update
    void Start()
    {
        //CharacterMainMoveのpublic定数を使う
        characterMainMove = target.GetComponent<CharacterMainMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //一定距離落下後に消去
        if (characterMainMove.transformCache.position.y < -50)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }

        //自分の操作キャラでなければ抜ける
        if (characterMainMove.onlineflag == false)
        {
            return;
        }
    }

    //オブジェクトと接触した瞬間に呼び出される
    void OnCollisionEnter(Collision other)
    {
        //障害物と当たった場合以外はreturn
        if (other.gameObject.tag != "Obstacle")
        {
            return;
        }

        //自分の操作キャラでなければ抜ける
        if (characterMainMove.onlineflag == false)
        {
            return;
        }

        //初あたり
        if (this.gameObject.layer == 10)
        {
            //動きを止める
            characterMainMove.onlineflag = false;
            characterMainMove.rb.velocity = new Vector3(0, characterMainMove.rb.velocity.y, 0);

            //アニメーションの設定
            characterMainMove.anim.SetBool("Death", true);

            //順位の確定
            battleRanking = (int)PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"];
            //ルーム内残り人数を減らす
            var n = PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] is int value ? value : 0;
            PhotonNetwork.CurrentRoom.CustomProperties["RemainingPlayerCount"] = n - 1;
            PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

            //レイヤーを変更し、下に落ちていく
            this.gameObject.layer = 9;
            //上方向に力を加える
            characterMainMove.jumpPower = 5.0f;
            characterMainMove.rb.AddForce(Vector3.up * characterMainMove.jumpPower, ForceMode.VelocityChange);
        }
    }
}

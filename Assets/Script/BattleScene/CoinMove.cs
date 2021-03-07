using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CoinMove : MonoBehaviour
{
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
            PhotonNetwork.Destroy(this.gameObject);
        }

        
    }
}

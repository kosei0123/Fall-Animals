using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SelectStage : MonoBehaviour
{
    //ステージ1
    [SerializeField]
    private GameObject Stage1;
    //ステージ2
    [SerializeField]
    private GameObject Stage2;
    //ステージ3
    [SerializeField]
    private GameObject Stage3;
    //ステージ4
    [SerializeField]
    private GameObject Stage4;
    //ステージ5
    [SerializeField]
    private GameObject Stage5;

    // Start is called before the first frame update
    void Start()
    {
        switch (PhotonNetwork.CurrentRoom.CustomProperties["DefinedStage"])
        {
            case 1:
                Stage1.SetActive(true);
                break;
            case 2:
                Stage2.SetActive(true);
                break;
            case 3:
                Stage3.SetActive(true);
                break;
            case 4:
                Stage4.SetActive(true);
                break;
            case 5:
                Stage5.SetActive(true);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

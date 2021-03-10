using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStage_offline : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        int randomStage = Random.Range(1,4);

        switch (randomStage)
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
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

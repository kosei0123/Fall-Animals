using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    //キリンオブジェクト
    public GameObject titleAnimal;
    //岩オブジェクト
    public GameObject titleRock;

    // Start is called before the first frame update
    void Start()
    {
        titleAnimal = (GameObject)Instantiate(Resources.Load("Title/TitleGiraffe"), new Vector3(-4.5f, 1.5f, 0), Quaternion.Euler(0.0f, 90.0f, 0.0f));
        titleRock = (GameObject)Instantiate(Resources.Load("Title/TitleRock"), new Vector3(2.0f, 1.5f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

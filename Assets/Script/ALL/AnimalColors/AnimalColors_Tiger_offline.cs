using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalColors_Tiger_offline : MonoBehaviour
{
    //虎
    //マテリアル
    [SerializeField]
    private Material TigerMaterial_Standard;
    [SerializeField]
    private Material TigerMaterial_White;
    [SerializeField]
    private Material TigerMaterial_Gray;

    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //虎
            case "Tiger(N)":
                this.GetComponent<Renderer>().material = TigerMaterial_Standard;
                break;
            case "Tiger(W)":
                this.GetComponent<Renderer>().material = TigerMaterial_White;
                break;
            case "Tiger(G)":
                this.GetComponent<Renderer>().material = TigerMaterial_Gray;
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            return;
        }

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //虎
            case "Tiger(N)":
                this.GetComponent<Renderer>().material = TigerMaterial_Standard;
                break;
            case "Tiger(W)":
                this.GetComponent<Renderer>().material = TigerMaterial_White;
                break;
            case "Tiger(G)":
                this.GetComponent<Renderer>().material = TigerMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

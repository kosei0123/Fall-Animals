using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalColors_Giraffe_offline : MonoBehaviour
{
    //キリン
    //マテリアル
    [SerializeField]
    private Material GiraffeMaterial_Standard;
    [SerializeField]
    private Material GiraffeMaterial_White;
    [SerializeField]
    private Material GiraffeMaterial_Gray;


    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //キリン
            case "Giraffe(N)":
                this.GetComponent<Renderer>().material = GiraffeMaterial_Standard;
                break;
            case "Giraffe(W)":
                this.GetComponent<Renderer>().material = GiraffeMaterial_White;
                break;
            case "Giraffe(G)":
                this.GetComponent<Renderer>().material = GiraffeMaterial_Gray;
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
            //キリン
            case "Giraffe(N)":
                this.GetComponent<Renderer>().material = GiraffeMaterial_Standard;
                break;
            case "Giraffe(W)":
                this.GetComponent<Renderer>().material = GiraffeMaterial_White;
                break;
            case "Giraffe(G)":
                this.GetComponent<Renderer>().material = GiraffeMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

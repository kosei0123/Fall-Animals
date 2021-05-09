using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalColors_Elephant_offline : MonoBehaviour
{
    //象
    //マテリアル
    [SerializeField]
    private Material ElephantMaterial_Standard;
    [SerializeField]
    private Material ElephantMaterial_White;
    [SerializeField]
    private Material ElephantMaterial_Gray;


    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //象
            case "Elephant(N)":
                this.GetComponent<Renderer>().material = ElephantMaterial_Standard;
                break;
            case "Elephant(W)":
                this.GetComponent<Renderer>().material = ElephantMaterial_White;
                break;
            case "Elephant(G)":
                this.GetComponent<Renderer>().material = ElephantMaterial_Gray;
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
            //象
            case "Elephant(N)":
                this.GetComponent<Renderer>().material = ElephantMaterial_Standard;
                break;
            case "Elephant(W)":
                this.GetComponent<Renderer>().material = ElephantMaterial_White;
                break;
            case "Elephant(G)":
                this.GetComponent<Renderer>().material = ElephantMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

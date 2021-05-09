using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalColors_Cat_offline : MonoBehaviour
{
    //猫
    //マテリアル
    [SerializeField]
    private Material CatMaterial_Standard;
    [SerializeField]
    private Material CatMaterial_White;
    [SerializeField]
    private Material CatMaterial_Gray;

    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //猫
            case "Cat(N)":
                this.GetComponent<Renderer>().material = CatMaterial_Standard;
                break;
            case "Cat(W)":
                this.GetComponent<Renderer>().material = CatMaterial_White;
                break;
            case "Cat(G)":
                this.GetComponent<Renderer>().material = CatMaterial_Gray;
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
            //猫
            case "Cat(N)":
                this.GetComponent<Renderer>().material = CatMaterial_Standard;
                break;
            case "Cat(W)":
                this.GetComponent<Renderer>().material = CatMaterial_White;
                break;
            case "Cat(G)":
                this.GetComponent<Renderer>().material = CatMaterial_Gray;
                break;
            default:
                break;
        }
    }

}

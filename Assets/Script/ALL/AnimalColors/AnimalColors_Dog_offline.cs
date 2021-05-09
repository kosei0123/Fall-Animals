using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalColors_Dog_offline : MonoBehaviour
{
    //犬
    //マテリアル
    [SerializeField]
    private Material DogMaterial_Standard;
    [SerializeField]
    private Material DogMaterial_White;
    [SerializeField]
    private Material DogMaterial_Gray;

    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //犬
            case "Dog(N)":
                this.GetComponent<Renderer>().material = DogMaterial_Standard;
                break;
            case "Dog(W)":
                this.GetComponent<Renderer>().material = DogMaterial_White;
                break;
            case "Dog(G)":
                this.GetComponent<Renderer>().material = DogMaterial_Gray;
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
            //犬
            case "Dog(N)":
                this.GetComponent<Renderer>().material = DogMaterial_Standard;
                break;
            case "Dog(W)":
                this.GetComponent<Renderer>().material = DogMaterial_White;
                break;
            case "Dog(G)":
                this.GetComponent<Renderer>().material = DogMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

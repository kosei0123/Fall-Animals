using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalColors_Rabbit_offline : MonoBehaviour
{
    //ウサギ
    //マテリアル
    [SerializeField]
    private Material RabbitMaterial_Standard;
    [SerializeField]
    private Material RabbitMaterial_White;
    [SerializeField]
    private Material RabbitMaterial_Gray;

    // Start is called before the first frame update
    void Start()
    {

        //キャラクターカラーを変更する
        switch (SelectCharacterUI.animalName_Color)
        {
            //ウサギ
            case "Rabbit(N)":
                this.GetComponent<Renderer>().material = RabbitMaterial_Standard;
                break;
            case "Rabbit(W)":
                this.GetComponent<Renderer>().material = RabbitMaterial_White;
                break;
            case "Rabbit(G)":
                this.GetComponent<Renderer>().material = RabbitMaterial_Gray;
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
            //ウサギ
            case "Rabbit(N)":
                this.GetComponent<Renderer>().material = RabbitMaterial_Standard;
                break;
            case "Rabbit(W)":
                this.GetComponent<Renderer>().material = RabbitMaterial_White;
                break;
            case "Rabbit(G)":
                this.GetComponent<Renderer>().material = RabbitMaterial_Gray;
                break;
            default:
                break;
        }
    }
}

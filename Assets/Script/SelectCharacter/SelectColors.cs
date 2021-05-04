using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectColors : MonoBehaviour
{
    //キリン
    //マテリアル
    [SerializeField]
    private Material GiraffeMaterial_Standard;
    //テクスチャ
    [SerializeField]
    private Texture Giraffe_Color;
    [SerializeField]
    private Texture Giraffe_Color_White;
    [SerializeField]
    private Texture Giraffe_Color_Gray;
    //象
    //マテリアル
    [SerializeField]
    private Material ElephantMaterial_Standard;
    //テクスチャ
    [SerializeField]
    private Texture Elephant_Color;
    [SerializeField]
    private Texture Elephant_Color_White;
    [SerializeField]
    private Texture Elephant_Color_Gray;
    //犬
    //マテリアル
    [SerializeField]
    private Material DogMaterial_Standard;
    //テクスチャ
    [SerializeField]
    private Texture Dog_Color;
    [SerializeField]
    private Texture Dog_Color_White;
    [SerializeField]
    private Texture Dog_Color_Gray;
    //虎
    //マテリアル
    [SerializeField]
    private Material TigerMaterial_Standard;
    //テクスチャ
    [SerializeField]
    private Texture Tiger_Color;
    [SerializeField]
    private Texture Tiger_Color_White;
    [SerializeField]
    private Texture Tiger_Color_Gray;
    //猫
    //マテリアル
    [SerializeField]
    private Material CatMaterial_Standard;
    //テクスチャ
    [SerializeField]
    private Texture Cat_Color;
    [SerializeField]
    private Texture Cat_Color_White;
    [SerializeField]
    private Texture Cat_Color_Gray;
    //ウサギ
    //マテリアル
    [SerializeField]
    private Material RabbitMaterial_Standard;
    //テクスチャ
    [SerializeField]
    private Texture Rabbit_Color;
    [SerializeField]
    private Texture Rabbit_Color_White;
    [SerializeField]
    private Texture Rabbit_Color_Gray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //キャラクターカラーの表示
    public void SelectColors_Display(string animalName, bool normal)
    {
        //初期値
        if (normal == true)
        {
            SelectCharacterUI.animalName_Color_Temporary = animalName + "(N)";
            return;
        }
        //カラーを変更する
        if (SelectCharacterUI.animalName_Color_Temporary == animalName + "(N)")
        {
            SelectCharacterUI.animalName_Color_Temporary = animalName + "(W)";
        }
        else if (SelectCharacterUI.animalName_Color_Temporary == animalName + "(W)")
        {
            SelectCharacterUI.animalName_Color_Temporary = animalName + "(G)";
        }
        else if (SelectCharacterUI.animalName_Color_Temporary == animalName + "(G)")
        {
            SelectCharacterUI.animalName_Color_Temporary = animalName + "(N)";
        }
    }

    //キャラクターカラーを変更する
    public void SelectColors_Decision()
    {
        switch (SelectCharacterUI.animalName_Color)
        {
            //キリン
            case "Giraffe(N)":
                GiraffeMaterial_Standard.SetTexture("_MainTex", Giraffe_Color);
                break;
            case "Giraffe(W)":
                GiraffeMaterial_Standard.SetTexture("_MainTex", Giraffe_Color_White);
                break;
            case "Giraffe(G)":
                GiraffeMaterial_Standard.SetTexture("_MainTex", Giraffe_Color_Gray);
                break;
            //象
            case "Elephant(N)":
                ElephantMaterial_Standard.SetTexture("_MainTex", Elephant_Color);
                break;
            case "Elephant(W)":
                ElephantMaterial_Standard.SetTexture("_MainTex", Elephant_Color_White);
                break;
            case "Elephant(G)":
                ElephantMaterial_Standard.SetTexture("_MainTex", Elephant_Color_Gray);
                break;
            //犬
            case "Dog(N)":
                DogMaterial_Standard.SetTexture("_MainTex", Dog_Color);
                break;
            case "Dog(W)":
                DogMaterial_Standard.SetTexture("_MainTex", Dog_Color_White);
                break;
            case "Dog(G)":
                DogMaterial_Standard.SetTexture("_MainTex", Dog_Color_Gray);
                break;
            //虎
            case "Tiger(N)":
                TigerMaterial_Standard.SetTexture("_MainTex", Tiger_Color);
                break;
            case "Tiger(W)":
                TigerMaterial_Standard.SetTexture("_MainTex", Tiger_Color_White);
                break;
            case "Tiger(G)":
                TigerMaterial_Standard.SetTexture("_MainTex", Tiger_Color_Gray);
                break;
            //猫
            case "Cat(N)":
                CatMaterial_Standard.SetTexture("_MainTex", Cat_Color);
                break;
            case "Cat(W)":
                CatMaterial_Standard.SetTexture("_MainTex", Cat_Color_White);
                break;
            case "Cat(G)":
                CatMaterial_Standard.SetTexture("_MainTex", Cat_Color_Gray);
                break;
            //ウサギ
            case "Rabbit(N)":
                RabbitMaterial_Standard.SetTexture("_MainTex", Rabbit_Color);
                break;
            case "Rabbit(W)":
                RabbitMaterial_Standard.SetTexture("_MainTex", Rabbit_Color_White);
                break;
            case "Rabbit(G)":
                RabbitMaterial_Standard.SetTexture("_MainTex", Rabbit_Color_Gray);
                break;
            default:
                break;
        }
    }

    
}

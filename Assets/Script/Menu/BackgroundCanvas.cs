using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject HorizonCanvas;
    [SerializeField]
    private GameObject VerticalCancas;

    // Start is called before the first frame update
    void Start()
    {
        //縦横で画面切り替える
        if (Screen.width > Screen.height)
        {
            HorizonCanvas.SetActive(true);
            VerticalCancas.SetActive(false);
        }
        else
        {
            HorizonCanvas.SetActive(false);
            VerticalCancas.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //縦横で画面切り替える
        if (Screen.width > Screen.height)
        {
            HorizonCanvas.SetActive(true);
            VerticalCancas.SetActive(false);
        }
        else
        {
            HorizonCanvas.SetActive(false);
            VerticalCancas.SetActive(true);
        }
    }
}

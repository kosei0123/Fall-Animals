using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviewUI : MonoBehaviour
{
    //ステージ4
    [SerializeField]
    private GameObject Stage4;
    //ステージ5
    [SerializeField]
    private GameObject Stage5;

    // Start is called before the first frame update
    void Start()
    {
        switch (UnlockStageUI.unlockStageName)
        {
            case "Stage4":
                Stage4.SetActive(true);
                break;
            case "Stage5":
                Stage5.SetActive(true);
                break;
            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //UnlockBackButton押下時
    public void OnClick_UnlockBackButton()
    {
        SceneManager.LoadScene("Unlock");
    }
}

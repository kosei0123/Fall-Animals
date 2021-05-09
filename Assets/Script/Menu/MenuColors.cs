using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuColors : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick_AnimalColorsButton()
    {
        if (SelectCharacterUI.animalName_Color == SelectCharacterUI.animalName + "(N)")
        {
            SelectCharacterUI.animalName_Color = SelectCharacterUI.animalName + "(W)";
        }
        else if (SelectCharacterUI.animalName_Color == SelectCharacterUI.animalName + "(W)")
        {
            SelectCharacterUI.animalName_Color = SelectCharacterUI.animalName + "(G)";
        }
        else if (SelectCharacterUI.animalName_Color == SelectCharacterUI.animalName + "(G)")
        {
            SelectCharacterUI.animalName_Color = SelectCharacterUI.animalName + "(N)";
        }
    }
}

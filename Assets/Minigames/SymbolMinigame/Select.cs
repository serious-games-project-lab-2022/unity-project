using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    private bool pilotIndices = false; 
    [SerializeField] private GameObject selectionPrefab;

    private GameObject newSelection;

    public bool isSelected;

    private void OnMouseDown()
    {
        if(newSelection == null)
        {
            newSelection = Instantiate(selectionPrefab, transform.position, Quaternion.identity);
            newSelection.transform.SetParent(gameObject.transform);
            newSelection.SetActive(false);
        }
        isSelected = !isSelected;

        if(isSelected)
        {
            // let SymbolMinigame know about it
            SymbolMinigame.addASymbol(this);
            newSelection.SetActive(true);
        }
        else
        {
            // let SymbolMinigame know about it
            SymbolMinigame.deleteASymbol(this);
            newSelection.SetActive(false);
        }
    }


    // set the pilotTexture as true ( according to the solution )
    public void isPilotTexture(bool value)
    {
        pilotIndices = value;
    }


    // return pilotIndices and indicate whether the choosen texture was correct
    public bool isChoosenPilotTextureCorrect()
    {
        return pilotIndices;
    }
}


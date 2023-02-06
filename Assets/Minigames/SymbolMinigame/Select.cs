using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{

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
            // let SymbolController know about it
            newSelection.SetActive(true);
        }
        else
        {
            // let SymbolController know about it
            newSelection.SetActive(false);
        }
    }
}

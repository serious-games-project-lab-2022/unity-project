using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolMinigameShape : MonoBehaviour
{
    [SerializeField] private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => { 
            // change the background
            //
            button.GetComponent<Image>().color = Color.red;
        });


    }

}

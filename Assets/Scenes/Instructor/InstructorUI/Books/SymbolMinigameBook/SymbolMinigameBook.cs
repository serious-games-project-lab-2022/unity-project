using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolMinigameBook : MinigameBook
{
    [SerializeField]
    private ScenarioManager scenarioManagerPrefab;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    // I guess we could let the Functions for the Beta Version be implemented in the parent class
    // in the future we need to improve them
    public override void Display()
    {
        transform.localPosition = Vector3.zero;
    }

    public override void Hide()
    {
        // I'm sorry for this but this is really the simplest solution
        transform.localPosition = new Vector3(1000, 1000, 1000);
    }
}

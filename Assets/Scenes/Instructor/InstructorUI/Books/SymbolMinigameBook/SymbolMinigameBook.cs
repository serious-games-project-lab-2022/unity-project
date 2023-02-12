using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolMinigameBook : MinigameBook
{
    [SerializeField] private ScenarioManager scenarioManagerPrefab;
    [SerializeField] private List<GameObject> symbols;
    [SerializeField] private List<Sprite> textures;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
        SharedGameState.OnInstructorReceivedGameState += () => {
            GenerateSolutionExplanation();
        };
    }

    void GenerateSolutionExplanation()
    {
        var sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
        // TODO: this should not be hard coded
        var symbolMinigameSolution = sharedGameState.minigameSolutions.Value.symbolMinigameSolutions.solution;
        
        mapTheTexturesToTheSymbols(symbolMinigameSolution.instructorSymbolIndices, symbolMinigameSolution.sameSymbolsIndices);
    }

    private void mapTheTexturesToTheSymbols(int[] instructorIndices, int[] similarIndices)
    {     
        // [0, 3)
        for (int i = 0; i < instructorIndices.Length; i++)
        {
            symbols[i].GetComponent<SpriteRenderer>().sprite = textures[instructorIndices[i]];
        }
        // [3,9)
        for (int i = 0; i < similarIndices.Length; i++)
        {
            symbols[i + 3].GetComponent<SpriteRenderer>().sprite = textures[similarIndices[i]];
        }
    }

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

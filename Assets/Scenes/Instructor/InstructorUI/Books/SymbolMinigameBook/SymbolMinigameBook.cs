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
            print(instructorIndices[i]);
        }
        // [3,9)
        for (int i = 0; i < similarIndices.Length; i++)
        {
            symbols[i + 3].GetComponent<SpriteRenderer>().sprite = textures[similarIndices[i]];
        }

        randomiseTheList(symbols, new System.Random());
    }

    public override void Display()
    {
        transform.localPosition = Vector3.zero;
    }

    public override void Hide()
    {
        // I'm sorry for this but this is really the simplest solution
        transform.localPosition = new Vector3(1000, 1000, 1000);
        Desktop.DesktopClean = true;
    }
    private void randomiseTheList(IList<GameObject> list, System.Random rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Vector3 value = list[k].transform.position;
            list[k].transform.position = list[n].transform.position;
            list[n].transform.position = value;
        }
    }
}

using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolController : MonoBehaviour
{
    [SerializeField] private List<GameObject> symbols;
    [SerializeField] private List<Sprite> textures;
   
    private SymbolMinigame symbolMinigame;
    // Start is called before the first frame update
    void Start()
    {
        symbolMinigame = GameObject.FindObjectOfType<SymbolMinigame>();
        mapTheTexturesToTheSymbols();
        
    }

    // Update is called once per frame
    void Update()
    {
        mapTheTexturesToTheSymbols();
    }

    private void mapTheTexturesToTheSymbols()
    {
        var solution = symbolMinigame.getSolutionOfMinigame();
        int[] pilotIndices = solution.pilotSymbolIndices;
        int[] sameIndices = solution.sameSymbolsIndices;
        // [0, 3)
        for (int i = 0; i < pilotIndices.Length; i++)
        {
            symbols[i].GetComponent<SpriteRenderer>().sprite = textures[pilotIndices[i]];
        }
        // [3,9)
        for (int i = 0; i < sameIndices.Length; i++)
        {
            symbols[i+3].GetComponent<SpriteRenderer>().sprite = textures[sameIndices[i]];
        }
    }
}

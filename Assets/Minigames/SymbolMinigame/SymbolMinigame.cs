using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class SymbolMinigame : Minigame
{
    [SerializeField] private List<Symbol> symbols;
    [SerializeField] private List<Sprite> textures;


    private SymbolMinigameSolution solution;
    private ScenarioManager scenarioManager;

    private static List<Symbol> choosenSymbols; 
    

    public static SymbolMinigameSolutions GenerateSolutionForSymbolMinigame(Random random, int numberOfIndices)
    {
        // Range [0, numberOfIndices] 
        //ALG
        // Randomly sort the list of numbers
        // choose the first 6 for the sameSymbolsIndices and the rest 6 for pilotSymbolIndices / instructorSymbolIndices
        int n = numberOfIndices;
        int[] allIndices = new int[numberOfIndices];

        // the first indice is 0
        for(int i = 0; i<numberOfIndices; i++) allIndices[i] = i;

        // sort the numbers randomly
        while (n > 1)
        {
            int k = random.Next(n--);

            int temp = allIndices[n];
            allIndices[n] = allIndices[k];
            allIndices[k] = temp;
        }


       // test for (int i = 0; i < allIndices.Length; i++) print(allIndices[i]);

        int[] sameIndices = new int[6];
        int[] pilotIndices = new int[3];
        int[] instructorIndices = new int[3];

        for (int i = 0; i < sameIndices.Length; i++) sameIndices[i] = allIndices[i];
        for (int i = 0; i < pilotIndices.Length; i++) pilotIndices[i] = allIndices[6+i];
        for (int i = 0; i < instructorIndices.Length; i++) instructorIndices[i] = allIndices[9+i];

        for (int i = 0; i < pilotIndices.Length; i++) print(pilotIndices[i]); // for the test purposes
        return new SymbolMinigameSolutions
        {
            solution = new SymbolMinigameSolution
            {
               sameSymbolsIndices = sameIndices,
               pilotSymbolIndices = pilotIndices,
               instructorSymbolIndices = instructorIndices,
            }
        };
    }


    public override void GetSolution()
    {
        solution = GenerateSolutionForSymbolMinigame(new System.Random(), 24).solution; //scenarioManager.minigameSolutions.symbolMinigameSolutions.solution;

    }
    void Awake()
    {
        scenarioManager = FindObjectOfType<ScenarioManager>();
    }

    public override void CheckSolution()
    {
        bool value = true;

        if(choosenSymbols.Count == 0)
        {
            value = false;
        }
        else
        { 
            if(choosenSymbols.Count > 3 || choosenSymbols.Count < 3)
            {
                value = false;
            }
            else
            {
                foreach (Symbol selectedSymbols in choosenSymbols)
                {
                    value = value && selectedSymbols.isChoosenPilotTextureCorrect();
                }
            }
            
        }
        print(value);
        //EmitEndedEvent(solved);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        
        choosenSymbols = new List<Symbol>();
        //base.Start();
        GetSolution(); // for the test purposes, delete the line, once the base.Start functions 
        mapTheTexturesToTheSymbols(solution.pilotSymbolIndices, solution.sameSymbolsIndices);
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();
        
    }

    
    
    private void mapTheTexturesToTheSymbols(int[] pilotIndices, int[] similarIndices)
    {
       
        // [0, 3)
        for (int i = 0; i < pilotIndices.Length; i++)
        {
            symbols[i].GetComponent<SpriteRenderer>().sprite = textures[pilotIndices[i]];
            symbols[i].isPilotTexture(true);
        }
        // [3,9)
        for (int i = 0; i < similarIndices.Length; i++)
        {
            symbols[i + 3].GetComponent<SpriteRenderer>().sprite = textures[similarIndices[i]];
        }
    }

    public static void addASymbol(Symbol selectedSymbol)
    {
        choosenSymbols.Add(selectedSymbol);
    }


    public static void deleteASymbol(Symbol unselectedSymbol)
    {
        choosenSymbols.Remove(unselectedSymbol);
    }
}

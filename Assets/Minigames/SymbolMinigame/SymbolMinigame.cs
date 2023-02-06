using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class SymbolMinigame : Minigame
{
    SymbolMinigameSolution solution;
    private ScenarioManager scenarioManager;
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


       // for (int i = 0; i < allIndices.Length; i++) print(allIndices[i]);

        int[] sameIndices = new int[6];
        int[] pilotIndices = new int[3];
        int[] instructorIndices = new int[3];

        for (int i = 0; i < sameIndices.Length; i++) sameIndices[i] = allIndices[i];
        for (int i = 0; i < pilotIndices.Length; i++) pilotIndices[i] = allIndices[6+i];
        for (int i = 0; i < instructorIndices.Length; i++) instructorIndices[i] = allIndices[9+i];

        
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
        solution = scenarioManager.minigameSolutions.symbolMinigameSolutions.solution;
    }
    void Awake()
    {
        scenarioManager = FindObjectOfType<ScenarioManager>();
    }

    public override void CheckSolution()
    {

        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}

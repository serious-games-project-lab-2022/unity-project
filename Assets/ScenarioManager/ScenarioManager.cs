using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    public List<MinigameShape> minigameShapePrefabs;
    public MinigameSolutions minigameSolutions;
    public OverworldMapGenerator overworldMapGenerator;
    public Terrain terrain;
    public List<Slider> frequencyMinigameSliders;
    
    public void generateMinigameSolutions()
    {
        minigameSolutions.shapeMinigameSolutions = ShapeMinigame.GenerateConfiguration(minigameShapePrefabs);
        minigameSolutions.frequencyMinigameSolutions = FrequencyMinigame.GenerateSolutionForFrequencyMinigame(frequencyMinigameSliders);
        minigameSolutions.symbolMinigameSolutions = SymbolMinigame.GenerateSolutionForSymbolMinigame(new System.Random(), 24);
    }

    public void generateScenario()
    {
        overworldMapGenerator = new OverworldMapGenerator();
        terrain = overworldMapGenerator.GenerateTerrain();
        generateMinigameSolutions();
    }
}

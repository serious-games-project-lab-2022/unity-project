using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    public List<MinigameShape> minigameShapePrefabs;
    public MinigameSolutions minigameSolutions;

    public List<Slider> frequenzMinigameSliders;
    public void generateMinigameSolutions()
    {
        minigameSolutions.shapeMinigameSolutions = ShapeMinigame.GenerateConfiguration(minigameShapePrefabs);
        minigameSolutions.frequencyMinigameSolutions = FrequencyMinigame.GenerateSolutionForFrequenceMinigame(frequenzMinigameSliders);
    }

    public void generateScenario()
    {
        generateMinigameSolutions();
    }
}

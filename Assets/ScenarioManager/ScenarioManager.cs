using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public List<MinigameShape> minigameShapePrefabs;
    public MinigameSolutions minigameSolutions;

    public void generateMinigameSolutions()
    {
        minigameSolutions = new MinigameSolutions {
            shapeMinigameSolution = ShapeMinigame.GenerateConfiguration(minigameShapePrefabs).ToArray()
        };
    }

    public void generateScenario()
    {
        generateMinigameSolutions();
    }
}

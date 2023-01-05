using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapeMinigameBook : MonoBehaviour
{
    [SerializeField]
    private ScenarioManager scenarioManagerPrefab;
    private Grid grid;
    void Start()
    {
        grid = transform.Find("Grid").GetComponent<Grid>();
        SharedGameState.OnInstructorReceivedGameState += () => {
            GenerateSolutionExplanation();
        };
    }

    void GenerateSolutionExplanation()
    {
        var sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
        var shapeMinigameSolution = sharedGameState.minigameSolutions.Value.shapeMinigameSolution;

        for (var i = 0; i < shapeMinigameSolution.Length; i++)
        {
            var shapePrefab = scenarioManagerPrefab.minigameShapePrefabs[i];
            var shapePosition = shapeMinigameSolution[i];

            var shape = Instantiate(shapePrefab, parent: grid.transform);
            shape.transform.localPosition = shapePosition;
        }
    }
}

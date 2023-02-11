using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapeMinigameBook : MinigameBook
{
    [SerializeField]
    private ScenarioManager scenarioManagerPrefab;
    private Grid grid;

    void Start()
    {
        Hide();
        grid = transform.Find("Grid").GetComponent<Grid>();
        SharedGameState.OnInstructorReceivedGameState += () => {
            GenerateSolutionExplanation();
        };
    }

    void GenerateSolutionExplanation()
    {
        var sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
        // TODO: this should not be hard coded
        var shapeMinigameSolution = sharedGameState.minigameSolutions.Value.shapeMinigameSolutions.solutions;

        foreach (var index in shapeMinigameSolution.shapeIndices)
        {
            var shapePrefab = scenarioManagerPrefab.minigameShapePrefabs[index];
            var shapePosition = shapeMinigameSolution.relativePositions[index];

            var shape = Instantiate(shapePrefab, parent: grid.transform);
            shape.transform.localPosition = shapePosition;
        }
    }
}

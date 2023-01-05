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
        var shapeMinigameSolution = sharedGameState.minigameSolutions.Value.shapeMinigameSolution;

        for (var i = 0; i < shapeMinigameSolution.Length; i++)
        {
            var shapePrefab = scenarioManagerPrefab.minigameShapePrefabs[i];
            var shapePosition = shapeMinigameSolution[i];

            var shape = Instantiate(shapePrefab, parent: grid.transform);
            shape.transform.localPosition = shapePosition;
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

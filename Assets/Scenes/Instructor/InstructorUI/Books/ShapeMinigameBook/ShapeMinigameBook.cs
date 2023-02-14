using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapeMinigameBook : MinigameBook
{
    private Grid grid;

    void Start()
    {
        Hide();
        grid = transform.Find("Grid").GetComponent<Grid>();

        if (GameManager.Singleton.sharedGameState != null)
        {
            GenerateSolutionExplanation();
        }
        var instructorManager = GameObject.FindObjectOfType<InstructorManager>();
        instructorManager.OnInstructorReceivedGameState += () => {
            GenerateSolutionExplanation();
        };
    }

    void GenerateSolutionExplanation()
    {
        // TODO: this should not be hard coded
        var sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
        var shapeMinigameSolution = sharedGameState.minigameSolutions.Value.shapeMinigameSolutions.solutions;
        foreach (var index in shapeMinigameSolution.shapeIndices)
        {
            var shapePrefab = GameManager.Singleton.scenarioManager.minigameShapePrefabs[index];
            var shapePosition = shapeMinigameSolution.relativePositions[index];

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
        Desktop.DesktopClean = true;
    }
}

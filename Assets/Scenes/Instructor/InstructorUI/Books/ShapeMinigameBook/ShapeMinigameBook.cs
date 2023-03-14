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
            GenerateSolutionAndSubscribe();
        }
        var instructorManager = GameObject.FindObjectOfType<InstructorManager>();
        instructorManager.OnInstructorReceivedGameState += GenerateSolutionAndSubscribe;
    }
    void GenerateSolutionAndSubscribe()
    {
        GameManager.Singleton.sharedGameState.minigameSolutions.OnValueChanged += SubscribeToSolution;
        GenerateSolutionExplanation(GameManager.Singleton.sharedGameState.minigameSolutions.Value.shapeMinigameSolutions.solutions);
    }

    void SubscribeToSolution(MinigameSolutions _, MinigameSolutions newMinigameSolution)
    {

        GenerateSolutionExplanation(newMinigameSolution.shapeMinigameSolutions.solutions);
    }
    private void GenerateSolutionExplanation(ShapeMinigameSolution solution)
    {
        clearTheShapes(grid);
        var shapeMinigameSolution = solution;
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
    private void OnDestroy()
    {
        GameManager.Singleton.sharedGameState.minigameSolutions.OnValueChanged -= SubscribeToSolution;

    }
    private void clearTheShapes(Grid grid)
    {
        for(int i = 0; i< grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
    }
}

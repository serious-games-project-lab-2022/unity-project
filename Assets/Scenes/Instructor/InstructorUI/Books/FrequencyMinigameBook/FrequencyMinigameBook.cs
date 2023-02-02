using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyMinigameBook : MinigameBook
{
    [SerializeField]
    private ScenarioManager scenarioManagerPrefab;
    private Sinewave sinewave;

    void Start()
    {
        Hide();
        //sinewave = transform.Find("Sinewave").GetComponent<Sinewave>();
        SharedGameState.OnInstructorReceivedGameState += () => {
            GenerateSolutionExplanation();
        };
    }

    void GenerateSolutionExplanation()
    {
        var sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
        // TODO: this should not be hard coded
        var frequenzMinigameSolution = sharedGameState.minigameSolutions.Value.frequenzMinigameSolutions.solution;

        sinewave.amplitude = frequenzMinigameSolution.amplitude;
        sinewave.frequency = frequenzMinigameSolution.frequence;

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
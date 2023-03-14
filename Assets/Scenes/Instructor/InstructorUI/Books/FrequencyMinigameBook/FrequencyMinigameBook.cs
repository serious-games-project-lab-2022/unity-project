using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyMinigameBook : MinigameBook
{
    private Sinewave sinewave;

    void Start()
    {
        Hide();
        sinewave = FindObjectOfType<Sinewave>();

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
        GenerateSolutionExplanation(GameManager.Singleton.sharedGameState.minigameSolutions.Value.frequencyMinigameSolutions.solution);
    }
    void SubscribeToSolution(MinigameSolutions _, MinigameSolutions newMinigameSolution)
    {
        GenerateSolutionExplanation(newMinigameSolution.frequencyMinigameSolutions.solution);
    }

    private void GenerateSolutionExplanation(FrequencyMinigameSolution solution)
    {
        sinewave.amplitude = solution.amplitude;
        sinewave.frequency = solution.frequency;
        sinewave.DrawSineWave();
        print("test");
        print(sinewave.amplitude);
        print(sinewave.frequency);
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
}
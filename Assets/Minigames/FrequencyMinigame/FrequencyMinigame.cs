using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

public class FrequencyMinigame : Minigame
{
    FrequencyMinigameSolution solution;
    private ScenarioManager scenarioManager;
    [SerializeField]
    private SineWaveController sineWaveController;
    public float solutionTolerance = 0.5f;

    public static FrequencyMinigameSolutions GenerateSolutionForFrequencyMinigame(List<Slider> frequencyMinigameSliders)
    {
        float amplitudeMinValue = frequencyMinigameSliders[0].minValue;
        float amplitudeMaxValue = frequencyMinigameSliders[0].maxValue;

        float frequencyMinValue = frequencyMinigameSliders[1].minValue;
        float frequencyMaxValue = frequencyMinigameSliders[1].maxValue;
        
        return new FrequencyMinigameSolutions
        {
            solution = new FrequencyMinigameSolution
            {
                // Both the lower and upper bounds are inclusive
                amplitude = UnityEngine.Random.Range(amplitudeMinValue, amplitudeMaxValue),
                frequency = UnityEngine.Random.Range(frequencyMinValue, frequencyMaxValue),
            }
        };
    }

    public override void GetSolution()
    {
        print("Pilot");
        solution = scenarioManager.minigameSolutions.frequencyMinigameSolutions.solution;
        Debug.Log(solution.amplitude);
        Debug.Log(solution.frequency);
    }

    void Awake()
    {
        scenarioManager = FindObjectOfType<ScenarioManager>();
    }

    public override void CheckSolution()
    {
        var sineWave = sineWaveController.sineWave;
        var solved = (
            nearlyEqual(sineWave.frequency, solution.frequency, solutionTolerance)
            && nearlyEqual(sineWave.amplitude, solution.amplitude, solutionTolerance)
        );
        EmitEndedEvent(solved);
    }

    protected override void Start()
    {
        base.Start();
        sineWaveController = FindObjectOfType<SineWaveController>();
        sineWaveController.EnableSliders(takeInput);
    }

    protected override void Update()
    {
        base.Update();
        sineWaveController.EnableSliders(takeInput);
    }
   
    private bool nearlyEqual(float observed, float expected, float tolerance)
    {
        return Math.Abs(observed - expected) <= tolerance;
    }
}

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

    public static FrequencyMinigameSolutions GenerateSolutionForFrequenceMinigame(List<Slider> frequencyMinigameSliders)
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

    void GetSolution()
    {
        solution = scenarioManager.minigameSolutions.frequencyMinigameSolutions.solution;
    }

    void Awake()
    {
        scenarioManager = FindObjectOfType<ScenarioManager>();
    }

    public override void CheckSolution()
    {
        var sineWave = GameObject.FindObjectOfType<Sinewave>();
        var solved = (nearlyEqual(solution.frequency,sineWave.frequency, float.Epsilon)) && (nearlyEqual(solution.amplitude,sineWave.amplitude, float.Epsilon));
        EmitEndedEvent(solved);
    }

    protected override void Start()
    {
        base.Start();
        GetSolution();
        FindObjectOfType<CanvasCameraSettings>().SetCamera();
        sineWaveController = FindObjectOfType<SineWaveController>();
        sineWaveController.EnableSliders(takeInput);
    }

    protected override void Update()
    {
        base.Update();
        sineWaveController.EnableSliders(takeInput);
    }
   
    private bool nearlyEqual(float solution, float result, float epsilon)
    {
        double diff = Math.Abs(solution - result);

        if (solution.Equals(result))
        {
            return true;
        }
        else
        {
            return diff / (solution + result) < epsilon;
        }
    }
}

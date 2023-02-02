using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

public class FrequenzMinigame : Minigame
{
    FrequenzMinigameSolution solution;
    private ScenarioManager scenarioManager;
    private SineWaveController sineWaveController;

    public  static FrequenzMinigameSolutions GenerateSolutionForFrequenceMinigame(List<Slider> frequenzMinigameSliders)
    {
        
        float amplitudeMinValue = frequenzMinigameSliders[0].minValue;
        float amplitudeMaxValue = frequenzMinigameSliders[0].maxValue;

        float frequenceMinValue = frequenzMinigameSliders[1].minValue;
        float frequenceMaxValue = frequenzMinigameSliders[1].maxValue;

        
        return new FrequenzMinigameSolutions
        {
            solution = new FrequenzMinigameSolution
            {
                // Both the lower and upper bounds are inclusive
                amplitude = UnityEngine.Random.Range(amplitudeMinValue, amplitudeMaxValue),
                frequence = UnityEngine.Random.Range(frequenceMinValue, frequenceMaxValue),
               
            }
        };


    }

    void GetSolution ()
    {
        
        solution = scenarioManager.minigameSolutions.frequenzMinigameSolutions.solution;
    }

    private void PlaceSinWave()
    {
        // Place the Minigame
    }


    void Awake()
    {
        scenarioManager = FindObjectOfType<ScenarioManager>();
    }

    public override void CheckSolution()
    {
        var sineWave = GameObject.FindObjectOfType<Sinewave>();
        var solved = (nearlyEqual(solution.frequence,sineWave.frequency, float.Epsilon)) && (nearlyEqual(solution.amplitude,sineWave.amplitude, float.Epsilon));
        EmitEndedEvent(solved);
    }

    protected override void Start()
    {
        base.Start();
        GetSolution();
        FindObjectOfType<CanvasCameraSettings>().SetCamera();
        sineWaveController = FindObjectOfType<SineWaveController>();
        sineWaveController.EnableSliders(takeInput);
        PlaceSinWave();

    }

    protected override void Update()
    {
        base.Update();
        sineWaveController.EnableSliders(takeInput);
    }

   
    private bool nearlyEqual(float solution, float result, float epsilon)
    {
        // The Solution is always > 0.5
        double diff = Math.Abs(solution - result);

        if (solution.Equals(result))
        {
            return true;
        }
        else
        { // use relative error
            return diff / (solution + result) < epsilon;
        }
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityEngine.UI;

public class FrequenzMinigame : Minigame
{

    [SerializeField] private Slider FrequenceSlider, AmplitudeSlider;

    FrequenzMinigameSolution solution;
    private ScenarioManager scenarioManager;


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
                amplitude = Random.Range(amplitudeMinValue, amplitudeMaxValue),
                frequence = Random.Range(frequenceMinValue, frequenceMaxValue),
               
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
        var solved = (sineWave.frequency == solution.frequence) && (sineWave.amplitude == solution.amplitude);
        EmitEndedEvent(solved);
    }

    protected override void Start()
    {
        base.Start();
        GetSolution();
        FindObjectOfType<CanvasCameraSettings>().SetCamera();
        FindObjectOfType<SineWaveController>().EnableSliders(takeInput);
        PlaceSinWave();

    }

    protected override void Update()
    {
        base.Update();
        FindObjectOfType<SineWaveController>().EnableSliders(takeInput);
    }

   

   
}

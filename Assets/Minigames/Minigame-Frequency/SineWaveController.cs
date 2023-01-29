using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SineWaveController : MonoBehaviour
{
    [SerializeField] private Slider frequencySlider;
    [SerializeField] private Slider amplitudeSlider;

    // Start is called before the first frame update
    void Start()
    {
        var sineWave = GameObject.FindObjectOfType<Sinewave>();
        print(sineWave == null);
        frequencySlider.onValueChanged.AddListener((v) =>
        {
            sineWave.frequency = (float)v;
        });
        amplitudeSlider.onValueChanged.AddListener((v) =>
        {
            sineWave.amplitude = (float)v;
        });
    }


    public void EnableSliders(bool input)
    {
        if (!input)
        {
            frequencySlider.enabled = false;
            amplitudeSlider.enabled = false;
        }
    }
}

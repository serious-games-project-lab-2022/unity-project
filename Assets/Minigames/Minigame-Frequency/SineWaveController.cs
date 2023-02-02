using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SineWaveController : MonoBehaviour
{
    [SerializeField] private Slider frequencySlider;
    [SerializeField] private Slider amplitudeSlider;
    [SerializeField] private Sinewave sinewave;

    void Start()
    {
        frequencySlider.onValueChanged.AddListener((v) => {
            sinewave.frequency = (float) v;
        });
        amplitudeSlider.onValueChanged.AddListener((v) => {
            sinewave.amplitude = (float) v;
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

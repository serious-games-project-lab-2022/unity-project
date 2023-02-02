using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    [SerializeField]
    private PilotManager pilotManager;
    private Image bar;

    void Awake()
    {
        bar = GetComponent<Image>();
        pilotManager.OnFuelChanged += (float newFuelValue) => {
            UpdateFuelBar(fillAmount: ((float) newFuelValue) / pilotManager.maxFuel);
        };
    }

    void UpdateFuelBar(float fillAmount)
    {
        bar.fillAmount = fillAmount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private PilotManager pilotManager;
    private Image bar;

    void Awake()
    {
        bar = GetComponent<Image>();
        pilotManager.OnHealthChanged += (int newHealthValue) => {
            UpdateHealthBar(fillAmount: ((float) newHealthValue) / pilotManager.maxHealth);
        };
    }

    void UpdateHealthBar(float fillAmount)
    {
        bar.fillAmount = fillAmount;
    }
}

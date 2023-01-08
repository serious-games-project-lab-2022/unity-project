using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private Image bar;

    void Awake()
    {
        bar = GetComponent<Image>();
        gameManager.OnHealthChanged += (int newHealthValue) => {
            UpdateHealthBar(fillAmount: ((float) newHealthValue) / gameManager.maxHealth);
        };
    }

    void UpdateHealthBar(float fillAmount)
    {
        bar.fillAmount = fillAmount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Image healthImage;
    public int maxHealth = 1;
    [HideInInspector]
    public int currentHealthAmount;

    public delegate void HealthChanged(int newHealthValue);
    public event HealthChanged OnHealthChanged = delegate {};

    private void Start()
    {
        currentHealthAmount = maxHealth;
        MinigameHandler.OnPlayerLostMinigame += (int damageAmount) =>
        {
            DepleteHealth(by: damageAmount);
        };
    }

    private void DepleteHealth(int by = 1)
    {
        var damageAmount = by;
        currentHealthAmount -= damageAmount;
        OnHealthChanged(newHealthValue: currentHealthAmount);
        if (currentHealthAmount <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
    }
}

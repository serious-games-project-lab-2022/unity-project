using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Image healthImage;
    public float currentHealthAmount;
    public float maxHealth = 1;
    // private float fuelAmount;

    


    

    private void Awake()
    {
        healthImage = transform.Find("HealthBar").GetComponent<Image>();
    }
   
    private void Start()
    {
        currentHealthAmount = maxHealth;
        MinigameHandler.OnPlayerLostMinigame += (float healthValue) =>
        {
            DepletHealth(healthValue);
        };
    }


    private void DepletHealth(float healthValue)
    {
        currentHealthAmount -= healthValue;
        healthImage.fillAmount = currentHealthAmount;

        if(currentHealthAmount <= 0f)
        {
            GameOver();
        }
    }

    public static void GameOver()
    {

    }
   

   










}

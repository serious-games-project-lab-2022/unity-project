using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float currentHealthAmount;
    public float maxHealth = 1;
    // private float fuelAmount;

    private Image healthImage;


    public delegate void PlayerLostMinigame(float value);
    public static event PlayerLostMinigame OnPlayerLostMinigame = delegate { };

    private void Awake()
    {
        healthImage = transform.Find("HealthBar").GetComponent<Image>();
    }
   
    private void Start()
    {
        currentHealthAmount = maxHealth;
        OnPlayerLostMinigame += (float healthValue) =>
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
   

   


    private void GameOver()
    {

    }








}

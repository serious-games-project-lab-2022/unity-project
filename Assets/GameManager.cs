using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private float healthAmount;
    // private float fuelAmount;

    private Image healthImage;


    private void Awake()
    {
        healthImage = transform.Find("HealthBar").GetComponent<Image>();
    }
    private void Start()
    {
        healthAmount = 1f;
    }

    private void Update()
    {
        UpdateTheHealthBar(healthAmount);
    }

    private void UpdateTheHealthBar(float healthValue)
    {
        healthImage.fillAmount = healthValue;

        if(healthAmount <= 0f)
        {
            GameOver();
        }
    }
   

    public void setHealthAmount(float h)
    {
        this.healthAmount = h;
    }


    private void GameOver()
    {

    }








}

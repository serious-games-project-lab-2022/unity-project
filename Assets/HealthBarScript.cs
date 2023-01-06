using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{


    private Image barImage;

    private void Awake()
    {
        barImage = transform.Find("HealthBar").GetComponent<Image>();
    }
    // Start is called before the first frame update
   private void SetHealth(float healthValue)
    {
        barImage.fillAmount = healthValue;
    }
    // Update is called once per frame
    private void Start()
    {
        //Testing
        SetHealth(0.8f);
    }
}

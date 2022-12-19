using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    [SerializeField] private float secondsToSolve = 30f;

    private float secondsLeftToSolve;
    
    [SerializeField] protected Image timerBar;

    public delegate void MinigameOver();
    public static event MinigameOver OnMinigameOver;

    private MinigameShapeController minigameShapeController;

    
    // Start is called before the first frame update
    virtual protected void Start()
    { 
       minigameShapeController = FindObjectOfType<MinigameShapeController>();
       minigameShapeController.SetCanMoveShapes(true);
       secondsLeftToSolve = secondsToSolve;
       Debug.Log("Minigame start");
       timerBar.fillAmount = 1f; // Sets the timerbar to full
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        secondsLeftToSolve-= Time.deltaTime;
        timerBar.fillAmount = secondsLeftToSolve / secondsToSolve; // Makes the timerbar deplete over time
        if(secondsLeftToSolve<=0)
        {
            if (OnMinigameOver!= null)
            {
                OnMinigameOver(); // Trigger MinigameOver Event
            }
            minigameShapeController.SetCanMoveShapes(false);
            Debug.Log("Time's UP!");
        }
    }
}

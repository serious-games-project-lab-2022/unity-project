using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    [SerializeField] private float secondsToSolve = 30f;
    private float secondsLeftToSolve;
    
    [SerializeField] protected Image timerBar;

    [SerializeField] private MinigameShapeController minigameShapeController;

    public delegate void MinigameOver();
    public event MinigameOver OnMinigameOver = delegate { };
    
    // Start is called before the first frame update
    virtual protected void Start()
    { 
       minigameShapeController.SetCanMoveShapes(true);
       secondsLeftToSolve = secondsToSolve;
       // Set the timer bar to full
       timerBar.fillAmount = 1f;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (secondsLeftToSolve > 0)
        {
            depleteTimerBar();
            if (secondsLeftToSolve < 0)
            {
                OnMinigameOver();
                minigameShapeController.SetCanMoveShapes(false);
                secondsLeftToSolve = 0;
            }
        }
    }

    private void depleteTimerBar()
    {
        secondsLeftToSolve -= Time.deltaTime;
        timerBar.fillAmount = secondsLeftToSolve / secondsToSolve;
    }
}

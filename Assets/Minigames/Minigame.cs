using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] private float secondsToSolve = 30f;
    private float secondsLeftToSolve;
    public bool takeInput = true;  
    [SerializeField] protected Image timerBar;
    public delegate void MinigameOver(bool solved);
    public event MinigameOver OnMinigameOver = delegate { };


    virtual protected void Start()
    { 
        GetSolution();
        SetCamera();

        takeInput = true; 
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
                CheckSolution();
                takeInput = false; 
                secondsLeftToSolve = 0;
            }
        }
    }

    private void depleteTimerBar()
    {
        secondsLeftToSolve -= Time.deltaTime;
        timerBar.fillAmount = secondsLeftToSolve / secondsToSolve;
    }

    protected void EmitEndedEvent(bool solved)
    {
        OnMinigameOver(solved);
    }

    public abstract void GetSolution();

    public abstract void CheckSolution();

    public void SetCamera()
    {
        var canvas = GetComponentInChildren<Canvas>();
        var minigameCamera = GameObject.FindGameObjectWithTag("MinigameCamera").GetComponent<Camera>();
        canvas.worldCamera = minigameCamera;
    }

    
}

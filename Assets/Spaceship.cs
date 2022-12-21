using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour
{
    private SharedGameState sharedGameState;

    void Start()
    {
        // sharedGameState = GameObject.FindObjectOfType<SharedGameState>();
    }

    void Update()
    {
        // sharedGameState.spaceshipPosition.Value = new Vector2(transform.position.x, transform.position.y);
    }

    // what to do when Overworld Goal is achieved
    public void achievedGoal(bool achieved)
    {
        if(achieved == true){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
            Debug.Log("Game Over!");   
        }
    }

}

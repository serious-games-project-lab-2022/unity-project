using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenInstructor : MonoBehaviour
{
    [SerializeField] private GameObject pilotCheckbox;
    [SerializeField] private float timer;
    private bool startedCount =false;

    void Update()
    {
        if(GameManager.Singleton.sharedGameState!= null)
        {
            bool ready = GameManager.Singleton.sharedGameState.instructorInvitedToStart.Value && GameManager.Singleton.sharedGameState.pilotInvitedToStart.Value;
           
            if (ready || startedCount)
            {
                startedCount = true;
                pilotCheckbox.SetActive(true);
                timer -= Time.fixedDeltaTime;
                print(timer);
            }
            if (timer < 0)
            {
                this.gameObject.SetActive(false);
                GameObject.FindObjectOfType<InstructorManager>().resumeTheGame();
                Destroy(this);
            }
        }
       
    }
}

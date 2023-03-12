using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenInstructor : MonoBehaviour
{
    void Update()
    {
        if(GameManager.Singleton.sharedGameState!= null)
        {
            bool ready = GameManager.Singleton.sharedGameState.instructorInvitedToStart.Value && GameManager.Singleton.sharedGameState.pilotInvitedToStart.Value;
           
            if (ready)
            {
                print("hmmm");
                this.gameObject.SetActive(false);
                Destroy(this);
            }
        }
       
    }
}

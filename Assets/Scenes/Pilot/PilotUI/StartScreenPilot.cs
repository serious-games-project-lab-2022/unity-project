using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class StartScreenPilot : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Singleton.sharedGameState != null)
        {
            bool ready = GameManager.Singleton.sharedGameState.instructorInvitedToStart.Value && GameManager.Singleton.sharedGameState.pilotInvitedToStart.Value;
            if (ready)
            {
                this.gameObject.SetActive(false);
                Destroy(this);
            }
        }
    }
}

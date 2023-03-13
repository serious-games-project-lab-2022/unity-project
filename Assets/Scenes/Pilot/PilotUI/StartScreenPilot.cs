using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class StartScreenPilot : MonoBehaviour
{
    [SerializeField] private GameObject instructorCheckBox;
    [SerializeField] private float timer;
    void Update()
    {
        if (GameManager.Singleton.sharedGameState != null)
        {
            bool ready = GameManager.Singleton.sharedGameState.instructorInvitedToStart.Value && GameManager.Singleton.sharedGameState.pilotInvitedToStart.Value;

            if (ready)
            {
                instructorCheckBox.SetActive(true);
                timer -= Time.fixedDeltaTime;
            }
            if (timer < 0)
            { 
                this.gameObject.SetActive(false);
                GameObject.FindObjectOfType<PilotManager>().resumeTheGame();
                Destroy(this);
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SharedGameState : NetworkBehaviour
{
    public NetworkVariable<Vector2> spaceshipPosition = new NetworkVariable<Vector2>(new Vector2(0, 0));
    public NetworkVariable<float> spaceshipRotation = new NetworkVariable<float>(0f);
    public NetworkVariable<Vector2> overworldGoalPosition = new NetworkVariable<Vector2>(new Vector2(0, 0));
    public NetworkVariable<MinigameSolutions> minigameSolutions = new NetworkVariable<MinigameSolutions>();
    public NetworkVariable<bool> instructorInvitedToRetry = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> pilotInvitedToRetry = new NetworkVariable<bool>(false);

    private bool IsPilot {
        get { return IsHost; }
    }
    private bool IsInstructor {
        get { return !IsHost; }
    }


    public delegate void InstructorReceivedGameEndedRpc(bool gameEndedSuccessfully);
    public event InstructorReceivedGameEndedRpc OnInstructorReceivedGameEndedRpc = delegate {};
   

    public override void OnNetworkSpawn()
    {
        if (IsPilot)
        {
            instructorInvitedToRetry.OnValueChanged += RetryGameIfBothPlayersInvitedEachOther;
            pilotInvitedToRetry.OnValueChanged += RetryGameIfBothPlayersInvitedEachOther;
        }

        if (IsInstructor)
        {
            DontDestroyOnLoad(this);
            GameManager.Singleton.sharedGameState = this;
            GameObject.FindObjectOfType<InstructorManager>().OnReceivedGameState();
            InstructorReadyServerRpc();
        }
    }

    private void RetryGameIfBothPlayersInvitedEachOther(bool _previousInvitationStatus, bool _currentInvitationStatus)
    {
        if (!IsPilot)
        {
            return;
        }

        if (instructorInvitedToRetry.Value && pilotInvitedToRetry.Value)
        {
            instructorInvitedToRetry.Value = false;
            pilotInvitedToRetry.Value = false;

            RetryGameForInstructorClientRpc();
            GameManager.Singleton.TransitionToGameScene();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void InstructorReadyServerRpc()
    {
    }

    [ServerRpc(RequireOwnership = false)]
    public void InstructorInvitePilotToNewGameServerRpc()
    {
        instructorInvitedToRetry.Value = true;
    }

    [ClientRpc]
    public void GameEndedClientRpc(bool gameEndedSuccessfully)
    {
        OnInstructorReceivedGameEndedRpc(gameEndedSuccessfully);
    }

    public void InviteToRetry()
    {
        if (IsInstructor)
        {
            InstructorInvitePilotToNewGameServerRpc();
        }
        if (IsPilot)
        {
            pilotInvitedToRetry.Value = true;
        }
    }

    [ClientRpc]
    public void RetryGameForInstructorClientRpc()
    {
        GameManager.Singleton.TransitionToGameScene();
    }

}

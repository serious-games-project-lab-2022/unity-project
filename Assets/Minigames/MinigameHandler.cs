using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class MinigameHandler : MonoBehaviour
{
    [SerializeField] private List<Minigame> minigamePrefabs;
    [SerializeField] private List<GameObject> checkpointPositions;
    private int checkpointIndex = 0;
    private int minigameIndex = 0;
    [SerializeField] private Checkpoint checkpointPrefab;
    public delegate void PlayerLostMinigame(float damageAmount);
    public event PlayerLostMinigame OnPlayerLostMinigame = delegate { };

    
    void Start()
    {
        SpawnCheckpoint(checkpointPositions[checkpointIndex].transform.position + new Vector3(0, 18, 0));
        ++checkpointIndex;
    }

    public void SpawnMinigame()
    {
        var scenarioManager = GameObject.FindObjectOfType<ScenarioManager>();
        var minigame = Instantiate(
            minigamePrefabs[minigameIndex],
            parent: this.transform
        );

        minigame.transform.localPosition = new Vector3(8, 0, 0);
        minigame.OnMinigameOver += (bool solved) =>
        {
            Destroy(minigame.gameObject);
            if(minigameIndex <= 2) 
            {
                ++minigameIndex;
            }
           
            if (!solved)
            {
                OnPlayerLostMinigame(damageAmount: 3.0f);
            }
            SpawnCheckpoint(checkpointPositions[checkpointIndex].transform.position + new Vector3(0, 18, 0));
            if(checkpointIndex <= 2) 
            {
                ++checkpointIndex;
            }
        };
    }

    public void SpawnCheckpoint(Vector3 newPosition) 
    {
        var checkpoint = Instantiate(
            checkpointPrefab, parent: this.transform
        );

        checkpoint.transform.localPosition = newPosition;
        var sharedGameState = GameManager.Singleton.sharedGameState;
       
        if (sharedGameState != null)
        {
            sharedGameState.checkpointPosition.Value = newPosition;
        }
    }
}

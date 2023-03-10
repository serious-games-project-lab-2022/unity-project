using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class MinigameHandler : MonoBehaviour
{
    [SerializeField] private List<Minigame> minigamePrefabs;
    [SerializeField] private List<GameObject> checkpointPositions;
    private int currentMinigameIndex = 0;
    [SerializeField] private Checkpoint checkpointPrefab;
    [SerializeField] private Transform worldOrigin;
    public delegate void PlayerLostMinigame(float damageAmount);
    public event PlayerLostMinigame OnPlayerLostMinigame = delegate { };

    
    void Start()
    {
        Debug.Assert(minigamePrefabs.Count == checkpointPositions.Count);
        SpawnCheckpoint(checkpointPositions[currentMinigameIndex].transform.position);
    }

    public void SpawnMinigame()
    {
        var scenarioManager = GameObject.FindObjectOfType<ScenarioManager>();
        var minigame = Instantiate(
            minigamePrefabs[currentMinigameIndex],
            parent: this.transform
        );
        minigame.transform.localPosition = new Vector3(8, 0, 0);

        minigame.OnMinigameOver += (bool solved) =>
        {
            Destroy(minigame.gameObject);
            if (!solved)
            {
                OnPlayerLostMinigame(damageAmount: 3.0f);
            }
        };

        currentMinigameIndex++;
        if (currentMinigameIndex < minigamePrefabs.Count) {
            SpawnCheckpoint(checkpointPositions[currentMinigameIndex].transform.position);
        }
    }

    public void SpawnCheckpoint(Vector3 newPosition) 
    {
        var checkpoint = Instantiate(
            checkpointPrefab,
            parent: worldOrigin.transform
        );
        checkpoint.transform.localPosition = newPosition - worldOrigin.transform.position;
        checkpoint.OnCheckpointReached += SpawnMinigame;
    }
}

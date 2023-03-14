using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using SystemRandom = System.Random;


public class MinigameHandler : MonoBehaviour
{
    [SerializeField] private List<Minigame> minigamePrefabs;
    private List<Vector3Int> checkpointPositions;
    private int currentMinigameIndex = 0;
    [SerializeField] private Checkpoint checkpointPrefab;
    [SerializeField] private OverworldGoal goalPrefab;
    [SerializeField] private Transform worldOrigin;
    public delegate void PlayerLostMinigame(float damageAmount);
    public event PlayerLostMinigame OnPlayerLostMinigame = delegate { };
    private Checkpoint currentCheckpoint;
    private OverworldGoal currentOverworldGoal;

    [SerializeField] private AudioSource minigameAppearSound;
    [SerializeField] private AudioSource minigameSuccessfulSound;
    [SerializeField] private AudioSource minigameUnsuccessfulSound;

    void Start()
    {
        checkpointPositions = GameManager.Singleton.scenarioManager.terrain.checkpointList;
        SpawnCheckpoint(checkpointPositions[currentMinigameIndex + 1]);
        minigamePrefabs = minigamePrefabs.OrderBy(x => Random.value).ToList();
    }

    public void SpawnMinigame()
    {
        var scenarioManager = GameObject.FindObjectOfType<ScenarioManager>();
        var minigame = Instantiate(
            minigamePrefabs[currentMinigameIndex],
            parent: this.transform
        );
        minigame.transform.localPosition = new Vector3Int(8, 0, 0);
        minigameAppearSound.Play();

        minigame.OnMinigameOver += (bool solved) =>
        {
            GameObject.FindObjectOfType<PilotManager>().score += 100f;
            Destroy(minigame.gameObject);
            if (!solved)
            {
                GameObject.FindObjectOfType<PilotManager>().score -= 100f;
                OnPlayerLostMinigame(damageAmount: 3.0f);
                minigameUnsuccessfulSound.Play();
            }
            else
            {
                minigameSuccessfulSound.Play();
            }

            if (currentCheckpoint != null)
            {
                currentCheckpoint.Activate();
            }
            if (currentOverworldGoal != null)
            {
                currentOverworldGoal.Activate();
            }
        };

        currentMinigameIndex++;
        if (currentMinigameIndex < minigamePrefabs.Count) {
            SpawnCheckpoint(checkpointPositions[currentMinigameIndex + 1]);
        } else {
            SpawnGoal(checkpointPositions[currentMinigameIndex + 1]);
        }
    }

    public void SpawnCheckpoint(Vector3Int newPosition) 
    {
        currentCheckpoint = Instantiate(
            checkpointPrefab,
            parent: worldOrigin.transform
        );
        currentCheckpoint.transform.localPosition = newPosition;
        currentCheckpoint.OnCheckpointReached += SpawnMinigame;
        if (currentMinigameIndex != 0)
        {
            currentCheckpoint.Deactivate();
        }
    }

    public void SpawnGoal(Vector3Int newPosition) 
    {
        currentOverworldGoal = Instantiate(
            goalPrefab,
            parent: worldOrigin.transform
        );
        currentOverworldGoal.transform.localPosition = newPosition;
        currentOverworldGoal.OnCollidedWithSpaceship += () =>
        {
            GameObject.FindObjectOfType<PilotManager>().EndGame(gameEndedSuccessfully: true);
        };
        currentOverworldGoal.Deactivate();
    }
}

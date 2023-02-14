using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class MinigameHandler : MonoBehaviour
{
    [SerializeField] private List<Minigame> minigamePrefabs;
    private int minigameIndex = 0;
    [SerializeField] private ShapeMinigame shapeMinigamePrefab;
    [SerializeField] private FrequencyMinigame frequencyMinigamePrefab;
    [SerializeField] private SymbolMinigame symbolMinigamePrefab;
    [SerializeField] private MinigoalCheckpoint minigoalCheckpoint;
    public delegate void PlayerLostMinigame(float damageAmount);
    public event PlayerLostMinigame OnPlayerLostMinigame = delegate { };

    /*
    void Start()
    {
        minigoalCheckpoint.OnCheckpointReached += () =>
        {
            SpawnMinigame();
        };
    }
    */
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
            ++minigameIndex;
            if (!solved)
            {
                OnPlayerLostMinigame(damageAmount: 3.0f);
            }
        };

        /*var scenarioManager = GameObject.FindObjectOfType<ScenarioManager>();
        var shapeMinigame = Instantiate(
            shapeMinigamePrefab,
            parent: this.transform
        );

        shapeMinigame.transform.localPosition = new Vector3(8, 0, 0);
        shapeMinigame.OnMinigameOver += (bool solved) =>
        {
            Destroy(shapeMinigame.gameObject);
            if (!solved)
            {
                OnPlayerLostMinigame(damageAmount: 3.0f);
            }
        };*/

        /*var frequencyMinigame = Instantiate(frequencyMinigamePrefab, parent: transform);

        frequencyMinigame.transform.localPosition = new Vector3(8, 0, 0);
        frequencyMinigame.OnMinigameOver += (bool solved) =>
        {
            Destroy(frequencyMinigame.gameObject);
            if (!solved)
            {
                OnPlayerLostMinigame(damageAmount: 3.0f);
            }
        };*/


        /* SymbolMinigame 
        var symbolMinigame = Instantiate(symbolMinigamePrefab, parent: transform);

        symbolMinigame.transform.localPosition = new Vector3(8, 0, 0);
        symbolMinigame.OnMinigameOver += (bool solved) =>
        {
            Destroy(symbolMinigame.gameObject);
            if (!solved)
            {
                OnPlayerLostMinigame(damageAmount: 3.0f);
            }
        };
        */
    }
}

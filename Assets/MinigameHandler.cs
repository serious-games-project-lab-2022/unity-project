using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameHandler : MonoBehaviour
{
    [SerializeField] private ShapeMinigame shapeMinigamePrefab;

    void Start()
    {
        var scenarioManager = GameObject.FindObjectOfType<ScenarioManager>();
        shapeMinigamePrefab.SetSolution(new List<Vector2> (scenarioManager.minigameSolutions.shapeMinigameSolution));
        var shapeMinigame = Instantiate(
            shapeMinigamePrefab,
            parent: this.transform
        );
        shapeMinigame.transform.localPosition = new Vector3(8, 0, 0);
        shapeMinigame.OnMinigameOver += () => {
            Destroy(shapeMinigame.gameObject);
        };
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameHandler : MonoBehaviour
{
    [SerializeField] private ShapeMinigame shapeMinigamePrefab;

    public delegate void PlayerLostMinigame(int damageAmount);
    public static event PlayerLostMinigame OnPlayerLostMinigame = delegate { };

    void Start()
    {
        var scenarioManager = GameObject.FindObjectOfType<ScenarioManager>();
        var shapeMinigame = Instantiate(
            shapeMinigamePrefab,
            parent: this.transform
        );
        shapeMinigame.transform.localPosition = new Vector3(8, 0, 0);
        shapeMinigame.OnMinigameOver += (bool solved) => {
            Destroy(shapeMinigame.gameObject);
            if (!solved)
            {
                OnPlayerLostMinigame(damageAmount: 1);
            }
        };
    }
}

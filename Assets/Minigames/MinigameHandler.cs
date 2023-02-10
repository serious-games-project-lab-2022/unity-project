using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameHandler : MonoBehaviour
{
    [SerializeField] private ShapeMinigame shapeMinigamePrefab;
    [SerializeField] private FrequencyMinigame frequencyMinigamePrefab;
    [SerializeField] private SymbolMinigame symbolMinigamePrefab;
    public delegate void PlayerLostMinigame(float damageAmount);
    public static event PlayerLostMinigame OnPlayerLostMinigame = delegate { };

    void Start()
    {
        /*var scenarioManager = GameObject.FindObjectOfType<ScenarioManager>();
        var shapeMinigame = Instantiate(
            shapeMinigamePrefab,
            parent: this.transform
        );
   
        shapeMinigame.transform.localPosition = new Vector3(8, 0, 0);
        shapeMinigame.OnMinigameOver += (bool solved) => {
            Destroy(shapeMinigame.gameObject);
            if (!solved)
            {
                OnPlayerLostMinigame(damageAmount: 3);
            }
        };*/

        /*var frequencyMinigame = Instantiate(frequencyMinigamePrefab, parent: transform);
        
        frequencyMinigame.transform.localPosition = new Vector3(8, 0, 0);
        frequencyMinigame.OnMinigameOver += (bool solved) => {
            Destroy(frequencyMinigame.gameObject);
            if (!solved)
            {
                OnPlayerLostMinigame(damageAmount: 3.0f);
            }
        };*/


        // SymbolMinigame 
        var symbolMinigame = Instantiate(symbolMinigamePrefab, parent: transform);

        symbolMinigame.transform.localPosition = new Vector3(8, 0, 0);
        symbolMinigame.OnMinigameOver += (bool solved) => {
            Destroy(symbolMinigame.gameObject);
            if (!solved)
            {
                OnPlayerLostMinigame(damageAmount: 3.0f);
            }
        };
    }
}

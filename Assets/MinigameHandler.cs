using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameHandler : MonoBehaviour
{
    [SerializeField] private ShapeMinigame shapeMinigamePrefab;
    // Start is called before the first frame update
    void Start()
    {
        var newShapeMinigameSolution = ShapeMinigame.GenerateConfiguration(
            shapeMinigamePrefab.shapePrefabs
        );
        shapeMinigamePrefab.SetSolution(newShapeMinigameSolution);
        var shapeMinigame = Instantiate(
            shapeMinigamePrefab,
            position: new Vector3(8, 0, 0),
            rotation: Quaternion.identity,
            parent: this.transform
        );
        shapeMinigame.transform.localPosition = new Vector3(8, 0, 0);
        shapeMinigame.OnMinigameOver += () => {
            Destroy(shapeMinigame.gameObject);
        };
    }
}

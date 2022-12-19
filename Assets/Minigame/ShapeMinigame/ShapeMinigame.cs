using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShapeMinigame : Minigame
{
    public List<MinigameShape> shapePrefabs;

    void GenerateConfiguration()
    {
        var randomNumberGenerator = new System.Random();
        // Shuffle prefabs
        var shuffledPrefabs = shapePrefabs.OrderBy(item => randomNumberGenerator.Next());
        var absolutePositions = new List<Vector2Int>() {};

        foreach (var shapePrefab in shuffledPrefabs)
        {
            if (absolutePositions.Count == 0)
            {
                // The first part gets the absolute position (0, 0)
                absolutePositions.Add(Vector2Int.zero);
            }

            // Pick random side

            // Pick random coordinate perpendicular to side normal

            // Find closest coordinate parallel to side normal

            // Save coordinate

            // Add shape to configuration
        }
        // Compute relative positions between all 
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinewave : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberPoints;
    public float amplitude = 1.75f;
    public float frequency = 0.2f;
    public float speed = 0;
    public Vector2 widthLimits = new Vector2(-6.5f, 6.5f);

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        DrawSineWave();
    }

    void DrawSineWave()
    {
        float xStart = widthLimits.x;
        float Tau = 2 * Mathf.PI;
        float xFinish = widthLimits.y;

        lineRenderer.positionCount = numberPoints;

        for (int currentPoint = 0; currentPoint < numberPoints; currentPoint++)
        {
            float progress = (float) currentPoint / (numberPoints-1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((Tau * x * frequency) + (Time.timeSinceLevelLoad*speed));
            lineRenderer.SetPosition(currentPoint, new Vector3(x, y, -3));
        }
    }
}

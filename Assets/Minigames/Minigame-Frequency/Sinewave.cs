using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinewave : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int points;
    public float amplitude = 1;
    public float frequency = 1;
    public float shift = 1;
    public Vector2 xLimits = new Vector2(0, 1);




    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawTheLine();
    }

    void DrawTheLine()
    {
        float xStart = xLimits.x;
        float Tau = 2 * Mathf.PI;
        float xFinish = xLimits.y;

        lineRenderer.positionCount = points;

        for(int currentPoint = 0; currentPoint< points; currentPoint++)
        {
            float progress = (float) currentPoint / (points-1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((Tau * x * frequency) + (Time.timeSinceLevelLoad*shift));
            lineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraSettings : MonoBehaviour
{
    public void SetCamera()
    {
        var canvas = GetComponent<Canvas>();
        var minigameCamera = GameObject.FindGameObjectWithTag("MinigameCamera").GetComponent<Camera>();
        canvas.worldCamera = minigameCamera;
    }
}

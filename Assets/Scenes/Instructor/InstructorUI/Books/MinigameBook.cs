using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameBook: MonoBehaviour
{
    public void Display()
    {
        transform.localPosition = Vector3.zero;
    }

    public void Hide()
    {
        // I'm sorry for this but this is really the simplest solution
        transform.localPosition = new Vector3(1000, 1000, 1000);
    }
}

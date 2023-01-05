using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameBookIcon : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private ShapeMinigameBook book;

    // Required for the IPointerDownHandler
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        book.Display();
    }
}

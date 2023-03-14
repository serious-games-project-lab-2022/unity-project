using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameBookIcon : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private MinigameBook book;
    // Required for the IPointerDownHandler
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(Desktop.DesktopClean)
        {
            book.Display();
            Desktop.DesktopClean = false;
        }
        else
        {
            print("Clean the Desktop by clicking on X ! ");
        }
       
    }
}

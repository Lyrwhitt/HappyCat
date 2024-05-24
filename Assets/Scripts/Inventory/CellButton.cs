using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class CellButton : MonoBehaviour, IPointerClickHandler
{
    public Action onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            onClick?.Invoke();
        }
    }
}

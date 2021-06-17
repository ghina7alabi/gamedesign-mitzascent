using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class EasyHardScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Color selectColor, deselectColor;

    //void Update
    //{

    //}

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = selectColor;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().fontSize = 45;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = deselectColor;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().fontSize = 40;
    }

    //public void OnPointerEnter(PointerEventData pointerEventData)
    //{
    //    gameObject.GetComponentInChildren<TextMeshProUGUI>().color = selectColor;
    //    gameObject.GetComponentInChildren<TextMeshProUGUI>().fontSize = 55;
    //}

    //public void OnPointerExit(PointerEventData pointerEventData)
    //{
    //    gameObject.GetComponentInChildren<TextMeshProUGUI>().color = deselectColor;
    //    gameObject.GetComponentInChildren<TextMeshProUGUI>().fontSize = 40;
    //}
}
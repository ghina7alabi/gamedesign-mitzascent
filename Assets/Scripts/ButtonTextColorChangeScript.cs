using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextColorChangeScript : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Color selectColor, deselectColor;

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = selectColor;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().fontSize = 75;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = deselectColor;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().fontSize = 70;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = selectColor;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().fontSize = 75;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().color = deselectColor;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().fontSize = 70;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIElementBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 
    public string tootTipParameter;
    public TooltipManager tooltip;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("Tooltip");
        tooltip.gameObject.SetActive(true);
        tooltip.UpdateTooltip(tootTipParameter);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        tooltip.gameObject.SetActive(false);
    }
}

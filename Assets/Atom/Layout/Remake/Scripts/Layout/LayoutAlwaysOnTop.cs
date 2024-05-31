using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Required when using UI Elements.
using UnityEngine.EventSystems; // Required when using event data.

public class LayoutAlwaysOnTop : MonoBehaviour, IPointerDownHandler
{
    public RectTransform panelRectTransform;

    void Start()
    {
        panelRectTransform.SetAsLastSibling();
    }

    public void InvokeAlwaysOnTop()
    {
        panelRectTransform.SetAsLastSibling();
    }

    //Invoked when the mouse pointer goes down on a UI element.
    public void OnPointerDown(PointerEventData data)
    {
        // Puts the panel to the front as it is now the last UI element to be drawn.
        panelRectTransform.SetAsLastSibling();
    }
}
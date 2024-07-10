using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomScrollRect : ScrollRect
{
    // Flag to track if the ScrollRect is being dragged
    public bool isDragging = false;

    public bool IsRunning => isDragging || (inertia && (horizontal ? Mathf.Abs(velocity.x) > 0 : false) || (vertical ? Mathf.Abs(velocity.y) > 0 : false));

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (ScrollManager.Instance.IsAnyScrollRectActive() && !ScrollManager.Instance.IsScrollRectActive(this))
        {
            eventData.pointerDrag = null;
            return;
        }

        base.OnBeginDrag(eventData);
        isDragging = true;
        ScrollManager.Instance.RegisterScrollRect(this);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        isDragging = false;
        ScrollManager.Instance.UnregisterScrollRect(this);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (ScrollManager.Instance.IsScrollRectActive(this))
        {
            base.OnDrag(eventData);
        }
    }
}

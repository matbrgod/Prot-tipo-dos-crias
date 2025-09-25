using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private Vector2 originalPosition;
    private Canvas dragCanvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        dragCanvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.localPosition;

        // Allow raycasts to pass through this item while dragging
        canvasGroup.blocksRaycasts = false;

        // Move item to top of hierarchy so it stays visible while dragging
        transform.SetParent(dragCanvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragCanvas == null)
            return;

        // Convert screen position to local position inside canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragCanvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        transform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Check if dropped over a slot
        GameObject hit = eventData.pointerEnter;

        if (hit != null && hit.CompareTag("Slot"))
        {
            // Snap into the slot
            transform.SetParent(hit.transform);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            // If dropped outside slots, destroy the item
            float distanceFromCanvas = Vector3.Distance(transform.position, dragCanvas.transform.position);

            if (distanceFromCanvas > 500f) // adjust threshold if needed
            {
                Destroy(gameObject);
            }
            else
            {
                // Otherwise, snap back to original position
                transform.SetParent(originalParent);
                transform.localPosition = originalPosition;
            }
        }

        // Restore raycast blocking
        canvasGroup.blocksRaycasts = true;
    }
}
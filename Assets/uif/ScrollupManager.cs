using UnityEngine;
using UnityEngine.EventSystems;

public class BottomSheet : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("References")]
    public RectTransform handle; // Drag the "Handle" child object here

    [Header("Position Settings")]
    public float closedY = -1200f; 
    public float openY = 0f;   
    
    [Header("Sensitivity")]
    [Range(0.1f, 0.9f)]
    public float snapThreshold = 0.5f; 
    public float flickVelocity = 400f; // Lowered slightly for easier flicking

    private RectTransform rectTransform;
    private Coroutine moveCoroutine;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, closedY);
        if (handle == null) handle = rectTransform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != handle.gameObject && handle != rectTransform) return;

        if (moveCoroutine != null) StopCoroutine(moveCoroutine);

        float newY = rectTransform.anchoredPosition.y + eventData.delta.y;
        newY = Mathf.Clamp(newY, closedY, openY + 20f); 
        rectTransform.anchoredPosition = new Vector2(0, newY);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float currentY = rectTransform.anchoredPosition.y;
        float dragVelocityY = eventData.delta.y / Time.deltaTime;

        if (Mathf.Abs(dragVelocityY) > flickVelocity)
        {
            SnapTo(dragVelocityY > 0 ? openY : closedY);
        }
        else
        {
            float progress = Mathf.InverseLerp(closedY, openY, currentY);
            SnapTo(progress > snapThreshold ? openY : closedY);
        }
    }

    void SnapTo(float targetY)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(SmoothMove(targetY));
    }

    System.Collections.IEnumerator SmoothMove(float targetY)
    {
        float elapsed = 0;
        float duration = 0.2f; 
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = new Vector2(0, targetY);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t); 
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = endPos;
    }
}
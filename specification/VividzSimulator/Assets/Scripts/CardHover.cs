using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalPosition;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localPosition = originalPosition + new Vector3(0, 20, 0); // 少し上にズラす
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localPosition = originalPosition; // 元の位置に戻す
    }
}

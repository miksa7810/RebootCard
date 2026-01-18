using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalPosition;

    public float hoverOffset = 20f;  // 上にずらす距離

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localPosition = originalPosition + new Vector3(0, hoverOffset, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localPosition = originalPosition;
    }
}

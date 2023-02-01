using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("onBeginDrag");
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("onDrag");
        GameObject canva = GetComponentInParent<Player>().canva;
        rectTransform.anchoredPosition += eventData.delta / canva.GetComponent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("onEndDrag");
    }
    
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("onPointerDown");
    }
}

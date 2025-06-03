using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string Content;
    public void OnPointerEnter(PointerEventData eventData) {
        UIManager.Instance.SetHoverUI(true, Content, GetWorldPositionForHoverUI());
    }

    public void OnPointerExit(PointerEventData eventData) {
        UIManager.Instance.SetHoverUI(false, Content, GetWorldPositionForHoverUI());
    }

    private Vector3 GetWorldPositionForHoverUI() {
        Vector3 worldPoint;
        RectTransform canvasRect = UIManager.Instance.WorldCanvas.transform as RectTransform;

        // 현재 UI 오브젝트의 중심을 기준으로 포지션 변환
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasRect,
            RectTransformUtility.WorldToScreenPoint(null, transform.position),
            UIManager.Instance.WorldCanvas.worldCamera, 
            out worldPoint
        );

        return worldPoint;
    }
}

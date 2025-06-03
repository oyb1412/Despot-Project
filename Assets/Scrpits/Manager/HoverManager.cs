using UnityEngine;

public class HoverManager : MonoBehaviour
{
    public LayerMask targetLayer;
    public string Content;
    private void Start() {
        
    }
    void OnMouseEnter() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, targetLayer);

        if (hit.collider != null && hit.collider.gameObject == gameObject) {
            UIManager.Instance.SetHoverUI(true, Content, transform.position);
        }
    }

    void OnMouseExit() {
        UIManager.Instance.SetHoverUI(false, Content, transform.position);
    }
}

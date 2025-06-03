using TMPro;
using UnityEngine;

public class HoverUi : MonoBehaviour
{
    public TextMeshProUGUI hoverTMP;

    public void SetHoverUI(bool isHover, string content, Vector3 pos) {
        hoverTMP.text = content;
        gameObject.SetActive(isHover);
        transform.position = pos + Vector3.left * 2 + Vector3.down * 2;
    }
}

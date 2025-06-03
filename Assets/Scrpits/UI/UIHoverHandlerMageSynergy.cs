using UnityEngine;

public class UIHoverHandlerMageSynergy : MonoBehaviour {
    private UIHoverHandler handler;

    private void Start() {
        handler = GetComponent<UIHoverHandler>();
        SetHandler(1);
        PlayerCretureManager.Instance.UnitSlotManager.OnMageUnitChanged += SetHandler;
    }

    public void SetHandler(int mageAmount) {
        handler.Content = "모든 아군의 치명타 확률이 " + (1+ 0.1f * mageAmount) + " 배 만큼 증가합니다";
    }
}